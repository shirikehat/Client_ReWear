using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client_ReWear.Services;
namespace Client_ReWear.Models
{
    public class Product
    {
        public int ProductCode { get; set; }

        public int Price { get; set; } 

        public int UserId { get; set; } 
        public string UserName { get; set; }
        public string Size { get; set; }

        public int StatusId { get; set; } 

        public int TypeId { get; set; }
        public string Store { get; set; }
        public string Description { get; set; }

        public string ProductImagePath { get; set; } = "";
        public string FullImagePath
        {
            get
            {
                return ReWearWebAPI.ImageBaseAddress + ProductImagePath;
            }
        }
        public Product() 
        {
            
        }
    }
}
