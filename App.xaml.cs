using Client_ReWear.ViewModels;
using Client_ReWear.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Client_ReWear
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new NavigationPage( serviceProvider.GetService<Login>());
        }
    }
}
