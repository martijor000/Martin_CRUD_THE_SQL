using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Martin_CRUD_THE_SQL.Pages.Checkout
{
    public class OrderConfirmationModel : PageModel
    {
        private readonly ILogger<OrderConfirmationModel> _logger;

        public OrderConfirmationModel(ILogger<OrderConfirmationModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
