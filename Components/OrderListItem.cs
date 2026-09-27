using Microsoft.Maui.Controls.Shapes;
using OrderManagerMaui.Models;
using OrderManagerMaui.Theme;
using OrderManagerMaui.ViewModels;

namespace OrderManagerMaui.Components;

public class OrderListItem : Border
{
    public OrderListItem()
    {
        StrokeShape = new RoundRectangle { CornerRadius = 12 };
        Stroke = AppColor.Border.ToColor();
        StrokeThickness = 1;
        BackgroundColor = AppColor.Surface.ToColor();
        Padding = new Thickness(16, 12);
        Margin = new Thickness(14, 6);

        Shadow = new Shadow
        {
            Brush = Colors.Black,
            Offset = new Point(0, 2),
            Radius = 4,
            Opacity = 0.06f
        };

        var customerNameLabel = new Label
        {
            FontSize = 16,
            FontAttributes = FontAttributes.Bold,
            TextColor = AppColor.TextPrimary.ToColor(),
            LineBreakMode = LineBreakMode.TailTruncation
        };
        customerNameLabel.SetBinding(Label.TextProperty, nameof(Order.CustomerName));

        var addressLabel = new Label
        {
            FontSize = 13,
            TextColor = AppColor.TextMuted.ToColor(),
            LineBreakMode = LineBreakMode.TailTruncation
        };
        addressLabel.SetBinding(Label.TextProperty, nameof(Order.Address));

        var textStack = new VerticalStackLayout
        {
            Spacing = 3,
            VerticalOptions = LayoutOptions.Center,
            Children = { customerNameLabel, addressLabel }
        };

        var amountLabel = new Label
        {
            FontSize = 15,
            FontAttributes = FontAttributes.Bold,
            TextColor = AppColor.Success.ToColor(),
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center
        };
        amountLabel.SetBinding(Label.TextProperty,
            new Binding(nameof(Order.TotalAmountString), stringFormat: "{0} Ft")
        );

        var deleteButton = new Button
        {
            Text = "✕",
            TextColor = AppColor.Danger.ToColor(),
            BackgroundColor = AppColor.DangerSubtle.ToColor(),
            CornerRadius = 16,
            FontSize = 13,
            FontAttributes = FontAttributes.Bold,
            WidthRequest = 32,
            HeightRequest = 32,
            Padding = 0,
            VerticalOptions = LayoutOptions.Center
        };

        deleteButton.SetBinding(
            Button.CommandProperty,
            new Binding(
                nameof(OrdersViewModel.DeleteOrderCommand),
                source: new RelativeBindingSource(
                    RelativeBindingSourceMode.FindAncestorBindingContext,
                    typeof(OrdersViewModel)
                )
            )
        );
        deleteButton.SetBinding(Button.CommandParameterProperty, ".");

        var grid = new Grid
        {
            ColumnSpacing = 12,
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = GridLength.Auto }
            }
        };

        grid.Add(textStack, 0);
        grid.Add(amountLabel, 1);
        grid.Add(deleteButton, 2);

        Content = grid;
    }
}
