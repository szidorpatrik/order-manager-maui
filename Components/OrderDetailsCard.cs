using System.Globalization;
using Microsoft.Maui.Controls.Shapes;
using OrderManagerMaui.Models;
using OrderManagerMaui.Theme;
using OrderManagerMaui.ViewModels;

namespace OrderManagerMaui.Components;

public class OrderDetailsCard : Border
{
    public OrderDetailsCard()
    {
        StrokeShape = new RoundRectangle { CornerRadius = 14 };
        Stroke = AppColor.Border.ToColor();
        StrokeThickness = 1;
        BackgroundColor = AppColor.Surface.ToColor();
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
            TextColor = AppColor.TextPrimary.ToColor()
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
            new Binding(nameof(Order.IsDelivered), converter: new StatusTextConverter())
        );
        statusLabel.SetBinding(Label.TextColorProperty,
            new Binding(nameof(Order.IsDelivered), converter: new StatusTextColorConverter())
        );

        var addressLabel = new Label
        {
            FontSize = 15,
            TextColor = AppColor.TextSecondary.ToColor()
        };
        addressLabel.SetBinding(Label.TextProperty, nameof(Order.Address));

        var amountLabel = new Label
        {
            FontSize = 20,
            FontAttributes = FontAttributes.Bold,
            TextColor = AppColor.Success.ToColor()
        };
        amountLabel.SetBinding(Label.TextProperty, new Binding(
            nameof(Order.TotalAmountString),
            stringFormat: "{0} Ft"
        ));

        var locationLabel = new Label
        {
            FontSize = 13,
            TextColor = AppColor.TextMuted.ToColor()
        };
        locationLabel.SetBinding(Label.TextProperty, new Binding(
            nameof(Order.Latitude),
            converter: new LocationTextConverter()
        ));

        var dateLabel = new Label
        {
            FontSize = 12,
            TextColor = AppColor.TextSubtle.ToColor()
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
                new BoxView { HeightRequest = 1, Color = AppColor.Divider.ToColor() },
                new VerticalStackLayout
                {
                    Spacing = 4,
                    Children =
                    {
                        new Label
                        {
                            Text = "Delivery Address:", FontSize = 12, TextColor = AppColor.TextSubtle.ToColor()
                        },
                        addressLabel
                    }
                },
                new VerticalStackLayout
                {
                    Spacing = 4,
                    Children =
                    {
                        new Label { Text = "Coordinates:", FontSize = 12, TextColor = AppColor.TextSubtle.ToColor() },
                        locationLabel
                    }
                },
                new VerticalStackLayout
                {
                    Spacing = 4,
                    Children =
                    {
                        new Label { Text = "Total Amount:", FontSize = 12, TextColor = AppColor.TextSubtle.ToColor() },
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
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is true ? "✓ Delivered" : "● Pending";

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}

public class StatusTextColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is true ? AppColor.Success.ToColor() : AppColor.Warning.ToColor();

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}

public class LocationTextConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is double lat && lat != 0 ? $"{lat:F8}° N" : "Not resolved yet";

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}
