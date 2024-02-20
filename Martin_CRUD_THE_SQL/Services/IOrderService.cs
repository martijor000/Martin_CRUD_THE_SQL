using Martin_CRUD_THE_SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martin_CRUD_THE_SQL.Services
{
    public interface IOrderService
    {
        List<Product> Products { get; set; }
        List<Product> GetAllProducts();
        List<NewCustomerSummary> GetRecentOrders();
        void InsertCustomerAndOrder(Customer customer, Order order, OrderItem orderItem);
    }
}
