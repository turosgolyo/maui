namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class ArtistListViewModel(IArtistService artistService)
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
    private ObservableCollection<ArtistModel> artists;

    private int page = 1;
    private bool isLoading = false;
    private bool hasNextPage = false;
    private int numberOfArtistsInDb = 0;

    private async Task OnAppearingAsync()
    {
        PreviousPageCommand = new Command(async () => await OnPreviousPageAsync(), () => page > 1 && !isLoading);
        NextPageCommand = new Command(async () => await OnNextPageAsync(), () => !isLoading && hasNextPage);

        await LoadArtistsAsync();
    }

    private async Task OnDisappearingAsync()
    { }

    private async Task OnPreviousPageAsync()
    {
        if (isLoading) return;

        page = page <= 1 ? 1 : --page;
        await LoadArtistsAsync();
    }

    private async Task OnNextPageAsync()
    {
        if (isLoading) return;

        page++;
        await LoadArtistsAsync();
    }

    private async Task LoadArtistsAsync()
    {
        isLoading = true;

        var result = await artistService.GetPagedAsync(page);

        if (result.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Artists not loaded!", "OK");
            return;
        }

        Artists = new ObservableCollection<ArtistModel>(result.Value.Items);
        numberOfArtistsInDb = result.Value.Count;

        hasNextPage = numberOfArtistsInDb - (page * 10) > 0;
        isLoading = false;

        ((Command)PreviousPageCommand).ChangeCanExecute();
        ((Command)NextPageCommand).ChangeCanExecute();
    }

    private async Task OnDeleteAsync(int id)
    {
        var result = await artistService.DeleteAsync(id);

        var message = result.IsError ? result.FirstError.Description : "Artist deleted.";
        var title = result.IsError ? "Error" : "Information";

        if (!result.IsError)
        {
            var artist = artists.SingleOrDefault(x => x.Id == id);
            artists.Remove(artist);

            if (artists.Count == 0)
            {
                await LoadArtistsAsync();
            }
        }

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }
}
