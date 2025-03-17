using Client_ReWear.ViewModels;

namespace Client_ReWear.Views;

public partial class Login : ContentPage
{
	public Login(LoginViewModel vm)
	{
		this.BindingContext = vm;
        InitializeComponent();
	}
}