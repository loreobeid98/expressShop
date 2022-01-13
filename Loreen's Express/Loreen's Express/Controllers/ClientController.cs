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
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private IClient iClient;
        public ClientController(ILogger<ClientController> logger, IClient iclient)
        {
            _logger = logger;
            iClient = iclient;


        }
        [HttpGet]

        public async Task<IActionResult> GET([FromHeader] string Authorization)
        {
            try
            {
                if (Authorization == "123")
                {
                    var client = await iClient.GetClients();
                    return Ok(client);
                }
                else return Unauthorized("API Call Not Authorized");
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        [HttpPost]
        public IActionResult Add(ClientModel client)
        {
            try
            {
                iClient.addClient(client);
                return Ok("Success");
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpPut]
        public IActionResult Update(ClientModel client)
        {
            try
            {
                iClient.updateClient(client);
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
                iClient.DeleteClient(id);
                return Ok("Success");

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        }
}
