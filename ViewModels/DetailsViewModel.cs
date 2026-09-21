using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace OrderManagerMaui.ViewModels;

[QueryProperty(nameof(ItemId), "ItemId")]
[QueryProperty(nameof(PageTitle), "Title")]
public partial class DetailsViewModel : ObservableObject
{
    [ObservableProperty]
    public partial int ItemId { get; set; } = 0;
    
    [ObservableProperty]
    public partial string PageTitle { get; set; } = string.Empty;

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}
