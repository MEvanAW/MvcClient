using Microsoft.AspNetCore.Mvc;
using MvcClient.Models.Catalog;

namespace MvcClient.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult Index()
        {
            return View(new List<CatalogListModel>());
        }
    }
}
