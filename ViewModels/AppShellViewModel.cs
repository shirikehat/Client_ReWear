using Client_ReWear.Models;
using Client_ReWear.Views;

namespace Client_ReWear.ViewModels;

public class AppShellViewModel : ViewModelBase
{
    private User? currentUser;
    private IServiceProvider serviceProvider;
    public AppShellViewModel(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
        this.currentUser = ((App)Application.Current).LoggedInUser;
    }

    public bool IsManager
    {
        get
        {
            return this.currentUser.IsManager;
        }
    }

    //this command will be used for logout menu item
    public Command LogoutCommand
    {
        get
        {
            return new Command(OnLogout);
        }
    }
    //this method will be trigger upon Logout button click
    public void OnLogout()
    {
        ((App)Application.Current).LoggedInUser = null;

        ((App)Application.Current).MainPage = new NavigationPage(serviceProvider.GetService<Login>());
    }
}