using Martin_CRUD_THE_SQL.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Martin_CRUD_THE_SQL.Data
{
    internal class db
    {
        private readonly IConfiguration _configuration;

        public db(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string GetConnectionString()
        {
            return _configuration.GetConnectionString("Default");
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ProductList_Lastname", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        ProductName = Convert.ToString(reader["ProductName"]),
                        SupplierId = Convert.ToInt32(reader["SupplierId"]),
                        UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                        Package = Convert.ToString(reader["Package"]),
                        IsDiscontinued = Convert.ToBoolean(reader["IsDiscontinued"])
                    };
                    products.Add(product);
                }
            }

            return products;
        }

        public List<NewCustomerSummary> GetRecentOrders()
        {
            List<NewCustomerSummary> recentOrders = new List<NewCustomerSummary>();

            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("NewCustomerSummary_Lastname", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    NewCustomerSummary order = new NewCustomerSummary
                    {
                        OrderId = Convert.ToInt32(reader["Id"]),
                        OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                        OrderNumber = reader["OrderNumber"].ToString(),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        City = reader["City"].ToString(),
                        Country = reader["Country"].ToString(),
                        Phone = reader["Phone"].ToString()
                    };
                    recentOrders.Add(order);
                }
            }

            return recentOrders;
        }

        public void InsertCustomerAndOrder(Customer customer, Order order, List<OrderItem> orderItem)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();

                foreach (OrderItem oi in orderItem)
                {
                    SqlCommand cmd = new SqlCommand("InsertCustomerAndOrder_Lastname", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@City", customer.City);
                    cmd.Parameters.AddWithValue("@Country", customer.Country);
                    cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                    cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    cmd.Parameters.AddWithValue("@OrderNumber", order.OrderNumber);
                    cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                    cmd.Parameters.AddWithValue("@UnitPrice", oi.UnitPrice);
                    cmd.Parameters.AddWithValue("@Quantity", oi.Quantity);
                    cmd.Parameters.AddWithValue("@ProductID", oi.ProductId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
