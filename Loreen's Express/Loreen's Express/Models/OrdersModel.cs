using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loreen_s_Express.Models
{
    public class OrdersModel
    {
        public Guid orderId { get; set; }
        public int shippingCost { get; set; }
        public string userId { get; set; }
        public string productId { get; set; }

            
        
    }
}
