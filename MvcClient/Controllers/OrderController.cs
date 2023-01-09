using Microsoft.AspNetCore.Mvc;
using MvcClient.Application;
using MvcClient.Dtos.Order;
using MvcClient.Models.Order;

namespace MvcClient.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;
        private const string _isDelete = "IsDelete";
        private const string _name = "Name";
        private const string _isError = "IsError";

        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Order Index call.");
            if (TempData.ContainsKey(_isDelete))
            {
                ViewData[_isDelete] = TempData[_isDelete];
            }
            else if (TempData.ContainsKey(_name))
            {
                ViewData[_name] = TempData[_name];
            }
            IEnumerable<OrderModel> orderModels;
            try
            {
                orderModels = await _orderService.Filter(new OrderFilterDto());
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("Error getting orders with the following exception: " + ex.Message + Environment.NewLine +
                    ex.InnerException + Environment.NewLine +
                    ex.StackTrace, ex, this);
                orderModels = new List<OrderModel>();
                ViewData[_isError] = true;
            }
            return View(orderModels);
        }

        public async Task<IActionResult> Details([FromRoute] Guid id)
        {
            _logger.LogInformation("Order Details {guid} call.", id);
            var orderDetailDto = new OrderDetailDto {
                Id = id
            };
            return View(await _orderService.Details(orderDetailDto));
        }

        public async Task<IActionResult> Delete()
        {
            _logger.LogInformation("Order Delete call.");
            await _orderService.Delete();
            TempData[_isDelete] = true;
            return RedirectToAction("Index");
        }
    }
}
