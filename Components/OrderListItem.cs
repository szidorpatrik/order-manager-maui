using OrderManagerMaui.Models;
using OrderManagerMaui.ViewModels;

namespace OrderManagerMaui.Components;

public class OrderListItem : Border
{
    public OrderListItem()
    {
        var customerNameLabel = new Label
        {
            FontSize = 16,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Black
        };
        customerNameLabel.SetBinding(Label.TextProperty, nameof(Order.CustomerName));

        var addressLabel = new Label
        {
            FontSize = 13,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.DimGray
        };
        addressLabel.SetBinding(Label.TextProperty, nameof(Order.Address));

        var amountLabel = new Label
        {
            FontSize = 15,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.SeaGreen,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center
        };
        amountLabel.SetBinding(Label.TextProperty, nameof(Order.TotalAmount));

        var textStack = new VerticalStackLayout
        {
            Spacing = 2,
            VerticalOptions = LayoutOptions.Center,
            Children =
            {
                customerNameLabel,
                addressLabel,
            }
        };

        var deleteButton = new Button
        {
            Text = "✕",
            TextColor = Colors.Crimson,
            BackgroundColor = Colors.Transparent,
            FontSize = 16,
            FontAttributes = FontAttributes.Bold,
            WidthRequest = 36,
            HeightRequest = 36,
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
            ColumnSpacing = 10,
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

        // Root
        Content = grid;
    }
}
