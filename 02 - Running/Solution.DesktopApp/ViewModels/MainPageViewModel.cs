using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ErrorOr;
using Solution.Core.Interfaces;
using Solution.Core.Models;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class MainPageViewModel : RunningModel
{
    public DateTime MaxDateTime => DateTime.Now;

    [ObservableProperty]
    private double datePickerWidth;
    public IRelayCommand DistanceValidationCommand => new RelayCommand(() => Distance.Validate());
    public IRelayCommand TimeValidationCommand => new RelayCommand(() => Time.Validate());
    public IRelayCommand AverageSpeedValidationCommand => new RelayCommand(() => AverageSpeed.Validate());
    public IRelayCommand BurnedCaloriesValidationCommand => new RelayCommand(() => BurnedCalories.Validate());

    private readonly IRunningService runningService;
    public MainPageViewModel(IRunningService runningService) : base()
    {
        this.runningService = runningService;
    }
    public IAsyncRelayCommand OnSubmitCommand => new AsyncRelayCommand(OnSubmitAsync);
    private async Task OnSubmitAsync()
    {
        if (!IsFormValid)
        {
            return;
        }

        ErrorOr<RunningModel> serviceResponse = await runningService.CreateAsync(this);

        string alertMessage = serviceResponse.IsError ? serviceResponse.FirstError.Description : "Run saved!";
        await Application.Current!.MainPage!.DisplayAlert("Alert", alertMessage, "OK");
    }
    private bool IsFormValid => Date.IsValid &&
                                Distance.IsValid &&
                                AverageSpeed.IsValid &&
                                BurnedCalories.IsValid &&
                                Time.IsValid;






}
