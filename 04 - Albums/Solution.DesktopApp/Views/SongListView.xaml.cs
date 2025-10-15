namespace Solution.DesktopApp.Views;

public partial class SongListView : ContentPage
{
	public SongListViewModel ViewModel => this.BindingContext as SongListViewModel;
	public static string Name => nameof(SongListView);
	public SongListView(SongListViewModel viewModel)
	{
		this.BindingContext = viewModel;

		InitializeComponent();
	}
}