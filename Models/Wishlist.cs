using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_ReWear.Models
{
    public class Wishlist
    {
        public int WishlistId { get; set; }

        public int? UserId { get; set; }

        public int? ProductCode { get; set; }

        public virtual Product? ProductCodeNavigation { get; set; }

        public virtual User? User { get; set; }

        public Wishlist() { }

        public Wishlist(Models.Wishlist wishlist)
        {
            this.WishlistId = wishlist.WishlistId;
            this.UserId = wishlist.UserId;
            this.ProductCode = wishlist.ProductCode;

        }
    }
}
