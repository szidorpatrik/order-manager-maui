using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OrderManagerMaui.Models;
using OrderManagerMaui.Services;

namespace OrderManagerMaui.ViewModels;

[QueryProperty(nameof(Order), nameof(Order))]
public partial class OrderCreateViewModel(DatabaseService db) : ObservableObject
{
    public string PageTitle => Order.Id > 0 ? $"Edit Order #{Order.Id}" : "New Order";

    [ObservableProperty]
    public partial Order Order { get; set; } = new();

    [ObservableProperty]
    public partial string CustomerName { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Address { get; set; } = string.Empty;

    [ObservableProperty]
    public partial float TotalAmount { get; set; }

    partial void OnOrderChanged(Order? value)
    {
        if (value is null) return;

        CustomerName = value.CustomerName;
        Address = value.Address;
        TotalAmount = value.TotalAmount;

        OnPropertyChanged(nameof(PageTitle));
    }

    [RelayCommand]
    private async Task SaveOrderAsync()
    {
        IToast toast;
        if (string.IsNullOrWhiteSpace(CustomerName))
        {
            toast = Toast.Make("Customer name is required");
            await toast.Show();
            return;
        }

        if (string.IsNullOrWhiteSpace(Address))
        {
            toast = Toast.Make("Address is required");
            await toast.Show();
            return;
        }

        if (TotalAmount <= 0)
        {
            toast = Toast.Make("Total amount must be greater than 0");
            await toast.Show();
            return;
        }

        if (Order.Id > 0)
        {
            Order.CustomerName = CustomerName.Trim();
            Order.Address = Address.Trim();
            Order.TotalAmount = TotalAmount;

            await db.SaveOrder(Order);
        }
        else
        {
            var newOrder = new Order
            {
                CustomerName = CustomerName.Trim(),
                Address = Address.Trim(),
                TotalAmount = TotalAmount,
                CreatedAt = DateTime.Now
            };

            await db.SaveOrder(newOrder);
        }

        await Shell.Current.GoToAsync("..");
    }
}
