using System.Collections.ObjectModel;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class MotorcycleListViewModel(IMotorcycleService motorcycleService)
{
    #region life cycle commands
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);
    #endregion

    #region paging commands
    public IAsyncRelayCommand PreviousPageCommand => new AsyncRelayCommand(OnPreviousPageAsync);
    public IAsyncRelayCommand NextPageCommand => new AsyncRelayCommand(OnNextPageAsync);
    #endregion

    [ObservableProperty]
    private ObservableCollection<MotorcycleModel> motorcycles = [];

    [ObservableProperty]
    private bool isPreviousButtonEnabled;

    [ObservableProperty]
    private bool isNextButtonEnabled;

    private int page = 1;
    private bool isLoading = false;
    private bool hasNextPage = true;

    private async Task OnAppearingAsync()
    {
        await LoadMotorcycles();
    }

    private async Task LoadMotorcycles()
    {
        var result = await motorcycleService.GetPagedAsync(page);

        if (result.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Motorcycles not loaded!", "OK");
            return;
        }

        hasNextPage = !(result.Value.Count < 5);

        Motorcycles = new ObservableCollection<MotorcycleModel>(result.Value);
    }

    private async Task OnDisappearingAsync()
    { }

    private async Task OnNextPageAsync()
    {
        page++;
        isLoading = true;
        await LoadMotorcycles();
        isLoading = false;

        isPreviousButtonEnabled = page > 1 && !isLoading;
        isNextButtonEnabled = !isLoading && hasNextPage;
    }

    private async Task OnPreviousPageAsync()
    {
        page = page <= 1 ? 1 : page--;
        isLoading = true;
        await LoadMotorcycles();
        isLoading = false;

        isPreviousButtonEnabled = page > 1 && !isLoading;
        isNextButtonEnabled = !isLoading && hasNextPage;
    }
}
