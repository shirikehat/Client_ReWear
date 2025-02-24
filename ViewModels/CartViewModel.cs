using Client_ReWear.Models;
using Client_ReWear.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Client_ReWear.ViewModels;

public class CartViewModel : ViewModelBase
{
    private ReWearWebAPI proxy;

    private IServiceProvider serviceProvider;


    public CartViewModel(ReWearWebAPI proxy, IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
        this.proxy = proxy;
        Carts = new ObservableCollection<Cart>();

        User u = ((App)Application.Current).LoggedInUser;
        User = u;
    }

    #region User
    private User user;
    public User User
    {
        get => user;
        set
        {
            if (user != value)
            {
                user = value;
                ReadDataFromServer();
                OnPropertyChanged(nameof(User));
            }
        }
    }


    #endregion


    #region collection view of carts
    private ObservableCollection<Cart> carts;
    public ObservableCollection<Cart> Carts
    {
        get => carts;
        set
        {
            carts = value;
            OnPropertyChanged("Carts");
        }
    }

    private async void ReadDataFromServer()
    {

        InServerCall = false;

        List<Cart>? carts = await proxy.GetCart();

        if (carts != null)
        {
            Carts.Clear();
            foreach (Cart c in carts)
            {
                Carts.Add(c);
            }
        }

    }
    #endregion


    #region Single Selection


    private Cart selectedCart;
    public Cart SelectedCart
    {
        get
        {
            return this.selectedCart;
        }
        set
        {
            this.selectedCart = value;
            OnSingleSelectCart(selectedCart);
            OnPropertyChanged();
        }
    }



    private async void OnSingleSelectCart(Cart c)
    {
        if (c != null)
        {
            var navParam = new Dictionary<string, object>
                {
                    {"selectedCart",c }
                };
            await Shell.Current.GoToAsync("ProductView", navParam);

            SelectedCart = null;

        }
    }

    #endregion



    private bool isRefreshing;
    public bool IsRefreshing
    {
        get => isRefreshing;
        set
        {
            if (isRefreshing != value)
            {
                isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }
    }

    //RefreshView refreshView = new RefreshView();
    //ICommand refreshCommand = new Command(() =>
    //{
    //    // IsRefreshing is true
    //    ReadDataFromServer();
    //    // Refresh data here
    //    refreshView.IsRefreshing = false;
    //});

    //refreshView.Command = refreshCommand;
    //    refreshView.Content = scrollView;


}