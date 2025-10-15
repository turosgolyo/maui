namespace Solution.DesktopApp.Views;

public partial class CreateOrEditSongView : ContentPage
{
	public CreateOrEditSongViewModel ViewModel => this.BindingContext as CreateOrEditSongViewModel;
	public static string Name => nameof(CreateOrEditSongView);

    public CreateOrEditSongView(CreateOrEditSongViewModel viewModel)
	{
		this.BindingContext = viewModel;

        InitializeComponent();
	}
}