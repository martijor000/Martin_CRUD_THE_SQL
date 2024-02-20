using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Martin_CRUD_THE_SQL.Models;
using Martin_CRUD_THE_SQL.Services;
using System.Collections.Generic;

namespace Martin_CRUD_THE_SQL.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly ILogger<CheckoutModel> _logger;

        public List<Product> CheckOutProducts
        {
            get
            {
                return HttpContext.Session.Get<List<Product>>("CheckOutProducts") ?? new List<Product>();
            }
        }

        public CheckoutModel(ILogger<CheckoutModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnPostRemoveFromCart(int id)
        {
            var product = CheckOutProducts.Find(p => p.Id == id);
            if (product != null)
            {
                CheckOutProducts.Remove(product);
                HttpContext.Session.Set("CheckOutProducts", CheckOutProducts);
            }
            return RedirectToPage();
        }

        public IActionResult OnGetProcessOrder()
        {
            // Perform any necessary processing before redirecting to the customer information page
            return RedirectToPage("/CustomerInfo");
        }
    }
}
