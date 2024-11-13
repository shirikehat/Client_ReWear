using Client_ReWear.Models;
using Client_ReWear.Services;
using Client_ReWear.Views;
using System.Diagnostics;
using System.Windows.Input;

namespace Client_ReWear.ViewModels;

public class LoginViewModel : ViewModelBase
{
    public ReWearWebAPI service;
    private readonly IServiceProvider serviceProvider;

    public ICommand LoginCommand { get; set; }
    public ICommand GoToRegisterCommand { get; set; }
    private string username;


    public LoginViewModel(ReWearWebAPI service, IServiceProvider serviceProvider)
    {
        this.service = service;
        this.serviceProvider = serviceProvider;
        //this.LoginCommand = new Command(OnLogin);
        this.GoToRegisterCommand = new Command(OnRegisterClicked);
    }

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


    //public async void OnLogin()
    //{
    //    LoginInfo info = new LoginInfo
    //    {
    //        Username = this.Username,
    //        Password = this.Password
    //    };
    //    User user = await service.Login(info);
    //    if (user != null)
    //    {
    //        // áãé÷ä äàí äîúùúîù äåà ÷åðä
    //        if (user.UserType == "3")
    //        {

    //            var buyerShell = serviceProvider.GetRequiredService<AppShell>();
    //            App.Current.MainPage = buyerShell;

    //            // ÷áìú UserDetailsPageViewModel îä-DI åäâãøú äðúåðéí
    //            var userDetailsViewModel = serviceProvider.GetRequiredService<UserDetailsPageViewModel>();
    //            userDetailsViewModel.Initialize(user); // àúçåì ôøèé äîùúîù

    //            // äâãøú BindingContext òáåø UserDetailsPage
    //            var userDetailsPage = serviceProvider.GetRequiredService<UserDetailsPage>();
    //            userDetailsPage.BindingContext = userDetailsViewModel;



    //        }
    //        // áãé÷ä äàí äîúùúîù äåà îåëø
    //        if (user.UserType == "2")
    //        {

    //            // ÷áìú BusinessProductsPageViewModel î-DI
    //            var viewModel = serviceProvider.GetRequiredService<BusinessProductsPageViewModel>();

    //            // èòéðú äîåöøéí ùì äîåëø
    //            await viewModel.LoadProducts(user.UserId);

    //            // äâãøú ä-BindingContext ùì BusinessProductsPage
    //            var businessProductsPage = serviceProvider.GetRequiredService<BusinessProductsPage>();
    //            businessProductsPage.BindingContext = viewModel;
    //            var sellerShell = serviceProvider.GetRequiredService<SellerShell>();

    //            App.Current.MainPage = sellerShell;

    //            // ÷áìú SellerDetailsPageViewModel î-DI åäâãøú äðúåðéí
    //            var sellerDetailsViewModel = serviceProvider.GetRequiredService<SellerDetailsPageViewModel>();
    //            await sellerDetailsViewModel.Initialize(user.UserId);

    //            // äâãøú BindingContext òáåø SellerDetailsPage
    //            var sellerDetailsPage = serviceProvider.GetRequiredService<SellerDetailsPage>();
    //            sellerDetailsPage.BindingContext = sellerDetailsViewModel;

    //        }

    //    }

    //    else
    //    {
    //        Debug.WriteLine("Login failed");
    //    }
    //}

    private async void OnRegisterClicked()
    {
        // ÷áìú äãó ãøê ä-DI åðéååè àìéå
        var registrationPage = serviceProvider.GetRequiredService<Register>();
        await App.Current.MainPage.Navigation.PushAsync(registrationPage);
    }





}