using Client_ReWear.Models;
using Client_ReWear.Services;
using System.Collections.ObjectModel;
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

        InServerCall = false;

        List<Wishlist>? wishlists = await proxy.GetWishlist(User);

        if (wishlists != null)
        {
            Wishlists.Clear();
            foreach (Wishlist w in wishlists)
            {
                Wishlists.Add(w);
            }
        }

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
            OnSingleSelectWishlist(selectedWishlist);
            OnPropertyChanged();
        }
    }



    private async void OnSingleSelectWishlist(Wishlist w)
    {
        if (w != null)
        {
            var navParam = new Dictionary<string, object>
                {
                    {"selectedWishlist",w }
                };
            await Shell.Current.GoToAsync("ProductView", navParam);

            selectedWishlist = null;

        }
    }

    #endregion

}