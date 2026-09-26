using CommunityToolkit.Mvvm.ComponentModel;
using OrderManagerMaui.Services;

namespace OrderManagerMaui.ViewModels;

public partial class OrderCreateViewModel(DatabaseService db) : ObservableObject
{
    public string PageTitle => "New Order";

    [ObservableProperty]
    public partial string CustomerName { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Address { get; set; } = string.Empty;

    [ObservableProperty]
    public partial float TotalAmount { get; set; }
}
