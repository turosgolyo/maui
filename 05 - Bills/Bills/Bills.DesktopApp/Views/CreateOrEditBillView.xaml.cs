using Bills.DesktopApp.ViewModels;

namespace Bills.DesktopApp.Views;

public partial class CreateOrEditBillView : ContentPage
{
	public CreateOrEditBillViewModel ViewModel => this.BindingContext as CreateOrEditBillViewModel;
    public static string Name => nameof(CreateOrEditBillView);
    public CreateOrEditBillView(CreateOrEditBillViewModel viewModel)
	{
		this.BindingContext = viewModel;
		InitializeComponent();
	}
}