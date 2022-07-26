using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Customer;
using SolarCoffee.Services.Order;
using SolarCoffee.Web.Serialization;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Controllers
{
    [ApiController]
    [Route("/orders")]
    public class OrderController : ControllerBase
    {
        public OrderController(
            ILogger<OrderController> logger, 
            IOrderService orderService,
            ICustomerService customerService)
        {
            _logger = logger;
            _orderService = orderService;
            _customerService = customerService;
        }

        [HttpPost("/invoice")]
        public ActionResult GenerateNewOrder([FromBody] InvoiceDataModel invoice)
        {
            _logger.LogInformation("Generating invoice.");
            var order = OrderMapper.SerialiseInvoiceToOrder(invoice);
            order.Customer = _customerService.GetCustomerById(invoice.CustomerId);
            
            return NoContent();
        }
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
    }
}