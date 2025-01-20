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
            CartCommand = new Command(OnCart);
            WishlistCommand = new Command(OnWishlist);
            
        }


        public int Price { get; set; }

        private string username;
        public string Username
        {
            get=> username;
            set
            {
                username = value;
                OnPropertyChanged("Username");
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

        #region PostImage

        private string productURL;

        public string ProductURL
        {
            get => productURL;
            set
            {
                productURL = value;
                OnPropertyChanged("ProductURL");
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



        public string Size { get; set; }

        public int StatusId { get; set; }

        private string type;
        public string Type
        {
            get => type;
            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }


        #region In it Fields with data
        //Define a method to initialize the fields with data

        private async void InItFieldsDataAsync()
        {
            int userId = (int)product.UserId;
            User u = await proxy.GetUser(userId);

            ProductURL = product.ProductImagePath;
           
            Username = u.UserName;
            PhotoURL = u.ProfileImagePath;

        }
        #endregion


        public ICommand CartCommand { get; }
        public ICommand WishlistCommand { get; }

        private void OnCart()
        {

        }

        private void OnWishlist()
        {

        }
    }
}
