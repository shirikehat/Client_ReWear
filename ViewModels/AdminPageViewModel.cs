
using Client_ReWear.Models;
using Client_ReWear.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace Client_ReWear.ViewModels;

public class AdminPageViewModel : ViewModelBase
{
    public ReWearWebAPI proxy;
    private readonly IServiceProvider serviceProvider;

    public AdminPageViewModel(ReWearWebAPI proxy, IServiceProvider serviceProvider)
	{
        this.serviceProvider = serviceProvider;
        this.proxy = proxy;

        User u = ((App)Application.Current).LoggedInUser;
        Name = u.UserName;
        PhotoURL = u.FullProfileImageUrl;
        LocalPhotoPath = "";
        InServerCall = false;
        ReadDataFromServer();
        Delete = new Command(OnDelete);
    }


    #region Name
    private string name;
    public string Name
    {
        get => name;
        set
        {
            name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    #endregion


    #region Photo

    private string photoURL;

    public string PhotoURL
    {
        get => photoURL;
        set
        {
            photoURL = value;
            OnPropertyChanged("PhotoURL");
        }
    }

    private string localPhotoPath;

    public string LocalPhotoPath
    {
        get => localPhotoPath;
        set
        {
            localPhotoPath = value;
            OnPropertyChanged("LocalPhotoPath");
        }
    }


    #endregion

    #region collection view of Users
    private ObservableCollection<User> users;
    public ObservableCollection<User> Users
    {
        get => users;
        set
        {
            users = value;
            OnPropertyChanged("Users");
        }
    }

    private async void ReadDataFromServer()
    {
        List<User>? users = await proxy.GetAllUsers();

        if (users != null)
        {
            Users = new ObservableCollection<User>();
            foreach (User u in users)
            {
                Users.Add(u);
            }
        }

    }
    #endregion

    #region Single Selection


    private User selectedUser;
    public User SelectedUser
    {
        get
        {
            return this.selectedUser;
        }
        set
        {
            this.selectedUser = value;
            OnSingleSelectUser(selectedUser);
            OnPropertyChanged();
        }
    }



    private async void OnSingleSelectUser(User u)
    {
        if (u != null)
        {
            ProfileNavigationState.NavigatedFromTabBar = false;
            var navParam = new Dictionary<string, object>
                {
                    {"selectedUser",u }
                };
            await Shell.Current.GoToAsync("AdminProfile", navParam);

            SelectedUser = null;

        }
    }

    #endregion

    public ICommand Delete { get; }
    public void OnDelete()
    {

    }
}