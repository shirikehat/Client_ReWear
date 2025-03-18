using Client_ReWear.Models;
using Client_ReWear.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace Client_ReWear.ViewModels;

public class WishlistViewModel : ViewModelBase
{
    private ReWearWebAPI proxy;

    private IServiceProvider serviceProvider;


    public WishlistViewModel(ReWearWebAPI proxy, IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
        this.proxy = proxy;
        Wishlists = new ObservableCollection<Wishlist>();
        RefreshCommand = new Command(Refresh);
        Remove = new Command<Wishlist>(OnRemove);
        User u = ((App)Application.Current).LoggedInUser;
        User = u;
        IsRefreshing = true;
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


    #region collection view of wishlists
    private ObservableCollection<Wishlist> wishlists;
    public ObservableCollection<Wishlist> Wishlists
    {
        get => wishlists;
        set
        {
            wishlists = value;
            OnPropertyChanged("Wishlists");
        }
    }

    private async void ReadDataFromServer()
    {

        

        List<Wishlist>? wishlists = await proxy.GetWishlist();

        if (wishlists != null)
        {
            Wishlists.Clear();
            foreach (Wishlist w in wishlists)
            {
                Wishlists.Add(w);
            }
        }
        IsRefreshing = false;
    }
    #endregion


    #region Single Selection


    private Wishlist selectedWishlist;
    public Wishlist SelectedWishlist
    {
        get
        {
            return this.selectedWishlist;
        }
        set
        {
            this.selectedWishlist = value;
            if (value != null)
                OnSingleSelectWishlist(selectedWishlist.ProductCodeNavigation);
            OnPropertyChanged();
        }
    }



    private async void OnSingleSelectWishlist(Product w)
    {
        if (w != null)
        {
            var navParam = new Dictionary<string, object>
                {
                    {"selectedProduct",w }
                };
            await Shell.Current.GoToAsync("ProductView", navParam);

            SelectedWishlist = null;

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

    public ICommand Remove { get; }
    public async void OnRemove(Wishlist w)
    {
        if (w != null)
        {
            await this.proxy.RemoveWishlist(w.WishlistId);
            Wishlists.Remove(w);

            await Application.Current.MainPage.DisplayAlert("success", "removed product from wishlist", "ok");
        }

    }
}