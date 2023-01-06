using Microsoft.AspNetCore.Mvc;
using MvcClient.Application;
using MvcClient.Dtos.Basket;
using MvcClient.Models.Catalog;

namespace MvcClient.Controllers
{
    public class BasketController : Controller
    {

        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _basketService.Filter(new BasketFilterDto()));
        }

        [HttpPost]
        public async Task<IActionResult> AddToBasket([FromForm] CatalogAddToBasketModel requestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _basketService.AddToBasket(new CatalogAddToBasketDto(requestModel));
            return RedirectToAction("Index");
        }
    }
}
