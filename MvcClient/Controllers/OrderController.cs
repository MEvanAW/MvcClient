using Microsoft.AspNetCore.Mvc;
using MvcClient.Models.Order;

namespace MvcClient.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View(new List<OrderModel>());
        }
    }
}
