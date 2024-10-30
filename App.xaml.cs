using Client_ReWear.ViewModels;
using Client_ReWear.Views;

namespace Client_ReWear
{
    public partial class App : Application
    {
        public App(RegisterViewModel vm)
        {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new Register(vm);
        }
    }
}
