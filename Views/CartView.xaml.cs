using Client_ReWear.ViewModels;

namespace Client_ReWear.Views;

public partial class CartView : ContentPage
{
	public CartView(CartViewModel vm)
	{
        this.BindingContext = vm;
        InitializeComponent();
	}
}