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
    public class OrderServiceImplementation : IOrder

    {
        private List<OrdersModel> listOrders = new List<OrdersModel>();
        private OrdersModel order = new OrdersModel();
        private readonly IConfiguration _configuration;
        public OrderServiceImplementation(IConfiguration iconfig)
        {
            _configuration = iconfig;
        }


        public void AddOrders(OrdersModel O)
        {
            string query = @"
INSERT INTO `loreen'sexpress`.`orders` (`idorders`, `shippingCost`, `userId`, `productId`) VALUES (@Idorders, @shCost, @uId, @pId);
    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ExpressConn");
            MySqlDataReader myReader;
            using (MySqlConnection myconn = new MySqlConnection(sqlDataSource))
            {
                myconn.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, myconn))
                {
                    mySqlCommand.Parameters.AddWithValue("@Idorders", System.Guid.NewGuid().ToString());
                    mySqlCommand.Parameters.AddWithValue("@shCost", O.shippingCost);
                    mySqlCommand.Parameters.AddWithValue("@uId", O.userId);
                    mySqlCommand.Parameters.AddWithValue("@pId", O.productId);

                    myReader = mySqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myconn.Close();
                    ReduceQuantity(O.productId);
                }


            }
        }
        public async Task<List<OrdersModel>> GetOrders()
        {
            string query = @"
    SELECT * FROM `loreen'sexpress`.Orders";
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

                            order.orderId = myReader.GetGuid(0);
                            order.shippingCost = myReader.GetInt32(1);
                            order.userId = myReader.GetString(2);
                            order.productId = myReader.GetString(3);

                            listOrders.Add(order);
                        }
                    }
                    table.Load(myReader);
                    myconn.Close();
                    return listOrders;
                }


            }

        }
        public void DeleteOrders(string id)
        {
            string query = @"
DELETE FROM `loreen'sexpress`.`orders` WHERE (`idorders` = @ordersId);";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ExpressConn");
            MySqlDataReader myReader;
            using (MySqlConnection myconn = new MySqlConnection(sqlDataSource))
            {
                myconn.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, myconn))
                {

                    mySqlCommand.Parameters.AddWithValue("@ordersId", id);
                    myReader = mySqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myconn.Close();
                }

            }
        }


        public void UpdateOrders(OrdersModel O)
        {
            string query = @"
UPDATE `loreen'sexpress`.`orders` SET `shippingCost` = @sc WHERE (`idorders` = @idOrder);
";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ExpressConn");
            MySqlDataReader myReader;
            using (MySqlConnection myconn = new MySqlConnection(sqlDataSource))
            {
                myconn.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, myconn))
                {
                    mySqlCommand.Parameters.AddWithValue("@sc", O.shippingCost);
                    mySqlCommand.Parameters.AddWithValue("@idOrder", O.orderId);

                   

                    myReader = mySqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myconn.Close();
                
                
                }

            }
        }
        
        public int GetQuantity(string productId)
        {
            int quantity = 0;
            string query = @"
    SELECT Quantity FROM `loreen'sexpress`.products WHERE (`idProducts` = @productId)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ExpressConn");
            MySqlDataReader myReader;
            using (MySqlConnection myconn = new MySqlConnection(sqlDataSource))
            {
                myconn.Open();

                using (MySqlCommand mySqlCommand = new MySqlCommand(query, myconn))
                {
                    mySqlCommand.Parameters.AddWithValue("@productId", productId);
                    myReader = mySqlCommand.ExecuteReader();
                    //table.Load(myReader);
                    while (myReader.Read())
                    {
                        var newQuantity = myReader.GetInt32(0);
                        quantity = Convert.ToInt32(newQuantity);
                    }

                    //quantity = Convert.ToInt32(newQuantity);
                    myconn.Close();
                }
                return quantity;
            }

        }
        public void ReduceQuantity(string productId)
        {
            var quantity = GetQuantity(productId);
            int q = 0;
            string query = @"
    UPDATE `loreen'sexpress`.`products` SET `Quantity` = @quantity WHERE (`idProducts` = @productId);";
            string query2 = @"
    SELECT Quantity FROM `loreen'sexpress`.products WHERE (`idProducts` = @productId)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ExpressConn");
            MySqlDataReader myReader;
            using (MySqlConnection myconn = new MySqlConnection(sqlDataSource))
            {
                myconn.Open();

                using (MySqlCommand mySqlCommand = new MySqlCommand(query, myconn))
                {
                    mySqlCommand.Parameters.AddWithValue("@productId", productId);
                    mySqlCommand.Parameters.AddWithValue("@quantity", quantity - 1);


                    myReader = mySqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myconn.Close();
                }

            }

        }
    }
}