using Loreen_s_Express.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoreenExpress.Interfaces
{
   public interface IProduct
    {
        public Task<List<ProductsModel>> GetProducts();

        public void AddProduct(ProductsModel P);

        public void updateProduct(ProductsModel P);
        public void DeleteProduct(string id);
    }
}
