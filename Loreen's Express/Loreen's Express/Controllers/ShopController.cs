using Loreen_s_Express.Models;
using LoreenExpress.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LoreenExpress.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ShopController : ControllerBase
    {
        private readonly ILogger<ShopController> _logger;
        private IShop iShop;
        public ShopController(ILogger<ShopController> logger, IShop Ishop)
        {
            _logger = logger;
            iShop = Ishop;


        }
        [HttpGet]
        //  [Route("api/Get")]
        public async Task<IActionResult> GET()
        {
            var shop = await iShop.GetShops();
            return Ok(shop);
        }


        [HttpPost]
        public IActionResult Add(ShopModel S)
        {

            iShop.AddShop(S);
            return Ok("Success");
        }

        [HttpPut]
        public IActionResult Update(ShopModel S)
        {

            iShop.UpdateShop(S);
            return Ok("Success");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {

            iShop.DeleteShop(id);
            return Ok("Success");

        }

    }
}
