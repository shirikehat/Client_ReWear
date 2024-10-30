using Client_ReWear.ViewModels;

namespace Client_ReWear.Views;

public partial class Register : ContentPage
{
	public Register(RegisterViewModel vm)
	{
        this.BindingContext = vm;
        InitializeComponent();
	}
}