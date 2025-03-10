using Client_ReWear.Models;
using Client_ReWear.Services;
using Client_ReWear.Views;
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
        Carts = new ObservableCollection<Models.Cart>();
        RefreshCommand = new Command(Refresh);
        OrdersCommand = new Command(OnOrders);
        User u = ((App)Application.Current).LoggedInUser;
        User = u;
        IsRefreshing = true;
        IsEmpty = true;
        ReadDataFromServer();

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
                OnPropertyChanged(nameof(User));
            }
        }
    }


    #endregion


    #region collection view of carts
    private ObservableCollection<Models.Cart> carts;
    public ObservableCollection<Models.Cart> Carts
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

        List<Models.Cart>? carts = await proxy.GetCart();

        if (carts != null)
        {
            IsEmpty = false;
            Carts.Clear();
            foreach (Models.Cart c in carts)
            {
                Carts.Add(c);
            }
            
        }
       
        IsRefreshing = false;
    }
    #endregion

    #region is empty?
    private string str;
    public string Str
    {
        get => str;
        set
        {
            str = "Cart is empty";
            OnPropertyChanged("Str");
        }
    }

    private bool isEmpty;
    public bool IsEmpty
    {
        get => isEmpty;
        set
        {
            if (isEmpty != value)
            {
                isEmpty = value;
                OnPropertyChanged(nameof(IsEmpty));
            }
        }
    }
    #endregion

    #region Single Selection

    private Product selectedCart;
    public Product SelectedCart
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



    private async void OnSingleSelectCart(Product c)
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

    public ICommand RefreshCommand { get; }
    public void Refresh()
    {
        ReadDataFromServer();

    }


    public ICommand OrdersCommand { get; }
    private void OnOrders()
    {

        // Navigate to the Orders View page
        ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<Orders>());
    }

}