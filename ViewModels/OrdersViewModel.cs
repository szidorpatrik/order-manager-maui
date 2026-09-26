using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OrderManagerMaui.Services;
using OrderManagerMaui.Views;

namespace OrderManagerMaui.ViewModels;

public partial class OrdersViewModel(DatabaseService db) : ObservableObject
{
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
}
