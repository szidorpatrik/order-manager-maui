using OrderManagerMaui.Components;
using OrderManagerMaui.ViewModels;

namespace OrderManagerMaui.Views;

public class OrdersPage : ContentPage
{
    public OrdersPage(OrdersViewModel viewModel)
    {
        BindingContext = viewModel;
        BackgroundColor = Color.FromArgb("#F8FAFC");

        var collectionView = new CollectionView
        {
            ItemTemplate = new DataTemplate(typeof(OrderListItem)),
            SelectionMode = SelectionMode.Single
        };
        collectionView.SetBinding(ItemsView.ItemsSourceProperty, nameof(OrdersViewModel.Orders));
        collectionView.SetBinding(
            SelectableItemsView.SelectedItemProperty,
            nameof(OrdersViewModel.SelectedOrder)
        );
        collectionView.SetBinding(
            SelectableItemsView.SelectionChangedCommandProperty, nameof(OrdersViewModel.NavigateToDetailsCommand)
        );

        var fabButton = new Button
        {
            Text = "+",
            FontSize = 26,
            TextColor = Colors.White,
            CornerRadius = 28,
            WidthRequest = 56,
            HeightRequest = 56,
            Padding = 0,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.End,
            Margin = new Thickness(0, 0, 20, 20),
            Shadow = new Shadow
            {
                Brush = Colors.Black,
                Offset = new Point(0, 4),
                Radius = 6,
                Opacity = 0.3f
            }
        };
        fabButton.SetBinding(Button.CommandProperty, nameof(OrdersViewModel.NavigateToCreateOrderCommand));

        var rootGrid = new Grid { Margin = new Thickness(0, 6, 0, 0) };
        rootGrid.Children.Add(collectionView);
        rootGrid.Children.Add(fabButton);

        // Root
        Content = rootGrid;
    }

    protected override async void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            if (BindingContext is OrdersViewModel viewModel)
            {
                await viewModel.LoadOrdersCommand.ExecuteAsync(null);
            }
        }
        catch (Exception)
        {
            // continue
        }
    }
}
