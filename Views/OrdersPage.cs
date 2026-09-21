using OrderManagerMaui.ViewModels;

namespace OrderManagerMaui.Views;

public class OrdersPage : ContentPage
{
    public OrdersPage(OrdersViewModel viewModel)
    {
        BindingContext = viewModel;

        var heading = new Label
        {
            Text = "Pure C# .NET MAUI",
            FontSize = 28,
            HorizontalOptions = LayoutOptions.Center
        };

        var detailsPageButton = new Button
        {
            HorizontalOptions = LayoutOptions.Center,
            Text = "Details"
        };
        detailsPageButton.SetBinding(Button.CommandProperty, nameof(viewModel.OpenDetailsCommand));
        
        
        Content = new VerticalStackLayout
        {
            Spacing = 20,
            VerticalOptions = LayoutOptions.Center,
            Children =
            {
                heading,
                detailsPageButton
            }
        };
    }
}
