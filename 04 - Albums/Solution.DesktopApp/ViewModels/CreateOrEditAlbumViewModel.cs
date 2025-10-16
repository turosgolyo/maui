using Solution.Services;

namespace Solution.DesktopApp.ViewModels;

public partial class CreateOrEditAlbumViewModel(
    AppDbContext dbContext, 
    IAlbumService albumService, 
    IGoogleDriveService googleDriveService) : AlbumModel, IQueryAttributable
{
    #region commands
 
    public ICommand ValidateCommand => new Command<string>(OnValidateAsync);
    public IAsyncRelayCommand SubmitCommand => new AsyncRelayCommand(OnSubmitAsync);
    public IAsyncRelayCommand ImageSelectCommand => new AsyncRelayCommand(OnImageSelectAsync);
    private AlbumModelValidator validator => new AlbumModelValidator();
    
    #endregion

    [ObservableProperty]
    private string title;

    [ObservableProperty]
    private ImageSource image;

    [ObservableProperty]
    private ValidationResult validationResult = new ValidationResult();

    [ObservableProperty]
    private IList<ArtistModel> artists = [];

    private delegate Task ButtonActionDelagate();

    private FileResult selectedFile = null;

    private ButtonActionDelagate asyncButtonAction;


    private async Task OnSubmitAsync() => await asyncButtonAction();

    private async void OnValidateAsync(string propertyName)
    {
        var result = await validator.ValidateAsync(this, options => options.IncludeProperties(propertyName));

        ValidationResult.Errors.Remove(ValidationResult.Errors.FirstOrDefault(x => x.PropertyName == propertyName));
        ValidationResult.Errors.Remove(ValidationResult.Errors.FirstOrDefault(x => x.PropertyName == ArtistModelValidator.GlobalProperty));

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

        await UploadImageAsync();

        var result = await albumService.CreateAsync(this);
        var message = result.IsError ? result.FirstError.Description : "Album saved.";
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

        await UploadImageAsync();

        var result = await albumService.UpdateAsync(this);

        var message = result.IsError ? result.FirstError.Description : "Album updated.";
        var title = result.IsError ? "Error" : "Information";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private async Task UploadImageAsync()
    {
        if (selectedFile is null)
        {
            return;
        }

        var imageUploadResult = await googleDriveService.UploadFileAsync(selectedFile);

        var message = imageUploadResult.IsError ? imageUploadResult.FirstError.Description : "Album cover uploaded.";
        var title = imageUploadResult.IsError ? "Error" : "Information";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");

        this.ImageId = imageUploadResult.IsError ? null : imageUploadResult.Value.Id;
        this.WebContentLink = imageUploadResult.IsError ? null : imageUploadResult.Value.WebContentLink;
    }

    private async Task OnImageSelectAsync()
    {
        selectedFile = await FilePicker.PickAsync(new PickOptions
        {
            FileTypes = FilePickerFileType.Images,
            PickerTitle = "Please select the album cover"
        });

        if (selectedFile is null)
        {
            return;
        }

        var stream = await selectedFile.OpenReadAsync();
        Image = ImageSource.FromStream(() => stream);
    }

    private void ClearForm()
    {
        this.Name = null;
    }

    private async Task LoadArtistsAsync()
    {
        Artists = await dbContext.Artists.AsNoTracking()
                                        .OrderBy(x => x.Name)
                                        .Select(x => new ArtistModel(x))
                                        .ToListAsync();
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        await Task.Run(LoadArtistsAsync);

        bool hasValue = query.TryGetValue("Album", out object result);

        if (!hasValue)
        {
            asyncButtonAction = OnSaveAsync;
            Title = "Add new album";
            return;
        }

        AlbumModel album = result as AlbumModel;

        this.Id = album.Id;
        this.Name = album.Name;
        this.ReleaseDate = album.ReleaseDate;
        this.Genre = album.Genre;
        this.Artist = album.Artist;

        if (!string.IsNullOrEmpty(album.WebContentLink))
        {
            Image = new UriImageSource
            {
                Uri = new Uri(album.WebContentLink),
                CacheValidity = new TimeSpan(10, 0, 0, 0)
            };
        }

        asyncButtonAction = OnUpdateAsync;
        Title = "Update album";
    }
}
