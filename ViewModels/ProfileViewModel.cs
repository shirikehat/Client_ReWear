using Client_ReWear.Models;
using Client_ReWear.Services;
using Client_ReWear.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace Client_ReWear.ViewModels;

[QueryProperty("TheUser", "selectedUser")]
public class ProfileViewModel : ViewModelBase
{
    private ReWearWebAPI proxy;

    private IServiceProvider serviceProvider;

    public ProfileViewModel(ReWearWebAPI proxy, IServiceProvider serviceProvider)
	{
        this.serviceProvider = serviceProvider;
        this.proxy = proxy;
        Products = new ObservableCollection<Product>();
        
        EditCommand = new Command(OnEdit);
        
    }

    private User theUser;
    public User TheUser
    {
        get => theUser;
        set
        {
            theUser = value;
            ReadDataFromServer();
            OnPropertyChanged("TheUser");
        }
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
        User u = TheUser;
        Name = u.UserName;
        PhotoURL = u.FullProfileImageUrl;
        LocalPhotoPath = "";
        InServerCall = false;
        
        List<Product>? products = await proxy.GetProducts(TheUser);
        
        if (products != null)
        {
            Products.Clear();
            foreach (Product p in products)
            {
                Products.Add(p);
            }
        }
        
    }
    #endregion


    #region Name
    private string name;
    public string Name
    {
        get => name;
        set
        {
            name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    #endregion


    #region Photo

    private string photoURL;

    public string PhotoURL
    {
        get => photoURL;
        set
        {
            photoURL = value;
            OnPropertyChanged("PhotoURL");
        }
    }

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
            if (value != null)
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

    public ICommand EditCommand { get; }
    private void OnEdit()
    {
        // Navigate to the Edit Profile View page
        ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<EditProfile>());
    }
}
