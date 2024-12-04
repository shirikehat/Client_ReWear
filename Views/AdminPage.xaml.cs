using Client_ReWear.ViewModels;

namespace Client_ReWear.Views;

public partial class AdminPage : ContentPage
{
	public AdminPage(AdminPageViewModel vm)
	{
        this.BindingContext = vm;
        InitializeComponent();
	}
}