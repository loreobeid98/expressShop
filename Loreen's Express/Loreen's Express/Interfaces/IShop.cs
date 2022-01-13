using Loreen_s_Express.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoreenExpress.Interfaces
{
   public interface IShop
    {
        public Task<List<ShopModel>> GetShops();

        public void AddShop(ShopModel S);

        public void UpdateShop(ShopModel S);

        public void DeleteShop (string id);
    }
}
