using System;
using System.Collections.Generic;
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

        public ProductPageViewModel(ReWearWebAPI proxy, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.proxy = proxy;
            CartCommand = new Command(OnCart);
            WishlistCommand = new Command(OnWishlist);
        }


        public int Price { get; set; }

        private int username;
        public int Username
        {
            get=> username;
            set => username = value;
        }

        public string Size { get; set; }

        public int StatusId { get; set; }

        private string type;
        public string Type
        {
            get => type;
            set => type = value;
        }

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
