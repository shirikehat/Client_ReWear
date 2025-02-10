using Client_ReWear.ViewModels;

namespace Client_ReWear.Views;

public partial class Profile : ContentPage
{
	public Profile(ProfileViewModel vm)
	{
		//אם לא שלחתי פרמטר של משתמש אחר העמוד פרופיל של המשתמש המחובר
		if (vm.TheUser == null)
			vm.TheUser = ((App)Application.Current).LoggedInUser;
        this.BindingContext = vm;
        InitializeComponent();
	}
}