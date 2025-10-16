using Solution.Services;

namespace Solution.DesktopApp.ViewModels;

public partial class CreateOrEditArtistViewModel(
    AppDbContext dbContext, 
    IArtistService artistService) : ArtistModel, IQueryAttributable
{
    #region commands
    public IAsyncRelayCommand SubmitCommand => new AsyncRelayCommand(OnSubmitAsync);
    public ICommand ValidateCommand => new Command<string>(OnValidateAsync);
    private ArtistModelValidator validator => new ArtistModelValidator();
    #endregion

    [ObservableProperty]
    private string title;

    [ObservableProperty]
    private ValidationResult validationResult = new ValidationResult();
    
    private delegate Task ButtonActionDelagate();
    
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

        var result = await artistService.CreateAsync(this);
        var message = result.IsError ? result.FirstError.Description : "Artist saved.";
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

        var result = await artistService.UpdateAsync(this);

        var message = result.IsError ? result.FirstError.Description : "Artist updated.";
        var title = result.IsError ? "Error" : "Information";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private void ClearForm()
    {
        this.Name = null;
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        bool hasValue = query.TryGetValue("Artist", out object result);

        if (!hasValue)
        {
            asyncButtonAction = OnSaveAsync;
            Title = "Add new artist";
            return;
        }

        ArtistModel artist = result as ArtistModel;

        this.Id = artist.Id;
        this.Name = artist.Name;

        asyncButtonAction = OnUpdateAsync;
        Title = "Update artist";
    }
}
