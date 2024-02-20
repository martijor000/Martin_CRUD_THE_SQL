using Martin_CRUD_THE_SQL.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Martin_CRUD_THE_SQL.Pages.Checkout
{
    public class OrderConfirmationModel : PageModel
    {
        private readonly ILogger<OrderConfirmationModel> _logger;
        public string FirstName { get; set; }
        public int CustomerId { get; set; }
        public string OrderNumber { get; set; }
        public List<Product> Products { get; set; }



        public OrderConfirmationModel(ILogger<OrderConfirmationModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(string firstName, int customerId, string orderNumber, List<Product> products)
        {
            FirstName = firstName;
            CustomerId = customerId;
            OrderNumber = orderNumber;
            Products = products;
        }
    }
}
