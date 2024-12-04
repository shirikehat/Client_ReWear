using Client_ReWear.ViewModels;

namespace Client_ReWear.Views;

public partial class Wishlist : ContentPage
{
	public Wishlist(WishlistViewModel vm)
	{
        this.BindingContext = vm;
        InitializeComponent();
	}
}