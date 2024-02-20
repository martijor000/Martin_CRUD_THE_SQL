using Martin_CRUD_THE_SQL.Data;
using Martin_CRUD_THE_SQL.Models;
using Martin_CRUD_THE_SQL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Services
{
    public class OrderService : IOrderService
    {
        private readonly db _dataAccess;
        private readonly IConfiguration _configuration;
        public List<Product> Products { get; set; }

        public OrderService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dataAccess = new db(_configuration);
        }

        public List<Product> GetAllProducts()
        {
            return _dataAccess.GetAllProducts();
        }

        public List<NewCustomerSummary> GetRecentOrders()
        {
            return _dataAccess.GetRecentOrders();
        }

        public void InsertCustomerAndOrder(Customer customer, Order order, List<OrderItem> orderItems)
        {
            _dataAccess.InsertCustomerAndOrder(customer, order, orderItems);
        }
    }
}
