using FluentValidation;

namespace Solution.DesktopApp.ViewModels;

public partial class CreateOrEditSongViewModel(
    AppDbContext dbContext, 
    ISongService songService, 
    IGoogleDriveService googleDriveService) : SongModel, IQueryAttributable
{
    #region commands
 
    public ICommand ValidateCommand => new Command<string>(OnValidateAsync);
    public IAsyncRelayCommand SubmitCommand => new AsyncRelayCommand(OnSubmitAsync);
    private SongModelValidator validator => new SongModelValidator();
    
    #endregion

    [ObservableProperty]
    private string title;

    [ObservableProperty]
    private ValidationResult validationResult = new ValidationResult();

    [ObservableProperty]
    private IList<ArtistModel> artists = [];

    [ObservableProperty]
    private IList<AlbumModel> albums = [];

    private delegate Task ButtonActionDelagate();

    private ButtonActionDelagate asyncButtonAction;


    private async Task OnSubmitAsync() => await asyncButtonAction();

    private async void OnValidateAsync(string propertyName)
    {
        var result = await validator.ValidateAsync(this, options => options.IncludeProperties(propertyName));

        ValidationResult.Errors.Remove(ValidationResult.Errors.FirstOrDefault(x => x.PropertyName == propertyName));
        ValidationResult.Errors.Remove(ValidationResult.Errors.FirstOrDefault(x => x.PropertyName == SongModelValidator.GlobalProperty));

        ValidationResult.Errors.AddRange(result.Errors);

        OnPropertyChanged(nameof(propertyName));
    }

    private async Task OnSaveAsync()
    {
        this.ValidationResult = await validator.ValidateAsync(this);

        if (!ValidationResult.IsValid)
        {
            return;
        }

        var result = await songService.CreateAsync(this);
        var message = result.IsError ? result.FirstError.Description : "Song saved.";
        var title = result.IsError ? "Error" : "Information";

        if (!result.IsError)
        {
            ClearForm();
        }

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private async Task OnUpdateAsync()
    {
        if (!ValidationResult.IsValid)
        {
            return;
        }

        var result = await songService.UpdateAsync(this);

        var message = result.IsError ? result.FirstError.Description : "Song updated.";
        var title = result.IsError ? "Error" : "Information";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private void ClearForm()
    {
        this.Name = null;
        this.Artist = null;
        this.Album = null;
    }

    private async Task LoadArtistsAsync()
    {
        Artists = await dbContext.Artists.AsNoTracking()
                                        .OrderBy(x => x.Name)
                                        .Select(x => new ArtistModel(x))
                                        .ToListAsync();
    }
    private async Task LoadAlbumsAsync()
    {
        Albums = await dbContext.Albums.AsNoTracking()
                                       .OrderBy(x => x.Name)
                                       .Select(x => new AlbumModel(x))
                                       .ToListAsync();
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        await Task.Run(LoadArtistsAsync);
        await Task.Run(LoadAlbumsAsync);

        bool hasValue = query.TryGetValue("Song", out object result);

        if (!hasValue)
        {
            asyncButtonAction = OnSaveAsync;
            Title = "Add new song";
            return;
        }

        SongModel song = result as SongModel;

        this.Id = song.Id;
        this.Name = song.Name;
        this.Duration = song.Duration;
        this.Artist = song.Artist;

        asyncButtonAction = OnUpdateAsync;
        Title = "Update song";
    }
}
