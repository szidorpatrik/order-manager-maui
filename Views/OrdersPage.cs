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
            SelectionMode = SelectionMode.Single,
            Footer = new BoxView
            {
                HeightRequest = 88,
                Color = Colors.Transparent
            }
        };
        collectionView.SetBinding(ItemsView.ItemsSourceProperty, nameof(OrdersViewModel.Orders));
        collectionView.SetBinding(
            SelectableItemsView.SelectedItemProperty,
            nameof(OrdersViewModel.SelectedOrder)
        );
        collectionView.SetBinding(
            SelectableItemsView.SelectionChangedCommandProperty,
            nameof(OrdersViewModel.NavigateToDetailsCommand)
        );

        var fabButton = new FabButton("add.svg", Color.FromArgb("#4F46E5"));
        fabButton.SetBinding(ImageButton.CommandProperty, nameof(OrdersViewModel.NavigateToCreateOrderCommand));

        var rootGrid = new Grid { Margin = new Thickness(0, 6, 0, 0) };
        rootGrid.Children.Add(collectionView);
        rootGrid.Children.Add(fabButton);

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
