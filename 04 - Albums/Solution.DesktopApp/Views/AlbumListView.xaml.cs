namespace Solution.DesktopApp.Views;

public partial class AlbumListView : ContentPage
{
	public AlbumListViewModel ViewModel => this.BindingContext as AlbumListViewModel;
	public static string Name => nameof(AlbumListView);
	public AlbumListView(AlbumListViewModel viewModel)
	{
		this.BindingContext = viewModel;

		InitializeComponent();
	}
}