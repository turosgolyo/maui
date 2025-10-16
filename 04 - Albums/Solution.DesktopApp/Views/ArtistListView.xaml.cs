namespace Solution.DesktopApp.Views;

public partial class ArtistListView : ContentPage
{
	public ArtistListViewModel ViewModel => this.BindingContext as ArtistListViewModel;
	public static string Name => nameof(ArtistListView);
	public ArtistListView(ArtistListViewModel viewModel)
	{
		this.BindingContext = viewModel;

		InitializeComponent();
	}
}