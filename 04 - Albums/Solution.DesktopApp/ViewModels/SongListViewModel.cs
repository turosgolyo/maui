namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class SongListViewModel(ISongService songService)
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
    private ObservableCollection<SongModel> songs;

    private int page = 1;
    private bool isLoading = false;
    private bool hasNextPage = false;
    private int numberOfSongsInDb = 0;

    private async Task OnAppearingAsync()
    {
        PreviousPageCommand = new Command(async () => await OnPreviousPageAsync(), () => page > 1 && !isLoading);
        NextPageCommand = new Command(async () => await OnNextPageAsync(), () => !isLoading && hasNextPage);

        await LoadSongsAsync();
    }

    private async Task OnDisappearingAsync()
    { }

    private async Task OnPreviousPageAsync()
    {
        if (isLoading) return;

        page = page <= 1 ? 1 : --page;
        await LoadSongsAsync();
    }

    private async Task OnNextPageAsync()
    {
        if (isLoading) return;

        page++;
        await LoadSongsAsync();
    }

    private async Task LoadSongsAsync()
    {
        isLoading = true;

        var result = await songService.GetPagedAsync(page);

        if (result.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Songs not loaded!", "OK");
            return;
        }

        Songs = new ObservableCollection<SongModel>(result.Value.Items);
        numberOfSongsInDb = result.Value.Count;

        hasNextPage = numberOfSongsInDb - (page * 10) > 0;
        isLoading = false;

        ((Command)PreviousPageCommand).ChangeCanExecute();
        ((Command)NextPageCommand).ChangeCanExecute();
    }

    private async Task OnDeleteAsync(int id)
    {
        var result = await songService.DeleteAsync(id);

        var message = result.IsError ? result.FirstError.Description : "Song deleted.";
        var title = result.IsError ? "Error" : "Information";

        if (!result.IsError)
        {
            var song = songs.SingleOrDefault(x => x.Id == id);
            songs.Remove(song);

            if (songs.Count == 0)
            {
                await LoadSongsAsync();
            }
        }

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }
}
