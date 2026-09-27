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

        var fabButton = new FabButton("edit.svg", AppColor.Warning);
        fabButton.SetBinding(ImageButton.CommandProperty, nameof(DetailsViewModel.GoEditCommand));
        fabButton.SetBinding(ImageButton.CommandParameterProperty, nameof(DetailsViewModel.Order));

        var rootGrid = new Grid
        {
            RowDefinitions =
            {
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Star },
            }
        };
        rootGrid.Add(detailsCard, 0, 0);
        rootGrid.Add(fabButton, 0, 1);

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
