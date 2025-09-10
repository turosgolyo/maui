using ErrorOr;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class CreateOrEditMotorcycleViewModel(
    AppDbContext dbContext, 
    IGoogleDriveService googleDriveService,
    IMotorcycleService motorcycleService) : MotorcycleModel(), IQueryAttributable
{
    #region life cycle commands
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);
    #endregion

    #region commands
    public IRelayCommand ManufacturerIndexChangedCommand => new RelayCommand(() => this.Manufacturer.Validate());
    public IRelayCommand CylindersIndexChangedCommand => new RelayCommand(() => this.NumberOfCylinders.Validate());
    public IRelayCommand TypeIndexChangedCommand => new RelayCommand(() => this.Type.Validate());
    public IRelayCommand ModelValidationCommand => new RelayCommand(() => this.Model.Validate());
    public IRelayCommand CubicValidationCommand => new RelayCommand(() => this.Cubic.Validate());
    public IRelayCommand ReleaseYearValidationCommand => new RelayCommand(() => this.ReleaseYear.Validate());
    public IAsyncRelayCommand SubmitCommand => new AsyncRelayCommand(OnSubmitAsync);
    #endregion


    private delegate Task ButtonActionDelegate();
    ButtonActionDelegate asyncButtonAction;

    [ObservableProperty]
    private string title;

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        await Task.Run(() => LoadManufacturersAsync());
        await Task.Run(() => LoadTypesAsync());

        bool hasValue = query.TryGetValue("Motorcycle", out object result);
        
        if (!hasValue)
        {
            asyncButtonAction = OnSaveAsync;
            Title = "Add new motorcycle";
            return;
        }

        MotorcycleModel motorcycle = result as MotorcycleModel;

        this.Id = motorcycle.Id;
        this.Manufacturer.Value = motorcycle.Manufacturer.Value;
        this.Model.Value = motorcycle.Model.Value;
        this.ReleaseYear.Value = motorcycle.ReleaseYear.Value;
        this.Cubic.Value = motorcycle.Cubic.Value;
        this.NumberOfCylinders.Value = motorcycle.NumberOfCylinders.Value;
        this.Type.Value = motorcycle.Type.Value;

        asyncButtonAction = OnUpdateAsync;
        Title = "Update motorcycle";
    }


    [ObservableProperty]
    private IList<ManufacturerModel> manufacturers = [];
    [ObservableProperty]
    private IList<uint> cylinders = [1, 2, 3, 4, 6, 8];
    [ObservableProperty]
    private IList<TypeModel> types = [];


    private async Task OnAppearingAsync()
    { }
    private async Task OnDisappearingAsync()
    { }
    private async Task OnSubmitAsync() => await asyncButtonAction();
    private async Task OnUpdateAsync()
    {
        if (!IsFormValid())
        {
            return;
        }

        var result = await motorcycleService.UpdateAsync(this);

        var title = result.IsError ? "Error" : "Information";
        var message = result.IsError ? result.FirstError.Description : "Motorcycle updated!";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }
    private async Task OnSaveAsync()
    {
        if (!IsFormValid())
        {
            return;
        }

        var result = await motorcycleService.CreateAsync(this);

        var title = result.IsError ? "Error" : "Information";
        var message = result.IsError ? result.FirstError.Description : "Motorcycle saved!";

        if (!result.IsError)
        {
            ClearForm();
        }

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }


    private async Task LoadManufacturersAsync()
    {
        Manufacturers = await dbContext.Manufacturers.AsNoTracking()
                                                     .OrderBy(x => x.Name)
                                                     .Select(x => new ManufacturerModel(x))
                                                     .ToListAsync();
    }
    private async Task LoadTypesAsync()
    {
        Types = await dbContext.Types.AsNoTracking()
                                     .OrderBy(x => x.Name)
                                     .Select(x => new TypeModel(x))
                                     .ToListAsync();
    }
    private void ClearForm()
    {
        this.Manufacturer.Value = null;
        this.Model.Value = null;
        this.Cubic.Value = null;
        this.ReleaseYear.Value = null;
        this.NumberOfCylinders.Value = null;
        this.Type.Value = null;
    }


    private bool IsFormValid()
    {
        this.Manufacturer.Validate();
        this.Model.Validate();
        this.Cubic.Validate();
        this.ReleaseYear.Validate();
        this.NumberOfCylinders.Validate();
        this.Type.Validate();


        return (this.Manufacturer?.IsValid ?? false) &&
               this.Model.IsValid &&
               this.Cubic.IsValid &&
               this.ReleaseYear.IsValid &&
               (this.NumberOfCylinders?.IsValid ?? false) &&
               (this.Type?.IsValid ?? false);
    }
}
