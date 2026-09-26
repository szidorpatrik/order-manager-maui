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

        var editToolbarItem = new ToolbarItem
        {
            IconImageSource = "edit.svg",
            Text = "Edit",
            Priority = 1,
            Order = ToolbarItemOrder.Primary,
        };
        editToolbarItem.SetBinding(
            MenuItem.CommandProperty,
            nameof(DetailsViewModel.GoEditCommand)
        );
        ToolbarItems.Add(editToolbarItem);

        var detailsCard = new OrderDetailsCard();
        detailsCard.SetBinding(BindingContextProperty, nameof(DetailsViewModel.Order));

        Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Spacing = 10,
                Children = { detailsCard }
            }
        };
    }
}
