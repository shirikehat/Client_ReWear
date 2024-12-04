using Client_ReWear.ViewModels;

namespace Client_ReWear.Views;

public partial class Buy : ContentPage
{
	public Buy(BuyViewModel vm)
	{
        this.BindingContext = vm;
        InitializeComponent();
	}
}