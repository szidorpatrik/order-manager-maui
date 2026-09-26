namespace OrderManagerMaui.Views;

public class LoadingPage : ContentPage
{
    public LoadingPage()
    {
        Content = new VerticalStackLayout
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            Spacing = 10,
            Children =
            {
                new ActivityIndicator
                {
                    IsRunning = true
                },
                new Label
                {
                    Text = "Loading orders...",
                    HorizontalTextAlignment = TextAlignment.Center
                }
            }
        };
    }
}
