using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Martin_CRUD_THE_SQL.Models;
using Martin_CRUD_THE_SQL.Services;
using System;
using System.Threading.Tasks;

namespace Martin_CRUD_THE_SQL.Pages.Checkout
{
    public class ProcessOrderModel : PageModel
    {
        private readonly ILogger<ProcessOrderModel> _logger;
        private readonly IOrderService _orderService;

        public ProcessOrderModel(ILogger<ProcessOrderModel> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        public IActionResult OnPost()
        {
            var customer = new Customer
            {
                FirstName = Request.Form["FirstName"],
                LastName = Request.Form["LastName"],
                City = Request.Form["City"],
                Country = Request.Form["Country"],
                Phone = Request.Form["Phone"]
            };

            var order = new Order
            {
                OrderDate = DateTime.Now, 
                OrderNumber = GenerateOrderNumber(customer.LastName.Substring(0,2)),
                TotalAmount = 0.0m
            };

            Product sessionProduct = HttpContext.Session.Get<Product>("SelectedProduct");

            var orderItem = new OrderItem
            {
                ProductId = sessionProduct.Id,
                Quantity = 1,
                UnitPrice = sessionProduct.UnitPrice
            };

            _orderService.InsertCustomerAndOrder(customer, order, orderItem);

            return RedirectToPage("/Checkout/OrderConfirmation");
        }

        private string GenerateOrderNumber(string lastName)
        {
            Random rand = new Random();
            int randomNumber = rand.Next(1, 10000);
            return $"{randomNumber}_{lastName.Substring(0, 2)}";
        }
    }
}
