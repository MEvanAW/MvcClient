using Microsoft.AspNetCore.Mvc;
using MvcClient.Application;
using MvcClient.Dtos.Order;

namespace MvcClient.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View(_orderService.Filter(new OrderFilterDto()));
        }
    }
}
