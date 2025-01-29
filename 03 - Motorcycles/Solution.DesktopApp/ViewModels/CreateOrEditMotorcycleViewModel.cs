namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class CreateOrEditMotorcycleViewModel(AppDbContext dbContext) : MotorcycleModel
{
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingkAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);

    
    public IRelayCommand ManufacturerIndexChangedCommand => new RelayCommand(() => this.Manufacturer.Validate());
    public IRelayCommand CylindersIndexChangedCommand => new RelayCommand(() => this.NumberOfCylinders.Validate());
    public IRelayCommand ModelValidationCommand => new RelayCommand(() => this.Model.Validate());
    public IRelayCommand CubicValidationCommand => new RelayCommand(() => this.Cubic.Validate());
    public IRelayCommand ReleaseYearValidationCommand => new RelayCommand(() => this.ReleaseYear.Validate());



    public IAsyncRelayCommand SaveCommand => new AsyncRelayCommand(OnSaveAsync);


    [ObservableProperty]
    private ICollection<ManufacturerModel> manufacturers;
    [ObservableProperty]
    private IList<uint> cylinders = [1, 2, 3, 4, 6, 8];

    private async Task OnAppearingkAsync()
    {
        Manufacturers = await dbContext.Manufacturers.AsNoTracking()
                                                     .Select(x => new ManufacturerModel(x))
                                                     .ToListAsync();
    }

    private async Task OnDisappearingAsync()
    { }

    private async Task OnSaveAsync()
    {
        if (!IsFormValid())
        {
            return;
        }
    }

    private bool IsFormValid()
    {
        this.Manufacturer.Validate();
        this.Model.Validate();
        this.Cubic.Validate();
        this.ReleaseYear.Validate();
        this.NumberOfCylinders.Validate();

        return this.Manufacturer?.IsValid ?? false &&
               this.Model.IsValid &&
               this.Cubic.IsValid &&
               this.ReleaseYear.IsValid &&
               this.NumberOfCylinders.IsValid;
    }
}
