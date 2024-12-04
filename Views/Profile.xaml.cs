using Client_ReWear.ViewModels;

namespace Client_ReWear.Views;

public partial class Profile : ContentPage
{
	public Profile(ProfileViewModel vm)
	{
        this.BindingContext = vm;
        InitializeComponent();
	}
}