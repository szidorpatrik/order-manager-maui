namespace OrderManagerMaui.Components;

public class FabButton : ImageButton
{
    public FabButton(string iconSource, Color backgroundColor)
    {
        Source = iconSource;
        BackgroundColor = backgroundColor;
        CornerRadius = 28;
        WidthRequest = 56;
        HeightRequest = 56;
        Padding = 14;
        HorizontalOptions = LayoutOptions.End;
        VerticalOptions = LayoutOptions.End;
        Margin = new Thickness(0, 0, 20, 20);
        Shadow = new Shadow
        {
            Brush = Colors.Black,
            Offset = new Point(0, 4),
            Radius = 6,
            Opacity = 0.3f
        };
    }
}
