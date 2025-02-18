using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_ReWear.Models
{
    public class Cart
    {
        public int CartId { get; set; }

        public int? UserId { get; set; }

        public int? ProductCode { get; set; }

        public virtual Product? ProductCodeNavigation { get; set; }

        public virtual User? User { get; set; }

        public Cart() { }

        public Cart(Models.Cart cart)
        {
            this.CartId = cart.CartId;
            this.UserId = cart.UserId;
            this.ProductCode = cart.ProductCode;

        }
    }
}
