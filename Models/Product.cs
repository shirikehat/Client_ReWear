using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_ReWear.Models
{
    internal class Product
    {
        public int ProductCode { get; set; }

        public int Price { get; set; } 

        public int UserId { get; set; } 

        public string Size { get; set; }

        public int StatusId { get; set; } 

        public int TypeId { get; set; } 

        public Product() { }
    }
}
