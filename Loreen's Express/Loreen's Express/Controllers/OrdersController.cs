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
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private IOrder iOrder;
        public OrdersController(ILogger<OrdersController> logger, IOrder IOrder)
        {
            _logger = logger;
            iOrder = IOrder;


        }
        [HttpGet]
        //  [Route("api/Get")]
        public async Task<IActionResult> GET()
        {
            var order = await iOrder.GetOrders();
            return Ok(order);
        }


        [HttpPost]
        public IActionResult Add(OrdersModel O)
        {

            iOrder.AddOrders(O);
            return Ok("Success");
        }

        [HttpPut]
        public IActionResult Update(OrdersModel o)
        {

           iOrder.UpdateOrders(o);
            return Ok("Success");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {

            iOrder.DeleteOrders(id);
            return Ok("Success");

        }

    }
}

   