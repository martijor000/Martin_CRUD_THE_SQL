using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Martin_CRUD_THE_SQL.Models;
using Martin_CRUD_THE_SQL.Services;
using System.Collections.Generic;

namespace Martin_CRUD_THE_SQL.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IOrderService _orderService;

        public List<Product> Products { get; set; }

        public List<Product> CheckOutProducts
        {
            get
            {
                var sessionProducts = HttpContext.Session.Get<List<Product>>("CheckOutProducts");
                if (sessionProducts == null)
                {
                    sessionProducts = new List<Product>();
                    HttpContext.Session.Set("CheckOutProducts", sessionProducts);
                }

                return sessionProducts;
            }
            set
            {
                HttpContext.Session.Set("CheckOutProducts", value);
            }
        }

        public IndexModel(ILogger<IndexModel> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
            Products = _orderService.GetAllProducts();
        }

        public IActionResult OnPostAddToCart(int id)
        {
            Product product = Products.Find(p => p.Id == id);
            if (product != null)
            {
                // Retrieve the current CheckOutProducts list, add the product, and then set it back
                var updatedCheckOutProducts = new List<Product>(CheckOutProducts);
                updatedCheckOutProducts.Add(product);
                CheckOutProducts = updatedCheckOutProducts;
            }
            return RedirectToPage();
        }

        public IActionResult OnPostRemoveFromCart(int id)
        {
            var product = CheckOutProducts.Find(p => p.Id == id);
            if (product != null)
            {
                // Retrieve the current CheckOutProducts list, remove the product, and then set it back
                var updatedCheckOutProducts = new List<Product>(CheckOutProducts);
                updatedCheckOutProducts.Remove(product);
                CheckOutProducts = updatedCheckOutProducts;
            }
            return RedirectToPage();
        }
    }
}
