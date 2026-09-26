using OrderManagerMaui.ViewModels;

namespace OrderManagerMaui.Views;

public class DetailsPage : ContentPage
{
    public DetailsPage(DetailsViewModel viewModel)
    {
        BindingContext = viewModel;
        this.SetBinding(TitleProperty, nameof(DetailsViewModel.PageTitle));
        
        var titleLabel = new Label
        {
            VerticalTextAlignment = TextAlignment.Center,
            TextColor = Colors.Aqua,
        };
        titleLabel.SetBinding(Label.TextProperty, nameof(DetailsViewModel.PageTitle));
        
        Content = new VerticalStackLayout
        {
            Children =
            {
                titleLabel
            }
        };
    }
}
