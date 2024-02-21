using DataLibrary.Services;
using Martin_CRUD_THE_SQL.Models;
using Martin_CRUD_THE_SQL.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Martin_CRUD_THE_SQL.Pages.Checkout
{
    public class OrderConfirmationModel : PageModel
    {
        private readonly ILogger<OrderConfirmationModel> _logger;
        private readonly IOrderService _orderService;
        public string FirstName { get; set; }
        public int CustomerId { get; set; }
        public string OrderNumber { get; set; }
        public List<NewCustomerSummary> NewCustomerSummaries { get; set; }



        public OrderConfirmationModel(ILogger<OrderConfirmationModel> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        public void OnGet()
        {
            NewCustomerSummaries = _orderService.GetRecentOrders();
        }
    }
}
