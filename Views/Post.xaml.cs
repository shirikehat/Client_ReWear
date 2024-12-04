using Client_ReWear.ViewModels;

namespace Client_ReWear.Views;

public partial class Post : ContentPage
{
	public Post(PostViewModel vm)
	{
        this.BindingContext = vm;
        InitializeComponent();
	}
}