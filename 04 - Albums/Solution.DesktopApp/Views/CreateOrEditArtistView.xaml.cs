namespace Solution.DesktopApp.Views;

public partial class CreateOrEditArtistView : ContentPage
{
	public CreateOrEditArtistViewModel ViewModel => this.BindingContext as CreateOrEditArtistViewModel;
	public static string Name => nameof(CreateOrEditArtistView);

    public CreateOrEditArtistView(CreateOrEditArtistViewModel viewModel)
	{
		this.BindingContext = viewModel;

        InitializeComponent();
	}
}