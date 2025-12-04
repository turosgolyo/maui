namespace Bills.DesktopApp.ViewModels;

[ObservableObject]
public partial class ListBillsViewModel(IBillService billService)
{
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingAsync);

    public IAsyncRelayCommand DeleteCommand => new AsyncRelayCommand<int>((id) => OnDeleteAsync(id));

    [ObservableProperty]
    public ObservableCollection<BillModel> bills;

    private async Task OnAppearingAsync()
    {
        await LoadBillsAsync();
    }

    private async Task LoadBillsAsync()
    {
        var result = await billService.GetAllAsync();

        if (result.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Bills not loaded!", "OK");
            return;
        }

        Bills = new ObservableCollection<BillModel>(result.Value);
    }

    private async Task OnDeleteAsync(int id)
    {
        var result = await billService.DeleteAsync(id);

        var message = result.IsError ? result.FirstError.Description : "Bill deleted.";
        var title = result.IsError ? "Error" : "Information";

        if (!result.IsError)
        {
            var bill = Bills.SingleOrDefault(x => x.Id == id);
            Bills.Remove(bill);
        }

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }
}

