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
    private async Task CopyOrderToClipboard()
    {
        
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}
