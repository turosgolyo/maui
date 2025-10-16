namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class AlbumListViewModel(IAlbumService albumService)
{
    #region life cycle commands
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);
    #endregion

    #region paging commands
    public ICommand PreviousPageCommand { get; private set; }
    public ICommand NextPageCommand { get; private set; }
    #endregion

    #region component commands
    public IAsyncRelayCommand DeleteCommand => new AsyncRelayCommand<int>((id) => OnDeleteAsync(id));
    #endregion


    [ObservableProperty]
    private ObservableCollection<AlbumModel> albums;

    private int page = 1;
    private bool isLoading = false;
    private bool hasNextPage = false;
    private int numberOfAlbumsInDb = 0;

    private async Task OnAppearingAsync()
    {
        PreviousPageCommand = new Command(async () => await OnPreviousPageAsync(), () => page > 1 && !isLoading);
        NextPageCommand = new Command(async () => await OnNextPageAsync(), () => !isLoading && hasNextPage);

        await LoadAlbumsAsync();
    }

    private async Task OnDisappearingAsync()
    { }

    private async Task OnPreviousPageAsync()
    {
        if (isLoading) return;

        page = page <= 1 ? 1 : --page;
        await LoadAlbumsAsync();
    }

    private async Task OnNextPageAsync()
    {
        if (isLoading) return;

        page++;
        await LoadAlbumsAsync();
    }

    private async Task LoadAlbumsAsync()
    {
        isLoading = true;

        var result = await albumService.GetPagedAsync(page);

        if (result.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Songs not loaded!", "OK");
            return;
        }

        Albums = new ObservableCollection<AlbumModel>(result.Value.Items);
        numberOfAlbumsInDb = result.Value.Count;

        hasNextPage = numberOfAlbumsInDb - (page * 10) > 0;
        isLoading = false;

        ((Command)PreviousPageCommand).ChangeCanExecute();
        ((Command)NextPageCommand).ChangeCanExecute();
    }

    private async Task OnDeleteAsync(int id)
    {
        var result = await albumService.DeleteAsync(id);

        var message = result.IsError ? result.FirstError.Description : "Album deleted.";
        var title = result.IsError ? "Error" : "Information";

        if (!result.IsError)
        {
            var album = albums.SingleOrDefault(x => x.Id == id);
            albums.Remove(album);

            if (albums.Count == 0)
            {
                await LoadAlbumsAsync();
            }
        }

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }
}
