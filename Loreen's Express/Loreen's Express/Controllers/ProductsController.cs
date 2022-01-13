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
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private IProduct iproduct;
        public ProductsController(ILogger<ProductsController> logger, IProduct Iproduct)
        {
            _logger = logger;
            iproduct = Iproduct;


        }
        [HttpGet]
        //  [Route("api/Get")]
        public async Task<IActionResult> GET()
        {
            var order = await iproduct.GetProducts();
            return Ok(order);
        }


        [HttpPost]
        public IActionResult Add(ProductsModel P)
        {

            iproduct.AddProduct(P);
            return Ok("Success");
        }

        [HttpPut]
        public IActionResult Update(ProductsModel P)
        {

            iproduct.updateProduct(P);
            return Ok("Success");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {

            iproduct.DeleteProduct(id);
            return Ok("Success");

        }

    }
}