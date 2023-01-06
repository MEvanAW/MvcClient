using Microsoft.AspNetCore.Mvc;
using MvcClient.Models.Basket;

namespace MvcClient.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View(new List<BasketListModel>());
        }
    }
}
