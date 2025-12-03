
namespace Bills.DesktopApp.ViewModels;

public partial class CreateOrEditBillViewModel(
    AppDbContext dbContext,
    IBillService billService,
    IItemService itemService) : BillModel, IQueryAttributable
{
    #region commands
    public IAsyncRelayCommand BillButtonCommand => new AsyncRelayCommand(OnSubmitBillAsync);
    public IAsyncRelayCommand ItemButtonCommand => new AsyncRelayCommand(OnSubmitItemAsync);
    public IAsyncRelayCommand DeleteCommand => new AsyncRelayCommand<ItemModel>((item) => OnDeleteItemAsync(item));
    public IAsyncRelayCommand EditCommand => new AsyncRelayCommand<ItemModel>((item) => OnClickUpdateItemAsync(item));
    public ICommand ValidateCommand => new Command<string>(OnValidateAsync);
    #endregion

    #region buttons
    private async Task OnSubmitBillAsync() => await asyncBillButtonAction();
    private async Task OnSubmitItemAsync() => await asyncItemButtonAction();

    private delegate Task ButtonActionDelagate();

    private ButtonActionDelagate asyncBillButtonAction;

    private ButtonActionDelagate asyncItemButtonAction;
    #endregion

    [ObservableProperty]
    private string title;

    [ObservableProperty]
    private ObservableCollection<ItemModel> addedItems = [];

    [ObservableProperty]
    private ItemModel item = new();

    [ObservableProperty]
    private bool canAddItem;

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        bool hasValue = query.TryGetValue("Bill", out object result);
        if (!hasValue)
        {
            asyncBillButtonAction = OnSaveAsync;
            asyncItemButtonAction = OnAddItemAsync;
            Title = "Add new  bills";
            return;
        }

        BillModel bill = result as BillModel;

        await LoadItemsByBillAsync(bill.Id);

        this.Id = bill.Id;
        this.Number = bill.Number;
        this.Items = bill.Items;
    }

    private async Task OnAddItemAsync()
    {
        this.ValidationResult = await itemValidator.ValidateAsync(this.Item);

        if (!ValidationResult.IsValid)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Add item failed", "OK");
            return;
        }

        var newItem = new ItemModel
        {
            TempId = Guid.NewGuid(),
            Name = this.Item.Name,
            Price = this.Item.Price,
            Amount = this.Item.Amount
        };

        this.AddedItems.Add(newItem);
        ClearItemForm();
        await Task.CompletedTask;
    }
    private async Task OnSaveAsync()
    {
        this.ValidationResult = await billValidator.ValidateAsync(this);

        if (!ValidationResult.IsValid)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Save failed", "OK");
            return;
        }

        this.Items = this.AddedItems.ToList();

        var result = await billService.CreateAsync(this);
        var message = result .IsError ? result.FirstError.Description : "Bill saved successfully.";
        var title = result.IsError ? "Error" : "Success";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
        ClearForm();
    }
    private async Task OnDeleteItemAsync(ItemModel item)
    {
        addedItems.Remove(item);

        if(item.Id != 0)
        {
            var result = await itemService.DeleteAsync(item);
            var message = result.IsError ? result.FirstError.Description : "Item deleted successfully.";
            var title = result.IsError ? "Error" : "Success";
            await Application.Current.MainPage.DisplayAlert(title, message, "OK");
        }
    }
    private async Task OnClickUpdateItemAsync(ItemModel item)
    {
        this.Item = new ItemModel
        {
            Id = item.Id,
            TempId = item.TempId,
            Name = item.Name,
            Price = item.Price,
            Amount = item.Amount
        };

        asyncItemButtonAction = OnUpdateItemAsync;
    }

    private async Task OnUpdateItemAsync()
    {
        this.ValidationResult = await itemValidator.ValidateAsync(this.Item);

        if (!ValidationResult.IsValid)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Add item failed", "OK");
            return;
        }

        var updatedItem = this.Item;

        var existingItem = addedItems.FirstOrDefault(item => item.TempId == updatedItem.TempId);

        if (existingItem != null)
        {
            existingItem.Name = updatedItem.Name;
            existingItem.Price = updatedItem.Price;
            existingItem.Amount = updatedItem.Amount;
        }


        if (updatedItem.Id != 0)
        {
            var result = await itemService.UpdateAsync(updatedItem);
            var message = result.IsError ? result.FirstError.Description : "Item updated.";
            var title = result.IsError ? "Error" : "Information";
            await Application.Current.MainPage.DisplayAlert(title, message, "OK");
        }

        ClearItemForm();
        asyncItemButtonAction = OnAddItemAsync;
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

    private void ClearForm() {
        this.Number = null;
        this.Date = DateTime.Now;
        this.Items = null;
        this.AddedItems = null;
        ClearItemForm();
    }

    private void ClearItemForm()
    {
        this.Item = new ItemModel();
    }


    #region validation
    private BillModelValidator billValidator => new BillModelValidator();
    private ItemModelValidator itemValidator => new ItemModelValidator();

    [ObservableProperty]
    private ValidationResult validationResult = new ValidationResult();
    [ObservableProperty]
    private ValidationResult itemValidationResult = new ValidationResult();

    private async void OnValidateAsync(string propertyName)
    {
        var result = await billValidator.ValidateAsync(this, options => options.IncludeProperties(propertyName));

        ValidationResult.Errors.Remove(ValidationResult.Errors.FirstOrDefault(x => x.PropertyName == propertyName));

        ValidationResult.Errors.Remove(ValidationResult.Errors.FirstOrDefault(x => x.PropertyName == BillModelValidator.GlobalProperty));
        ValidationResult.Errors.AddRange(result.Errors);

        OnPropertyChanged(nameof(ValidationResult));

        CanAddItem = !string.IsNullOrEmpty(Number) && 
                     Date != default && 
                     !ValidationResult.Errors.Any(e => e.PropertyName == BillModelValidator.NumberProperty || 
                                                       e.PropertyName == BillModelValidator.DateProperty);
    }
    #endregion
}
