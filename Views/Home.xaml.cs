using Client_ReWear.ViewModels;

namespace Client_ReWear.Views;

public partial class Home : ContentPage
{
	public Home(HomeViewModel vm)
	{
        this.BindingContext = vm;
        InitializeComponent();
	}
}