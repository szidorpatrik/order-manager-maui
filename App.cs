using OrderManagerMaui.Services;
using OrderManagerMaui.Views;

namespace OrderManagerMaui;

public class App(AppShell appShell, DatabaseService dbService) : Application
{
    protected override Window CreateWindow(IActivationState? activationState)
    {
        // Loading page until db initializes
        var loadingPage = new ContentPage
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
            }
        };

        var window = new Window(loadingPage);
        window.Created += async (_, _) =>
        {
            await dbService.InitAsync();

            // Small delay to fix flashing
            await Task.Delay(125);

            window.Page = appShell;
        };

        return window;
    }
}
