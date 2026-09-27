using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OrderManagerMaui.Models;
using OrderManagerMaui.Services;
using OrderManagerMaui.Views;

namespace OrderManagerMaui.ViewModels;

[QueryProperty(nameof(Order), nameof(Order))]
public partial class DetailsViewModel(DatabaseService db) : ObservableObject
{
    [ObservableProperty]
    public partial Order Order { get; set; } = new();

    [ObservableProperty]
    public partial bool IsBusy { get; set; }

    public bool IsNotBusy => !IsBusy;
    public string PageTitle => Order.Id > 0 ? $"Order #{Order.Id}" : "Order Details";
    public bool HasCoordinates => Order is { Latitude: not null, Longitude: not null };
    public bool CanDeliver => !Order.IsDelivered;
    public bool IsReadonly => !Order.IsDelivered;

    partial void OnOrderChanged(Order value)
    {
        OnPropertyChanged(nameof(PageTitle));
        OnPropertyChanged(nameof(HasCoordinates));
        OnPropertyChanged(nameof(CanDeliver));
        OnPropertyChanged(nameof(IsReadonly));
    }

    partial void OnIsBusyChanged(bool value)
    {
        OnPropertyChanged(nameof(IsNotBusy));
    }

    [RelayCommand]
    public async Task RefreshAsync()
    {
        if (Order.Id <= 0) return;

        var refreshed = await db.GetOrderById(Order.Id);
        if (refreshed is not null)
        {
            Order = refreshed;
            OnPropertyChanged(nameof(HasCoordinates));
            OnPropertyChanged(nameof(CanDeliver));
            OnPropertyChanged(nameof(IsReadonly));
        }
    }

    [RelayCommand]
    private async Task DeliverOrderAsync()
    {
        if (Order.Id <= 0 || Order.IsDelivered || IsBusy) return;

        if (Shell.Current is not null)
        {
            var confirmed = await Shell.Current.DisplayAlertAsync(
                "Confirm Delivery",
                $"Are you sure you want to mark Order #{Order.Id} as delivered?",
                "Yes",
                "No"
            );
            if (!confirmed) return;
        }

        IsBusy = true;

        try
        {
            var permStatus = await GetLocationPermission();
            if (!permStatus)
            {
                if (Shell.Current is not null)
                {
                    await Shell.Current.DisplayAlertAsync(
                        "Permission Required",
                        "Location access is required to record delivery coordinates.",
                        "OK"
                    );
                }

                return;
            }

            var toast = Toast.Make("Resolving GPS location");
            await toast.Show();

            var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));
            var location = await Geolocation.Default.GetLocationAsync(request);

            if (location is not null)
            {
                Order.Latitude = location.Latitude;
                Order.Longitude = location.Longitude;
            }

            Order.IsDelivered = true;
            await db.SaveOrder(Order);
            await RefreshAsync();

            toast = Toast.Make("Order delivered and GPS location saved");
            await toast.Show();
        }
        catch (Exception ex)
        {
            if (Shell.Current is not null)
            {
                await Shell.Current.DisplayAlertAsync("GPS Error", ex.Message, "OK");
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task OpenMapAsync()
    {
        if (Order.Latitude is null || Order.Longitude is null || IsBusy) return;

        await Map.Default.OpenAsync(Order.Latitude.Value, Order.Longitude.Value, new MapLaunchOptions
        {
            Name = Order.CustomerName,
            NavigationMode = NavigationMode.None
        });
    }

    [RelayCommand]
    private async Task ShareOrderAsync()
    {
        if (string.IsNullOrWhiteSpace(Order.CustomerName) || IsBusy) return;

        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Title = OrderIdToString(),
            Text = OrderToString()
        });
    }

    [RelayCommand]
    private async Task CopyOrderToClipboardAsync()
    {
        if (string.IsNullOrWhiteSpace(Order.CustomerName) || IsBusy) return;

        await Clipboard.Default.SetTextAsync(OrderToString());

        var toast = Toast.Make($"Order #{OrderIdToString()} copied!");
        await toast.Show();
    }

    [RelayCommand]
    private async Task GoEditAsync()
    {
        if (IsBusy) return;

        await Shell.Current.GoToAsync(nameof(OrderCreatePage), new Dictionary<string, object>
        {
            { nameof(Order), Order }
        });
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    private string OrderIdToString()
    {
        return Order.Id > 0 ? Order.Id.ToString() : "New";
    }

    private string OrderToString()
    {
        return $"Order #{OrderIdToString()}\n" +
               $"Customer: {Order.CustomerName}\n" +
               $"Address: {Order.Address}\n" +
               $"Total: {Order.TotalAmount:N0} Ft\n" +
               $"Status: {(Order.IsDelivered ? "Delivered" : "Pending")}\n" +
               $"Created: {Order.CreatedAt:yyyy-MM-dd HH:mm}";
    }

    private async Task<bool> GetLocationPermission()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        }

        return status == PermissionStatus.Granted;
    }
}
