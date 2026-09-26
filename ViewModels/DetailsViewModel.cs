using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OrderManagerMaui.Models;
using OrderManagerMaui.Views;

namespace OrderManagerMaui.ViewModels;

[QueryProperty(nameof(Order), nameof(Order))]
public partial class DetailsViewModel : ObservableObject
{
    [ObservableProperty]
    public partial Order Order { get; set; } = new();

    public string PageTitle => $"Order #{Order.Id}";

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
    private async Task CopyOrderToClipboardAsync()
    {
        if (string.IsNullOrWhiteSpace(Order.CustomerName)) return;

        var orderIdDisplay = Order.Id > 0 ? Order.Id.ToString() : "New";

        var text = $"Order #{orderIdDisplay}\n" +
                   $"Customer: {Order.CustomerName}\n" +
                   $"Address: {Order.Address}\n" +
                   $"Total: {Order.TotalAmount:N0} HUF\n" +
                   $"Status: {(Order.IsDelivered ? "Delivered" : "Pending")}\n" +
                   $"Created: {Order.CreatedAt:yyyy-MM-dd HH:mm}";

        await Clipboard.Default.SetTextAsync(text);

        if (Shell.Current is not null)
        {
            await Shell.Current.DisplayAlertAsync("Clipboard", "Order details copied to clipboard.", "OK");
        }
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}
