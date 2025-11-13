using Bills.Core.Models;
using Bills.Database;
using CommunityToolkit.Mvvm.Input;

namespace Bills.DesktopApp.ViewModels;

public partial class CreateOrEditBillViewModel(
    AppDbContext dbContext,
    IBillService billService,
    IItemService itemService) : BillModel, IQueryAttributable
{
    public IAsyncRelayCommand SaveCommand => new AsyncRelayCommand(OnSaveAsync);

    [ObservableProperty]
    private IList<ItemModel> items = [];

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        bool hasValue = query.TryGetValue("Bill", out object result);

        if (!hasValue)
        {
            return;
        }

        BillModel bill = result as BillModel;

        this.Id = bill.Id;
        this.Number = bill.Number;
        this.Items = bill.Items;
    }
    
    //private async Task OnAddItemAsync()
    //{
    //    var newItem = new ItemModel
    //    {
    //        Name = 
    //    };
    //    this.Items.Add(newItem);
    //    await Task.CompletedTask;
    //}
    private async Task OnSaveAsync()
    {
        var result = await billService.CreateAsync(this);
        var message = result .IsError ? result.FirstError.Description : "Bill saved successfully.";
        var title = result.IsError ? "Error" : "Success";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }


}
