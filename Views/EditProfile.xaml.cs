using Client_ReWear.ViewModels;

namespace Client_ReWear.Views;

public partial class EditProfile : ContentPage
{
	public EditProfile(EditProfileViewModel vm)
	{
        this.BindingContext = vm;
        InitializeComponent();
	}
}