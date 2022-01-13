using Loreen_s_Express.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoreenExpress.Interfaces
{
   public interface IOrder
    {
        public Task<List<OrdersModel>> GetOrders();

        public void AddOrders(OrdersModel O);
        public void UpdateOrders(OrdersModel O);
        public void DeleteOrders(string id);

    }
}
