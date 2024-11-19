using Client_ReWear.Services;

namespace Client_ReWear.ViewModels;

public class HomeViewModel : ViewModelBase
{


    private ReWearWebAPI proxy;

    public HomeViewModel(ReWearWebAPI proxy)
	{
        this.proxy = proxy;

    }


    //This is a public method that should be called when the page needs to be refreshed
    public void Refresh()
    {
        //this.userTasks = ((App)Application.Current).LoggedInUser.UserTasks;
        //FilterTasks();
    }
}