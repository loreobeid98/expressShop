using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoreenExpress.Models
{
    public class RefundModel
    {
        public Guid RefundId { get; set; }
        public string Date { get; set; }
        public string OrderId { get; set; }
       
    }
}
