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

    public string PageTitle => Order.Id > 0 ? $"Order #{Order.Id}" : "Order Details";

    partial void OnOrderChanged(Order value)
    {
        OnPropertyChanged(nameof(PageTitle));
    }

    [RelayCommand]
    private async Task GoEditAsync()
    {
        var parameters = new Dictionary<string, object>
        {
            { "Order", Order }
        };
        await Shell.Current.GoToAsync(nameof(OrderCreatePage), parameters);
    }

    [RelayCommand]
    private async Task ShareOrderAsync()
    {
        if (string.IsNullOrWhiteSpace(Order.CustomerName)) return;

        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Title = OrderIdToString(),
            Text = OrderToString()
        });
    }

    [RelayCommand]
    private async Task CopyOrderToClipboardAsync()
    {
        if (string.IsNullOrWhiteSpace(Order.CustomerName)) return;

        await Clipboard.Default.SetTextAsync(OrderToString());

        var toast = Toast.Make($"Order #{OrderIdToString()} copied!");
        await toast.Show();
    }

    [RelayCommand]
    private async Task GoEditAsync()
    {
        await Shell.Current.GoToAsync(nameof(OrderCreatePage), new Dictionary<string, object>
        {
            { "Order", Order }
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
               $"Total: {Order.TotalAmount:N0} HUF\n" +
               $"Status: {(Order.IsDelivered ? "Delivered" : "Pending")}\n" +
               $"Created: {Order.CreatedAt:yyyy-MM-dd HH:mm}";
    }
}
