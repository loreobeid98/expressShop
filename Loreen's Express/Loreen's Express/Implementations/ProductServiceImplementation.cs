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
    public class ProductServiceImplementation : IProduct

    {
        private List<ProductsModel> listproducts = new List<ProductsModel>();
        private ProductsModel products = new ProductsModel();
        private readonly IConfiguration _configuration;
        public ProductServiceImplementation(IConfiguration iconfig)
        {
            _configuration = iconfig;
        }

        public async Task<List<ProductsModel>> GetProducts()
        {
            string query = @"
    SELECT * FROM `loreen'sexpress`.products";
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

                            products.idProduct = myReader.GetGuid(0);
                            products.Name = myReader.GetString(1);
                            products.Price = myReader.GetInt32(2);
                            products.Quantity = myReader.GetInt32(3);
                            products.ShopId = myReader.GetString(4);


                            listproducts.Add(products);
                        }
                    }
                    table.Load(myReader);
                    myconn.Close();
                    return listproducts;
                }


            }
        }

        public void AddProduct(ProductsModel P)
        {
            string query = @"
INSERT INTO `loreen'sexpress`.`products` (`idProducts`, `Name`, `Price`, `Quantity`, `shopId`) VALUES (@idprod, @name, @price, @quan, @shopId);
    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ExpressConn");
            MySqlDataReader myReader;
            using (MySqlConnection myconn = new MySqlConnection(sqlDataSource))
            {
                myconn.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, myconn))
                {
                    mySqlCommand.Parameters.AddWithValue("@idprod", System.Guid.NewGuid().ToString());
                    mySqlCommand.Parameters.AddWithValue("@name", P.Name);
                    mySqlCommand.Parameters.AddWithValue("@price", P.Price);
                    mySqlCommand.Parameters.AddWithValue("@quan", P.Quantity);
                    mySqlCommand.Parameters.AddWithValue("@shopId", P.ShopId);

                    myReader = mySqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myconn.Close();

                }


            }
        }

        public void updateProduct(ProductsModel P)
        {
            string query = @"
UPDATE `loreen'sexpress`.`products` SET `Price` = @price WHERE (`idProducts` = @idProd);
";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ExpressConn");
            MySqlDataReader myReader;
            using (MySqlConnection myconn = new MySqlConnection(sqlDataSource))
            {
                myconn.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, myconn))
                {
                    mySqlCommand.Parameters.AddWithValue("@price", P.Price);
                    mySqlCommand.Parameters.AddWithValue("@idProd", P.idProduct);


                    myReader = mySqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myconn.Close();
                }

            }
        }

        public void DeleteProduct(string id)
        {
            string query = @"
            delete  FROM `loreen'sexpress`.products where products.idProducts = @productsId ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ExpressConn");
            MySqlDataReader myReader;
            using (MySqlConnection myconn = new MySqlConnection(sqlDataSource))
            {
                myconn.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, myconn))
                {

                    mySqlCommand.Parameters.AddWithValue("@productsId", id);
                    myReader = mySqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myconn.Close();
                }
            }
        }

    }
}