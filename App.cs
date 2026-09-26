using OrderManagerMaui.Services;
using OrderManagerMaui.Views;

namespace OrderManagerMaui;

public class App(AppShell appShell, DatabaseService dbService) : Application
{
    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(new LoadingPage());

        window.Created += async (_, _) =>
        {
            await dbService.InitAsync();

            // Small delay to fix UI flashing
            await Task.Delay(125);

            window.Page = appShell;
        };

        return window;
    }
}
