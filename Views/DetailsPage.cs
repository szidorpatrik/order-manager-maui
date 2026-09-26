using Microsoft.Maui.Controls.Shapes;
using OrderManagerMaui.Models;
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

        var customerNameLabel = new Label
        {
            FontSize = 22,
            FontAttributes = FontAttributes.Bold,
            TextColor = Color.FromArgb("#0F172A")
        };
        customerNameLabel.SetBinding(
            Label.TextProperty,
            $"{nameof(DetailsViewModel.Order)}.{nameof(Order.CustomerName)}"
        );

        var addressLabel = new Label
        {
            FontSize = 15,
            TextColor = Color.FromArgb("#475569")
        };
        addressLabel.SetBinding(
            Label.TextProperty,
            $"{nameof(DetailsViewModel.Order)}.{nameof(Order.Address)}"
        );

        var amountLabel = new Label
        {
            FontSize = 20,
            FontAttributes = FontAttributes.Bold,
            TextColor = Color.FromArgb("#16A34A")
        };
        amountLabel.SetBinding(
            Label.TextProperty,
            new Binding(
                $"{nameof(DetailsViewModel.Order)}.{nameof(Order.TotalAmountString)}",
                stringFormat: "{0} Ft"
            )
        );

        var dateLabel = new Label
        {
            FontSize = 13,
            TextColor = Color.FromArgb("#94A3B8")
        };
        dateLabel.SetBinding(Label.TextProperty, new Binding(
            $"{nameof(DetailsViewModel.Order)}.{nameof(Order.CreatedAt)}",
            stringFormat: "Created: {0:yyyy-MM-dd HH:mm}"
        ));

        var card = new Border
        {
            StrokeShape = new RoundRectangle { CornerRadius = 14 },
            Stroke = Color.FromArgb("#E2E8F0"),
            StrokeThickness = 1,
            BackgroundColor = Colors.White,
            Padding = new Thickness(20),
            Margin = new Thickness(16, 20),
            Shadow = new Shadow
            {
                Brush = Colors.Black,
                Offset = new Point(0, 3),
                Radius = 6,
                Opacity = 0.08f
            },
            Content = new VerticalStackLayout
            {
                Spacing = 14,
                Children =
                {
                    customerNameLabel,
                    new BoxView
                    {
                        HeightRequest = 1,
                        Color = Color.FromArgb("#F1F5F9")
                    },
                    new VerticalStackLayout
                    {
                        Spacing = 4,
                        Children =
                        {
                            new Label
                            {
                                Text = "Delivery Address:",
                                FontSize = 12,
                                TextColor = Color.FromArgb("#94A3B8")
                            },
                            addressLabel,
                        }
                    },
                    new VerticalStackLayout
                    {
                        Spacing = 4,
                        Children =
                        {
                            new Label
                            {
                                Text = "Total Amount:",
                                FontSize = 12,
                                TextColor = Color.FromArgb("#94A3B8")
                            },
                            amountLabel
                        }
                    },
                    dateLabel
                }
            }
        };

        Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Spacing = 10,
                Children = { card }
            }
        };
    }
}
