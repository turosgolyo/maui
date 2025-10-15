namespace Solution.DesktopApp.Views;

public partial class CreateOrEditAlbumView : ContentPage
{
	public CreateOrEditAlbumViewModel ViewModel => this.BindingContext as CreateOrEditAlbumViewModel;
	public static string Name => nameof(CreateOrEditAlbumView);

    public CreateOrEditAlbumView(CreateOrEditAlbumViewModel viewModel)
	{
		this.BindingContext = viewModel;

        InitializeComponent();
	}
}