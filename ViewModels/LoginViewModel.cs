using System.Windows.Input;

namespace Client_ReWear.ViewModels;

public class LoginViewModel : ViewModelBase
{
    //public SweetBoxWebApi service;
    //private readonly IServiceProvider serviceProvider;

    public ICommand LoginCommand { get; set; }
    public ICommand GoToRegisterCommand { get; set; }
    private string username;
    public string Username
    {
        get => username;
        set
        {
            if (username != value)
            {
                username = value;
                OnPropertyChanged(nameof(Username));
                // can add more logic here like email validation etc.
                // can add error message property and set it here
            }
        }
    }
    private string password;
    public string Password
    {
        get => password;
        set
        {
            if (password != value)
            {
                password = value;
                OnPropertyChanged(nameof(Password));
                // can add more logic here like email validation etc.
                // can add error message property and set it here
            }
        }
    }
}