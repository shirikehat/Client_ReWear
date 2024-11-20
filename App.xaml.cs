using Client_ReWear.Models;
using Client_ReWear.Services;
using Client_ReWear.ViewModels;
using Client_ReWear.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Client_ReWear
{
    public partial class App : Application
    {

        //Application level variables
        public User? LoggedInUser { get; set; }
        private ReWearWebAPI proxy;

        public App(IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            InitializeComponent();

            LoggedInUser = null;
            //MainPage = new AppShell();
            MainPage = new NavigationPage( serviceProvider.GetService<Login>());
        }
    }
}
