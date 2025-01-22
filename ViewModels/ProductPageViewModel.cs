using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Client_ReWear.Models;
using Client_ReWear.Services;



namespace Client_ReWear.ViewModels
{
    public class ProductPageViewModel:ViewModelBase
    {
        public ReWearWebAPI proxy;
        private readonly IServiceProvider serviceProvider;


        private Product product;
        public Product Product
        {
            get => product;
            set
            {
                if (product != value)
                {
                    product = value;
                    InItFieldsDataAsync();
                    OnPropertyChanged(nameof(Product));
                }
            }
        }

        public ProductPageViewModel(ReWearWebAPI proxy, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.proxy = proxy;

            

            LocalPhotoPath = "";
            CartCommand = new Command(OnCart);
            WishlistCommand = new Command(OnWishlist);
            
        }

        #region productvals

        private int price;
        public int Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        private string size;
        public string Size
        {
            get => size;
            set
            {
                size = value;
                OnPropertyChanged(nameof(Size));
            }
        }

        private string status;
        public string Status
        {
            get => status;
            set
            {
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        private string type;
        public string Type
        {
            get => type;
            set
            {
                type = value;
                OnPropertyChanged(nameof(Type));
            }
        }


        private string desc;
        public string Desc
        {
            get => desc;
            set
            {
                desc = value;
                OnPropertyChanged(nameof(Desc));
            }
        }


        private string store;
        public string Store
        {
            get => store;
            set
            {
                store = value;
                OnPropertyChanged(nameof(Store));
            }
        }
        #endregion


        private string username;
        public string Username
        {
            get=> username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        #region ProfileImage

        private string photoURL;

        public string PhotoURL
        {
            get => photoURL;
            set
            {
                photoURL = value;
                OnPropertyChanged(nameof(PhotoURL));
            }
        }

        private string localPhotoPath;

        public string LocalPhotoPath
        {
            get => localPhotoPath;
            set
            {
                localPhotoPath = value;
                OnPropertyChanged(nameof(LocalPhotoPath));
            }
        }
        #endregion

        #region ProductImage

        private string productURL;

        public string ProductURL
        {
            get => productURL;
            set
            {
                productURL = value;
                OnPropertyChanged(nameof(productURL));
            }
        }

        public ICommand ProfileCommand { get; }
        public async void OnProfile(User p)
        {
            if (p != null)
            {
                var navParam = new Dictionary<string, object>
                {
                    {"selectedUser",p }
                };
                await Shell.Current.GoToAsync("ProfileView", navParam);
                //SelectedPost = null;

            }


        }
        #endregion
        



        #region In it Fields with data
        //Define a method to initialize the fields with data

        private async void InItFieldsDataAsync()
        {
            int userId = (int)product.UserId;
            int typeId = (int)product.TypeId;
            int statusId = (int)product.StatusId;
            int productId = (int)product.ProductCode;
            User u = await proxy.GetUser(userId);
            PrType t = await proxy.GetType(typeId);
            Status s = await proxy.GetStatus(statusId);


            ProductURL = product.FullImagePath;
            Username = u.UserName;
            PhotoURL = u.FullProfileImageUrl;
            Size = product.Size;
            Price = product.Price;
            Type = t.Name;
            Status = s.Name;
            Desc = product.Description;
            Store = product.Store;

        }
        #endregion


        public ICommand CartCommand { get; }
        public ICommand WishlistCommand { get; }

        private async void OnCart()
        {

            //If the add succeed, display a message
            string Msg = "added product to cart!";
            await Application.Current.MainPage.DisplayAlert("cart", Msg, "ok");
        }

        private async void OnWishlist()
        {
            //If the add succeed, display a message
            string Msg = "added product to wishlist!";
            await Application.Current.MainPage.DisplayAlert("wishlist", Msg, "ok");
        }
    }
}
