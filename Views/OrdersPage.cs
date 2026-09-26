using OrderManagerMaui.Components;
using OrderManagerMaui.ViewModels;

namespace OrderManagerMaui.Views;

public class OrdersPage : ContentPage
{
    public OrdersPage(OrdersViewModel viewModel)
    {
        BindingContext = viewModel;

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
            SelectableItemsView.SelectionChangedCommandProperty, nameof(OrdersViewModel.OpenDetailsCommand)
        );

        // Root
        Content = collectionView;
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
