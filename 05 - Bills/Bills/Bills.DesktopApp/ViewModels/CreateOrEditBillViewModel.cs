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

    [ObservableProperty]
    private ObservableCollection<ItemModel> items = [];

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
        this.Items = new ObservableCollection<ItemModel>(bill.Items);
    }

    private async Task OnAddItemAsync()
    {
        var newItem = new ItemModel
        {
            Name = this.ItemName,
            Price = this.ItemPrice,
            Amount = this.ItemAmount
        };
        this.Items.Add(newItem);
        await Task.CompletedTask;
    }
    private async Task OnSaveAsync()
    {
        var result = await billService.CreateAsync(this);
        var message = result .IsError ? result.FirstError.Description : "Bill saved successfully.";
        var title = result.IsError ? "Error" : "Success";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }


}
