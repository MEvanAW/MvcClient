using Microsoft.AspNetCore.Mvc;
using MvcClient.Application;
using MvcClient.Dtos.Order;
using MvcClient.Models.Order;

namespace MvcClient.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _orderService.Filter(new OrderFilterDto()));
        }

        public async Task<IActionResult> Details([FromRoute] Guid id)
        {
            return View(new OrderDetailModel
            {
                Id = id
            });
        }
    }
}
