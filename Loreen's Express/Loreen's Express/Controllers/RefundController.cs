using Loreen_s_Express.Models;
using LoreenExpress.Interfaces;
using LoreenExpress.Models;
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
    public class RefundController : ControllerBase
    {
        private readonly ILogger<RefundController> _logger;
        private IRefund irefund;
        public RefundController(ILogger<RefundController> logger, IRefund Irefund)
        {
            _logger = logger;
            irefund = Irefund;


        }
        [HttpGet]

        public async Task<IActionResult> GET([FromHeader] string Authorization)
        {
            try
            {
                if (Authorization == "123")
                {
                    var refund = await irefund.GetRefund();
                    return Ok(refund);
                }
                else return Unauthorized("API Call Not Authorized");
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        [HttpPost]
        public IActionResult Add(RefundModel refund)
        {
            try
            {
                irefund.addRefund(refund);
                return Ok("Success");
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpPut]
        public IActionResult Update(RefundModel refund)
        {
            try
            {
                irefund.updateRefund(refund);
                return Ok("Success");
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                irefund.DeleteRefund(id);
                return Ok("Success");

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        }
}
