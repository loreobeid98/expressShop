using Loreen_s_Express.Models;
using LoreenExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoreenExpress.Interfaces
{
   public interface IRefund
    {
        public Task<List<RefundModel>> GetRefund();

        public void addRefund(RefundModel R);
        public void updateRefund(RefundModel R);
        public void DeleteRefund(string id);
    }
}
