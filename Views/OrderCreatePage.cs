using Microsoft.Maui.Controls.Shapes;
using OrderManagerMaui.ViewModels;

namespace OrderManagerMaui.Views;

public class OrderCreatePage : ContentPage
{
    public OrderCreatePage(OrderCreateViewModel viewModel)
    {
        BindingContext = viewModel;
        this.SetBinding(TitleProperty, nameof(OrderCreateViewModel.PageTitle));
        BackgroundColor = Color.FromArgb("#F8FAFC");

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
            Stroke = Color.FromArgb("#E2E8F0"),
            StrokeThickness = 1,
            BackgroundColor = Colors.White,
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
                    new Label { Text = "Customer Name", FontSize = 12, TextColor = Color.FromArgb("#94A3B8") },
                    customerNameEntry,
                    new BoxView { HeightRequest = 1, Color = Color.FromArgb("#F1F5F9") },
                    new Label { Text = "Delivery Address", FontSize = 12, TextColor = Color.FromArgb("#94A3B8") },
                    addressEntry,
                    new BoxView { HeightRequest = 1, Color = Color.FromArgb("#F1F5F9") },
                    new Label { Text = "Total Amount (Ft)", FontSize = 12, TextColor = Color.FromArgb("#94A3B8") },
                    amountEntry
                }
            }
        };

        var fabButton = new ImageButton
        {
            Source = "save.svg",
            BackgroundColor = Color.FromArgb("#16A34A"),
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
