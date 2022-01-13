using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loreen_s_Express.Models
{
    public class ProductsModel
    {
        public Guid idProduct { get; set; }
        public String Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string ShopId { get; set; }




    }
}
