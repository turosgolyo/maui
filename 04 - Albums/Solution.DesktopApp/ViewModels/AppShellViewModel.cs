
namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class AppShellViewModel
{
    public IAsyncRelayCommand ExitCommand => new AsyncRelayCommand(OnExitAsync);

    public IAsyncRelayCommand AddArtistCommand => new AsyncRelayCommand(OnAddArtistAsync);
    public IAsyncRelayCommand AddAlbumCommand => new AsyncRelayCommand(OnAddAlbumAsync);
    public IAsyncRelayCommand AddSongCommand => new AsyncRelayCommand(OnAddSongAsync);
    public IAsyncRelayCommand ViewSongCommand => new AsyncRelayCommand(OnListSongs);
    public IAsyncRelayCommand ViewAlbumCommand => new AsyncRelayCommand(OnListAlbums);
    public IAsyncRelayCommand ViewArtistCommand => new AsyncRelayCommand(OnListArtists);

    private async Task OnAddArtistAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(nameof(CreateOrEditArtistView));
    }
    private async Task OnAddAlbumAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(nameof(CreateOrEditAlbumView));
    }
    private async Task OnAddSongAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(nameof(CreateOrEditSongView));
    }
    private async Task OnListSongs()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(nameof(SongListView));
    }
    private async Task OnListAlbums()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(nameof(AlbumListView));
    }
    private async Task OnListArtists()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(nameof(ArtistListView));
    }

    private async Task OnExitAsync() => Application.Current.Quit();

}
