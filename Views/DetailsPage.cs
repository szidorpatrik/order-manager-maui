using OrderManagerMaui.Components;
using OrderManagerMaui.Theme;
using OrderManagerMaui.ViewModels;

namespace OrderManagerMaui.Views;

public class DetailsPage : ContentPage
{
    public DetailsPage(DetailsViewModel viewModel)
    {
        BindingContext = viewModel;
        this.SetBinding(TitleProperty, nameof(DetailsViewModel.PageTitle));
        BackgroundColor = AppColor.Background.ToColor();

        var copyToolbarItem = new ToolbarItem
        {
            IconImageSource = "content_copy.svg",
            Text = "Copy",
            Priority = 0,
            Order = ToolbarItemOrder.Primary,
        };
        copyToolbarItem.SetBinding(MenuItem.CommandProperty, nameof(DetailsViewModel.CopyOrderToClipboardCommand));
        ToolbarItems.Add(copyToolbarItem);

        var shareToolbarItem = new ToolbarItem
        {
            IconImageSource = "share.svg",
            Text = "Share",
            Priority = 1,
            Order = ToolbarItemOrder.Primary,
        };
        shareToolbarItem.SetBinding(MenuItem.CommandProperty, nameof(DetailsViewModel.ShareOrderCommand));
        ToolbarItems.Add(shareToolbarItem);

        var detailsCard = new OrderDetailsCard();
        detailsCard.SetBinding(BindingContextProperty, nameof(DetailsViewModel.Order));

        var contentStack = new VerticalStackLayout
        {
            Spacing = 10,
            Children =
            {
                detailsCard,
                new BoxView { HeightRequest = 220, Color = Colors.Transparent }
            }
        };

        var scrollView = new ScrollView { Content = contentStack };

        var mapFab = new FabButton("map.svg", AppColor.Secondary) { Margin = new Thickness(0) };
        mapFab.SetBinding(ImageButton.CommandProperty, nameof(DetailsViewModel.OpenMapCommand));
        mapFab.SetBinding(IsVisibleProperty, nameof(DetailsViewModel.HasCoordinates));
        mapFab.SetBinding(IsEnabledProperty, nameof(DetailsViewModel.IsNotBusy));

        var deliverFab = new FabButton("check.svg", AppColor.Success) { Margin = new Thickness(0) };
        deliverFab.SetBinding(ImageButton.CommandProperty, nameof(DetailsViewModel.DeliverOrderCommand));
        deliverFab.SetBinding(IsVisibleProperty, nameof(DetailsViewModel.CanDeliver));
        deliverFab.SetBinding(IsEnabledProperty, nameof(DetailsViewModel.IsNotBusy));

        var editFab = new FabButton("edit.svg", AppColor.Warning) { Margin = new Thickness(0) };
        editFab.SetBinding(ImageButton.CommandProperty, nameof(DetailsViewModel.GoEditCommand));
        editFab.SetBinding(IsVisibleProperty, nameof(DetailsViewModel.IsReadonly));
        editFab.SetBinding(IsEnabledProperty, nameof(DetailsViewModel.IsNotBusy));

        var fabStack = new VerticalStackLayout
        {
            Spacing = 12,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.End,
            Margin = new Thickness(0, 0, 20, 20),
            Children = { mapFab, editFab, deliverFab }
        };
        fabStack.SetBinding(InputTransparentProperty, nameof(DetailsViewModel.IsBusy));

        var rootGrid = new Grid();
        rootGrid.Children.Add(scrollView);
        rootGrid.Children.Add(fabStack);

        Content = rootGrid;
    }

    protected override async void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            if (BindingContext is DetailsViewModel viewModel)
            {
                await viewModel.RefreshCommand.ExecuteAsync(null);
            }
        }
        catch (Exception)
        {
            // continue
        }
    }
}
