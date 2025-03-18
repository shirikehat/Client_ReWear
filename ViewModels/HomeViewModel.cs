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
        FilteredProducts = new ObservableCollection<Product>();
        searchText = "";
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
        List<Product>? products = await proxy.GetAllProducts();

        if (products != null)
        {
            Products.Clear();
            foreach (Product p in products)
            {
                Products.Add(p);
            }
        }
        Filter();
        IsRefreshing = false;
    }
    #endregion


    

    


    

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

    #region filter
    

    

    
    

    private ObservableCollection<Product> filteredProducts;
    public ObservableCollection<Product> FilteredProducts
    {
        get => filteredProducts;
        set
        {
            filteredProducts = value;
            OnPropertyChanged();
        }
    }

    //Search bar text
    private string searchText;
    public string SearchText
    {
        get => searchText;
        set
        {
            searchText = value;
            Filter();
            OnPropertyChanged();
        }
    }

    //this method filter the products based on the search text 
    private void Filter()
    {
        try
        {
            filteredProducts.Clear();
            //Sort the tasks by urgency level
            products.OrderByDescending(p => p.ProductCode);
            int maxPrice = 0;

            int.TryParse(searchText, out maxPrice);
            foreach (var product in products)
            {
                if (product.Size.Contains(SearchText) || product.Store.Contains(SearchText) ||
                    product.UserName.Contains(SearchText) || /*product.TypeId== PrType.TypeCode ||*/
                    product.Price <= maxPrice || string.IsNullOrEmpty(SearchText))
                {

                    FilteredProducts.Add(product);
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        

    }


    #endregion



}