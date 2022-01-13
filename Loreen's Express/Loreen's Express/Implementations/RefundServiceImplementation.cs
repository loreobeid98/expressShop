using Loreen_s_Express.Models;
using LoreenExpress.Interfaces;
using LoreenExpress.Models;
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
    public class RefundServiceImplementation : IRefund

    {
        private List<RefundModel> listRefund = new List<RefundModel>();
        private RefundModel refund = new RefundModel();
        private readonly IConfiguration _configuration;
        public RefundServiceImplementation(IConfiguration iconfig)
        {
            _configuration = iconfig;
        }

        public async Task<List<RefundModel>> GetRefund()
        {
            try
            {
                string query = @"
           SELECT * FROM `loreen'sexpress`.refund
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

                                refund.RefundId = myReader.GetGuid(0);
                                refund.Date = myReader.GetString(1);
                                refund.OrderId = myReader.GetString(2);

                                listRefund.Add(refund);
                            }
                        }
                        table.Load(myReader);
                        myconn.Close();
                        return listRefund;
                    }


                }
            }
            catch (Exception e)
            {

                throw e;
            }

        }


        public void addRefund(RefundModel R)
        {
            try
            {
                string query = @"
INSERT INTO `loreen'sexpress`.`refund` (`idrefund`, `date`, `orderId`) VALUES (@id, @date, @orderId);
";
                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("ExpressConn");
                MySqlDataReader myReader;
                using (MySqlConnection myconn = new MySqlConnection(sqlDataSource))
                {
                    myconn.Open();
                    using (MySqlCommand mySqlCommand = new MySqlCommand(query, myconn))
                    {
                        mySqlCommand.Parameters.AddWithValue("@id", System.Guid.NewGuid().ToString());
                        mySqlCommand.Parameters.AddWithValue("@date", System.DateTime.Today);
                        mySqlCommand.Parameters.AddWithValue("@orderId", R.OrderId);


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
    

        public void updateRefund(RefundModel R)
        {
        try
        {
            string query = @"
                     UPDATE `loreen'sexpress`.`refund` SET `orderId` = @orderId WHERE (`idrefund` = @idref);";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ExpressConn");
            MySqlDataReader myReader;
            using (MySqlConnection myconn = new MySqlConnection(sqlDataSource))
            {
                myconn.Open();
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, myconn))
                {
                    mySqlCommand.Parameters.AddWithValue("@orderId", R.OrderId);
                    mySqlCommand.Parameters.AddWithValue("@idref", R.RefundId);


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

        public void DeleteRefund(string id)
        {
            try
            {
                string query = @"
                       delete  FROM `loreen'sexpress`.refund where refund.idrefund = @refid ";
                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("ExpressConn");
                MySqlDataReader myReader;
                using (MySqlConnection myconn = new MySqlConnection(sqlDataSource))
                {
                    myconn.Open();
                    using (MySqlCommand mySqlCommand = new MySqlCommand(query, myconn))
                    {

                        mySqlCommand.Parameters.AddWithValue("@refid", id);
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
       