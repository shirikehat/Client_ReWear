using Client_ReWear.Models;
using Client_ReWear.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace Client_ReWear.ViewModels;

public class OrdersViewModel : ViewModelBase
{
    private ReWearWebAPI proxy;

    private IServiceProvider serviceProvider;
    public OrdersViewModel(ReWearWebAPI proxy, IServiceProvider serviceProvider)
	{
        this.serviceProvider = serviceProvider;
        this.proxy = proxy;
        RefreshCommand = new Command(Refresh);
        IsRefreshing = true;
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


    #region collection view of products
    private ObservableCollection<Product> products;
    public ObservableCollection<Product> Products
    {
        get => products;
        set
        {
            products = value;
            OnPropertyChanged("Products");
        }
    }

    private async void ReadDataFromServer()
    {

        List<Product>? products = await proxy.GetOrders();

        if (products != null)
        {

            Products.Clear();
            foreach (Product p in products)
            {
                Products.Add(p);
            }

        }

        IsRefreshing = false;
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
}