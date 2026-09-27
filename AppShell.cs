using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Core;
using OrderManagerMaui.Theme;
using OrderManagerMaui.Views;

namespace OrderManagerMaui;

public class AppShell : Shell
{
    public AppShell()
    {
        // Status bar
        Behaviors.Add(new StatusBarBehavior()
        {
            StatusBarColor = AppColor.Primary.ToColor(),
            StatusBarStyle = StatusBarStyle.LightContent
        });

        // Top bar
        SetBackgroundColor(this, AppColor.Surface.ToColor());
        SetTitleColor(this, AppColor.TextPrimary.ToColor());
        SetForegroundColor(this, AppColor.TextPrimary.ToColor());

        // Root page
        Items.Add(new ShellContent
        {
            Title = "Orders",
            Route = nameof(OrdersPage),
            ContentTemplate = new DataTemplate(typeof(OrdersPage))
        });

        // Sub routes
        Routing.RegisterRoute(nameof(OrderCreatePage), typeof(OrderCreatePage));
        Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
    }
}
