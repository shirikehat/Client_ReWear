using Client_ReWear.ViewModels;

namespace Client_ReWear.Views;

public partial class ProductPage : ContentPage
{
	public ProductPage(ProductPageViewModel vm)
	{
        this.BindingContext = vm;
        InitializeComponent();
	}
}