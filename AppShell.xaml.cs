using Client_ReWear.ViewModels;

namespace Client_ReWear
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel vm)
        {
            this.BindingContext = vm;
            InitializeComponent();
        }
    }
}
