using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Martin_CRUD_THE_SQL.Models;
using Martin_CRUD_THE_SQL.Services;
using System;
using System.Collections.Generic;
using System.Linq;

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

            List<Product> orderItems = HttpContext.Session.Get<List<Product>>("CheckOutProducts");

            var orderItemsWithQuantity = orderItems.Select(p => new OrderItem
            {
                ProductId = p.Id,
                Quantity = 1,
                UnitPrice = p.UnitPrice
            }).ToList();

            var order = new Order
            {
                OrderDate = DateTime.Now,
                OrderNumber = GenerateOrderNumber(customer.LastName.Substring(0, 2)),
                TotalAmount = orderItems.Sum(p => p.UnitPrice)
            };

            foreach (var orderItem in orderItemsWithQuantity)
            {
                orderItem.OrderId = order.Id;
            }

            _orderService.InsertCustomerAndOrder(customer, order, orderItemsWithQuantity);

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