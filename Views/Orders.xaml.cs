using Client_ReWear.ViewModels;

namespace Client_ReWear.Views;

public partial class Orders : ContentPage
{
	public Orders(OrdersViewModel vm)
	{
        this.BindingContext = vm;
        InitializeComponent();
	}
}