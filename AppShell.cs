using OrderManagerMaui.Views;

namespace OrderManagerMaui;

public class AppShell : Shell
{
    public AppShell()
    {
        // Root page
        Items.Add(new ShellContent
        {
            Title = "Orders",
            Route = nameof(OrdersPage),
            ContentTemplate = new DataTemplate(typeof(OrdersPage))
        });
        
        // Sub routes
        Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
    }
}
