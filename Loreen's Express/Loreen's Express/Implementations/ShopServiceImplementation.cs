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
    public class ShopServiceImplementation : IShop

    {
        private List<ShopModel> listShop = new List<ShopModel>();
        private ShopModel shop = new ShopModel();
        private readonly IConfiguration _configuration;
        public ShopServiceImplementation(IConfiguration iconfig)
        {
            _configuration = iconfig;
        }

        public async Task<List<ShopModel>> GetShops()
        {
            string query = @"
            SELECT * FROM `loreen'sexpress`.shop
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

                            shop.shopId = myReader.GetGuid(0);
                            shop.Name = myReader.GetString(1);
                            shop.Address = myReader.GetString(2);
                            shop.Category = myReader.GetString(3);
                            listShop.Add(shop);
                        }
                    }
                    table.Load(myReader);
                    myconn.Close();
                    return listShop;
                }
            }
        }

        public void AddShop(ShopModel S)
        {
            string query = @"
INSERT INTO `loreen'sexpress`.`shop` (`idShop`, `Name`, `Address`, `Category`) VALUES (@idShop, @name, @address, @category);

           ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ExpressConn");
            MySqlDataReader myReader;
            using (MySqlConnection myconn = new MySqlConnection(sqlDataSource))
            {
                myconn.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, myconn))
                {
                    mySqlCommand.Parameters.AddWithValue("@idShop", System.Guid.NewGuid().ToString());
                    mySqlCommand.Parameters.AddWithValue("@name", S.Name);
                    mySqlCommand.Parameters.AddWithValue("@address", S.Address);
                    mySqlCommand.Parameters.AddWithValue("@category", S.Category);

                    myReader = mySqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myconn.Close();
                }

            }
        }

        public void UpdateShop(ShopModel S)
        {
            string query = @"
UPDATE `loreen'sexpress`.`shop` SET `Name` = @name WHERE (`idShop` = @id);
";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ExpressConn");
            MySqlDataReader myReader;
            using (MySqlConnection myconn = new MySqlConnection(sqlDataSource))
            {
                myconn.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, myconn))
                {
                    mySqlCommand.Parameters.AddWithValue("@name", S.Name);
                    mySqlCommand.Parameters.AddWithValue("@id", S.shopId);


                    myReader = mySqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myconn.Close();
                }
            }
        }

        public void DeleteShop(string id)
        {

            string query = @"
            delete  FROM `loreen'sexpress`.shop where shop.idShop = @shopId ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ExpressConn");
            MySqlDataReader myReader;
            using (MySqlConnection myconn = new MySqlConnection(sqlDataSource))
            {
                myconn.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, myconn))
                {

                    mySqlCommand.Parameters.AddWithValue("@shopId", id);
                    myReader = mySqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myconn.Close();
                }
            }
        }

    }
}
       