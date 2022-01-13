using Loreen_s_Express.Models;
using LoreenExpress.Interfaces;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LoreenExpress.Implementations
{
    public class ClientServiceImplementation : IClient

    {
        private List<ClientModel> listClient = new List<ClientModel>();
        private ClientModel client = new ClientModel();
        private readonly IConfiguration _configuration;
        public ClientServiceImplementation(IConfiguration iconfig)
        {
            _configuration = iconfig;
        }


        public void addClient(ClientModel C)
        {
            try
            {
                string query = _configuration.GetConnectionString("addClientQ");
                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("ExpressConn");
                MySqlDataReader myReader;
                using (MySqlConnection myconn = new MySqlConnection(sqlDataSource))
                {
                    myconn.Open();
                    using (MySqlCommand mySqlCommand = new MySqlCommand(query, myconn))
                    {
                        mySqlCommand.Parameters.AddWithValue("@clientId", System.Guid.NewGuid().ToString());
                        mySqlCommand.Parameters.AddWithValue("@fname", C.fname);
                        mySqlCommand.Parameters.AddWithValue("@lname", C.lname);
                        mySqlCommand.Parameters.AddWithValue("@pNumber", C.pNumber);
                        mySqlCommand.Parameters.AddWithValue("@Address", C.Address);
                        myReader = mySqlCommand.ExecuteReader();
                        table.Load(myReader);
                        myconn.Close();
                    }

                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public void DeleteClient(string id)
        {
            try
            {
                string query = @"
                       delete  FROM `loreen'sexpress`.client where client.idClient = @clientId ";
                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("ExpressConn");
                MySqlDataReader myReader;
                using (MySqlConnection myconn = new MySqlConnection(sqlDataSource))
                {
                    myconn.Open();
                    using (MySqlCommand mySqlCommand = new MySqlCommand(query, myconn))
                    {

                        mySqlCommand.Parameters.AddWithValue("@clientId", id);
                        myReader = mySqlCommand.ExecuteReader();
                        table.Load(myReader);
                        myconn.Close();
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public async Task<List<ClientModel>> GetClients()
        {
            try
            {
                string query = @"
            SELECT * FROM `loreen'sexpress`.client
            ";
                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("ExpressConn");
                MySqlDataReader myReader;
                using (MySqlConnection myconn = new MySqlConnection(sqlDataSource))
                {
                    myconn.Open();
                    using (MySqlCommand mySqlCommand = new MySqlCommand(query, myconn))
                    {
                        myReader = mySqlCommand.ExecuteReader();
                        if (myReader.HasRows)
                        {
                            while (myReader.Read())
                            {

                                client.ClientId = myReader.GetGuid(0);
                                client.fname = myReader.GetString(1);
                                client.lname = myReader.GetString(2);
                                client.pNumber = myReader.GetInt32(3);
                                client.Address = myReader.GetString(4);
                                listClient.Add(client);
                            }
                        }
                        table.Load(myReader);
                        myconn.Close();
                        return listClient;
                    }


                }
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public void updateClient(ClientModel C)
        {
            try
            {
                string query = @"
                     UPDATE `loreen'sexpress`.`client` SET `Fname` = @fname WHERE (`idClient` = @clientId);";
                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("ExpressConn");
                MySqlDataReader myReader;
                using (MySqlConnection myconn = new MySqlConnection(sqlDataSource))
                {
                    myconn.Open();
                    using (MySqlCommand mySqlCommand = new MySqlCommand(query, myconn))
                    {
                        mySqlCommand.Parameters.AddWithValue("@fname", C.fname);
                        mySqlCommand.Parameters.AddWithValue("@clientId", C.ClientId);


                        myReader = mySqlCommand.ExecuteReader();
                        table.Load(myReader);
                        myconn.Close();
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}