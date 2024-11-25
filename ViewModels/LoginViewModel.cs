using Client_ReWear.Models;
using Client_ReWear.Services;
using Client_ReWear.Views;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Input;

namespace Client_ReWear.ViewModels;

public class LoginViewModel : ViewModelBase
{
    public ReWearWebAPI proxy;
    private readonly IServiceProvider serviceProvider;

    
  
   


    public LoginViewModel(ReWearWebAPI service, IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
            this.proxy = service;
            LoginCommand = new Command(OnLogin);
            RegisterCommand = new Command(OnRegister);
            username = "";
            password = "";
            InServerCall = false;
            errorMsg = "";
    }

    private string username;
    private string password;

    public string Username
    {
        get => username;
        set
        {
            if (username != value)
            {
                username = value;
                OnPropertyChanged(nameof(username));
            }
        }
    }

    public string Password
    {
        get => password;
        set
        {
            if (password != value)
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
    }

    private string errorMsg;
    public string ErrorMsg
    {
        get => errorMsg;
        set
        {
            if (errorMsg != value)
            {
                errorMsg = value;
                OnPropertyChanged(nameof(ErrorMsg));
            }
        }
    }


    public ICommand LoginCommand { get; }
    public ICommand RegisterCommand { get; }



    private async void OnLogin()
    {
        //Choose the way you want to blobk the page while indicating a server call
        InServerCall = true;
        ErrorMsg = "";
        //Call the server to login
        LoginInfo loginInfo = new LoginInfo { Username = Username, Password = Password };
        User? u = await this.proxy.Login(loginInfo);

        InServerCall = false;

        //Set the application logged in user to be whatever user returned (null or real user)
        ((App)Application.Current).LoggedInUser = u;
        if (u == null)
        {
            ErrorMsg = "Invalid username or password";
        }
        else
        {
            ErrorMsg = "";
            //Navigate to the main page
            AppShell shell = serviceProvider.GetService<AppShell>();
            //HomeViewModel homeViewModel = serviceProvider.GetService<HomeViewModel>();
            //homeViewModel.Refresh(); //Refresh data and user in the tasksview model as it is a singleton
            ((App)Application.Current).MainPage = shell;
            Shell.Current.FlyoutIsPresented = false; //close the flyout
            Shell.Current.GoToAsync("Home"); //Navigate to the Home tab page
        }
    }

    private void OnRegister()
    {
        ErrorMsg = "";
        Username = "";
        Password = "";
        // Navigate to the Register View page
        ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<Register>());
    }





}