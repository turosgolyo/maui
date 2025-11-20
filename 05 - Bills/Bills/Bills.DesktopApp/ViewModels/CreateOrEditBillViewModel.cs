using Bills.Core.Models;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Bills.DesktopApp.ViewModels;

public partial class CreateOrEditBillViewModel(
    AppDbContext dbContext,
    IBillService billService,
    IItemService itemService) : BillModel, IQueryAttributable
{
    public IAsyncRelayCommand SaveCommand => new AsyncRelayCommand(OnSaveAsync);
    public IAsyncRelayCommand AddItemCommand => new AsyncRelayCommand(OnAddItemAsync);
    public IAsyncRelayCommand DeleteCommand => new AsyncRelayCommand<ItemModel>((item) => OnDeleteItemAsync(item));

    [ObservableProperty]
    private ObservableCollection<ItemModel> addedItems = [];

    [ObservableProperty]
    private string itemName;

    [ObservableProperty]
    private double itemPrice;

    [ObservableProperty]
    private int itemAmount;


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

    private async Task OnAddItemAsync()
    {
        var newItem = new ItemModel
        {
            Name = this.ItemName,
            Price = this.ItemPrice,
            Amount = this.ItemAmount
        };

        this.AddedItems.Add(newItem);
        await Task.CompletedTask;
    }
    private async Task OnSaveAsync()
    {
        this.Items = this.AddedItems.ToList();

        var result = await billService.CreateAsync(this);
        var message = result .IsError ? result.FirstError.Description : "Bill saved successfully.";
        var title = result.IsError ? "Error" : "Success";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
        //await LoadItemsByBillAsync(this.Id);
    }

    private async Task OnDeleteItemAsync(ItemModel item)
    {
        //var addedItem = addedItems.SingleOrDefault(i => i.Id == id);
        //addedItems.Remove(addedItem);

        //var result = await itemService.DeleteAsync(id);

        //var message = result.IsError ? result.FirstError.Description : "Item deleted successfully.";
        //var title = result.IsError ? "Error" : "Success";

        //await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private async Task LoadItemsByBillAsync(int id)
    {
        var billResult = await billService.GetByIdAsync(id);
        if (billResult.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Error", billResult.FirstError.Description, "OK");
            return;
        }
        var bill = billResult.Value;
        var items = bill.Items ?? new List<ItemModel>();
        this.AddedItems = new ObservableCollection<ItemModel>(items);
    }
}
