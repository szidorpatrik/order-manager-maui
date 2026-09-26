using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OrderManagerMaui.Models;
using OrderManagerMaui.Services;
using OrderManagerMaui.Views;

namespace OrderManagerMaui.ViewModels;

public partial class OrdersViewModel(DatabaseService db) : ObservableObject
{
    [ObservableProperty]
    public partial ObservableCollection<Order> Orders { get; set; } = [];

    [ObservableProperty]
    public partial Order SelectedOrder { get; set; } = new();

    [RelayCommand]
    private async Task LoadOrdersAsync()
    {
        var orders = await db.GetOrders();

        Orders = new ObservableCollection<Order>(orders);
    }

    [RelayCommand]
    private async Task OpenDetailsAsync()
    {
        var parameters = new Dictionary<string, object>
        {
            {"ItemId", 42},
            {"Title", "Sensor Data"}
        };

        await Shell.Current.GoToAsync(nameof(DetailsPage), parameters);
    }

    [RelayCommand]
    private async Task DeleteOrder(Order? order)
    {
        if (order is null) return;

        var result = await db.DeleteOrder(order);
        if (result > 0) await LoadOrdersAsync();
    }
}
