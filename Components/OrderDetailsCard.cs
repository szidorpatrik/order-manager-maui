using Microsoft.Maui.Controls.Shapes;
using OrderManagerMaui.Models;

namespace OrderManagerMaui.Components;

public class OrderDetailsCard : Border
{
    public OrderDetailsCard()
    {
        StrokeShape = new RoundRectangle { CornerRadius = 14 };
        Stroke = Color.FromArgb("#E2E8F0");
        StrokeThickness = 1;
        BackgroundColor = Colors.White;
        Padding = new Thickness(20);
        Margin = new Thickness(16, 20);

        Shadow = new Shadow
        {
            Brush = Colors.Black,
            Offset = new Point(0, 3),
            Radius = 6,
            Opacity = 0.08f
        };

        var customerNameLabel = new Label
        {
            FontSize = 22,
            FontAttributes = FontAttributes.Bold,
            TextColor = Color.FromArgb("#0F172A")
        };
        customerNameLabel.SetBinding(Label.TextProperty, nameof(Order.CustomerName));

        var addressLabel = new Label
        {
            FontSize = 15,
            TextColor = Color.FromArgb("#475569")
        };
        addressLabel.SetBinding(Label.TextProperty, nameof(Order.Address));

        var amountLabel = new Label
        {
            FontSize = 20,
            FontAttributes = FontAttributes.Bold,
            TextColor = Color.FromArgb("#16A34A")
        };
        amountLabel.SetBinding(
            Label.TextProperty,
            new Binding(nameof(Order.TotalAmountString), stringFormat: "{0} Ft")
        );

        var dateLabel = new Label
        {
            FontSize = 13,
            TextColor = Color.FromArgb("#94A3B8")
        };
        dateLabel.SetBinding(
            Label.TextProperty,
            new Binding(nameof(Order.CreatedAt), stringFormat: "Created: {0:yyyy-MM-dd HH:mm}")
        );

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
                        addressLabel
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
        };
    }
}
