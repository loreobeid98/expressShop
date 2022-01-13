using Loreen_s_Express.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoreenExpress.Interfaces
{
   public interface IClient
    {
        public Task<List<ClientModel>> GetClients();

        public void addClient(ClientModel C);
        public void updateClient(ClientModel C);
        public void DeleteClient(string id);
    }
}
