using Client_ReWear.Models;
using Client_ReWear.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Client_ReWear.ViewModels;

public class HomeViewModel : ViewModelBase
{


    private ReWearWebAPI proxy;

    public HomeViewModel(ReWearWebAPI proxy)
	{
        this.proxy = proxy;
        RefreshCommand = new Command(Refresh);
        IsRefreshing = true;
        Products = new ObservableCollection<Product>();
        ReadDataFromServer();
    }

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
        LocalPhotoPath = "";
        List<Product>? products = await proxy.GetAllProducts();

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


    

    private string localPhotoPath;

    public string LocalPhotoPath
    {
        get => localPhotoPath;
        set
        {
            localPhotoPath = value;
            OnPropertyChanged("LocalPhotoPath");
        }
    }


    

    #region Single Selection


    private Product selectedProduct;
    public Product SelectedProduct
    {
        get
        {
            return this.selectedProduct;
        }
        set
        {
            this.selectedProduct = value;
            OnSingleSelectPost(selectedProduct);
            OnPropertyChanged();
        }
    }



    private async void OnSingleSelectPost(Product p)
    {
        if (p != null)
        {
            var navParam = new Dictionary<string, object>
                {
                    {"selectedProduct",p }
                };
            await Shell.Current.GoToAsync("ProductView", navParam);

            SelectedProduct = null;

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
}