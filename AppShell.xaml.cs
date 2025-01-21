using Client_ReWear.ViewModels;
using Client_ReWear.Views;
namespace Client_ReWear
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel vm)
        {
            this.BindingContext = vm;
            DefineRoutes();
            InitializeComponent();
        }

        private void DefineRoutes()
        {
            Routing.RegisterRoute("ProductView", typeof(ProductPage));
            Routing.RegisterRoute("EditProfileView", typeof(EditProfile));
        }
    }
}
