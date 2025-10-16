namespace Solution.DesktopApp;

public partial class AppShell : Shell
{
    public AppShellViewModel ViewModel => this.BindingContext as AppShellViewModel;

    public AppShell(AppShellViewModel viewModel)
    {
        this.BindingContext = viewModel;

        InitializeComponent();

        ConfigureShellNavigation();
    }

    private static void ConfigureShellNavigation()
    {
        Routing.RegisterRoute(MainView.Name, typeof(MainView));
        Routing.RegisterRoute(CreateOrEditArtistView.Name, typeof(CreateOrEditArtistView));
        Routing.RegisterRoute(CreateOrEditAlbumView.Name, typeof(CreateOrEditAlbumView));
        Routing.RegisterRoute(CreateOrEditSongView.Name, typeof(CreateOrEditSongView));
        Routing.RegisterRoute(SongListView.Name, typeof(SongListView));
        Routing.RegisterRoute(AlbumListView.Name, typeof(AlbumListView));
        Routing.RegisterRoute(ArtistListView.Name, typeof(ArtistListView));
    }
}
