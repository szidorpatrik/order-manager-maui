using OrderManagerMaui.Components;
using OrderManagerMaui.ViewModels;

namespace OrderManagerMaui.Views;

public class DetailsPage : ContentPage
{
    public DetailsPage(DetailsViewModel viewModel)
    {
        BindingContext = viewModel;
        this.SetBinding(TitleProperty, nameof(DetailsViewModel.PageTitle));
        BackgroundColor = Color.FromArgb("#F8FAFC");

        var copyToolbarItem = new ToolbarItem
        {
            IconImageSource = "content_copy.svg",
            Text = "Copy",
            Priority = 0,
            Order = ToolbarItemOrder.Primary,
        };
        copyToolbarItem.SetBinding(
            MenuItem.CommandProperty,
            nameof(DetailsViewModel.CopyOrderToClipboardCommand)
        );
        ToolbarItems.Add(copyToolbarItem);

        var shareToolbarItem = new ToolbarItem
        {
            IconImageSource = "share.svg",
            Text = "Share",
            Priority = 1,
            Order = ToolbarItemOrder.Primary,
        };
        shareToolbarItem.SetBinding(
            MenuItem.CommandProperty,
            nameof(DetailsViewModel.ShareOrderCommand)
        );
        ToolbarItems.Add(shareToolbarItem);

        var detailsCard = new OrderDetailsCard();
        detailsCard.SetBinding(BindingContextProperty, nameof(DetailsViewModel.Order));

        var fabButton = new ImageButton
        {
            Source = "edit.svg",
            BackgroundColor = Colors.Goldenrod,
            CornerRadius = 28,
            WidthRequest = 56,
            HeightRequest = 56,
            Padding = 14,
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
