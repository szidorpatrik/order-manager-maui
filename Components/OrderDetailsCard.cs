using System.Globalization;
using Microsoft.Maui.Controls.Shapes;
using OrderManagerMaui.Models;
using OrderManagerMaui.ViewModels;

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
        Margin = new Thickness(16, 12);

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

        var statusLabel = new Label
        {
            FontSize = 13,
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center
        };
        statusLabel.SetBinding(Label.TextProperty,
            new Binding(
                nameof(Order.IsDelivered),
                converter: new StatusTextConverter()
            )
        );
        statusLabel.SetBinding(Label.TextColorProperty,
            new Binding(
                nameof(Order.IsDelivered),
                converter: new StatusTextColorConverter()
            )
        );

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
        amountLabel.SetBinding(Label.TextProperty, new Binding(
            nameof(Order.TotalAmountString),
            stringFormat: "{0} Ft"
        ));

        var locationLabel = new Label
        {
            FontSize = 13,
            TextColor = Color.FromArgb("#64748B")
        };
        locationLabel.SetBinding(Label.TextProperty, new Binding(
            nameof(Order.Latitude),
            converter: new LocationTextConverter()
        ));

        var dateLabel = new Label
        {
            FontSize = 12,
            TextColor = Color.FromArgb("#94A3B8")
        };
        dateLabel.SetBinding(Label.TextProperty, new Binding(
            nameof(Order.CreatedAt),
            stringFormat: "Created: {0:yyyy-MM-dd HH:mm}"
        ));

        Content = new VerticalStackLayout
        {
            Spacing = 12,
            Children =
            {
                new HorizontalStackLayout
                {
                    Spacing = 8,
                    Children = { customerNameLabel, statusLabel }
                },
                new BoxView { HeightRequest = 1, Color = Color.FromArgb("#F1F5F9") },
                new VerticalStackLayout
                {
                    Spacing = 4,
                    Children =
                    {
                        new Label { Text = "Delivery Address:", FontSize = 12, TextColor = Color.FromArgb("#94A3B8") },
                        addressLabel
                    }
                },
                new VerticalStackLayout
                {
                    Spacing = 4,
                    Children =
                    {
                        new Label { Text = "Coordinates:", FontSize = 12, TextColor = Color.FromArgb("#94A3B8") },
                        locationLabel
                    }
                },
                new VerticalStackLayout
                {
                    Spacing = 4,
                    Children =
                    {
                        new Label { Text = "Total Amount:", FontSize = 12, TextColor = Color.FromArgb("#94A3B8") },
                        amountLabel
                    }
                },
                dateLabel
            }
        };
    }
}

public class StatusTextConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is true ? "✓ Delivered" : "● Pending";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class StatusTextColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is true ? Color.FromArgb("#16A34A") : Colors.Goldenrod;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class LocationTextConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is double lat && lat != 0 ? $"{lat:F8}° N" : "Not resolved yet";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter,
        CultureInfo culture) =>
        throw new NotImplementedException();
}
