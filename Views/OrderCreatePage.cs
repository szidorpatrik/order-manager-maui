using Microsoft.Maui.Controls.Shapes;
using OrderManagerMaui.Components;
using OrderManagerMaui.Theme;
using OrderManagerMaui.ViewModels;

namespace OrderManagerMaui.Views;

public class OrderCreatePage : ContentPage
{
    public OrderCreatePage(OrderCreateViewModel viewModel)
    {
        BindingContext = viewModel;
        this.SetBinding(TitleProperty, nameof(OrderCreateViewModel.PageTitle));
        BackgroundColor = AppColor.Background.ToColor();

        var customerNameEntry = new Entry
        {
            Placeholder = "Customer Name",
            FontSize = 15,
            ClearButtonVisibility = ClearButtonVisibility.WhileEditing
        };
        customerNameEntry.SetBinding(Entry.TextProperty, nameof(OrderCreateViewModel.CustomerName));

        var addressEntry = new Entry
        {
            Placeholder = "Delivery Address",
            FontSize = 15,
            ClearButtonVisibility = ClearButtonVisibility.WhileEditing
        };
        addressEntry.SetBinding(Entry.TextProperty, nameof(OrderCreateViewModel.Address));

        var amountEntry = new Entry
        {
            Placeholder = "Total Amount (Ft)",
            FontSize = 15,
            Keyboard = Keyboard.Numeric,
            ClearButtonVisibility = ClearButtonVisibility.WhileEditing
        };
        amountEntry.SetBinding(Entry.TextProperty, nameof(OrderCreateViewModel.TotalAmount));

        var formCard = new Border
        {
            StrokeShape = new RoundRectangle { CornerRadius = 14 },
            Stroke = AppColor.Border.ToColor(),
            StrokeThickness = 1,
            BackgroundColor = AppColor.Surface.ToColor(),
            Padding = new Thickness(20),
            Margin = new Thickness(16, 12),
            Shadow = new Shadow
            {
                Brush = Colors.Black,
                Offset = new Point(0, 3),
                Radius = 6,
                Opacity = 0.08f
            },
            Content = new VerticalStackLayout
            {
                Spacing = 16,
                Children =
                {
                    new Label { Text = "Customer Name", FontSize = 12, TextColor = AppColor.TextSubtle.ToColor() },
                    customerNameEntry,
                    new BoxView { HeightRequest = 1, Color = AppColor.Divider.ToColor() },
                    new Label { Text = "Delivery Address", FontSize = 12, TextColor = AppColor.TextSubtle.ToColor() },
                    addressEntry,
                    new BoxView { HeightRequest = 1, Color = AppColor.Divider.ToColor() },
                    new Label { Text = "Total Amount (Ft)", FontSize = 12, TextColor = AppColor.TextSubtle.ToColor() },
                    amountEntry
                }
            }
        };

        var fabButton = new FabButton("save.svg", AppColor.Success);
        fabButton.SetBinding(ImageButton.CommandProperty, nameof(OrderCreateViewModel.SaveOrderCommand));

        var rootGrid = new Grid
        {
            RowDefinitions =
            {
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Star },
            }
        };
        rootGrid.Add(formCard, 0, 0);
        rootGrid.Add(fabButton, 0, 1);

        Content = rootGrid;
    }
}
