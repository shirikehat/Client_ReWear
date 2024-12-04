using Client_ReWear.ViewModels;

namespace Client_ReWear.Views;

public partial class Cart : ContentPage
{
	public Cart(CartViewModel vm)
	{
        this.BindingContext = vm;
        InitializeComponent();
	}
}