using OrderManagerMaui.ViewModels;

namespace OrderManagerMaui.Views;

public class OrderCreatePage : ContentPage
{
    public OrderCreatePage(OrderCreateViewModel viewModel)
    {
        BindingContext = viewModel;
        this.SetBinding(TitleProperty, nameof(OrderCreateViewModel.PageTitle));
        
    }
}
