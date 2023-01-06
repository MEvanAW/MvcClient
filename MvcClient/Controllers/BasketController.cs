using Microsoft.AspNetCore.Mvc;
using MvcClient.Application;
using MvcClient.Dtos.Basket;
using MvcClient.Enums;
using MvcClient.Models.Catalog;

namespace MvcClient.Controllers
{
    public class BasketController : Controller
    {

        private readonly IBasketService _basketService;
        private const string _basketState = "BasketState";
        private const string _name = "Name";

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<IActionResult> Index()
        {
            if (TempData.ContainsKey(_basketState))
            {
                ViewData[_basketState] = TempData[_basketState];
                ViewData[_name] = TempData[_name];
            }
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
            TempData[_basketState] = BasketState.Added;
            TempData[_name] = requestModel.ProductName;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string buyer, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var basketDeleteDto = new BasketDeleteDto
            {
                Buyer = buyer,
                CacheId = id
            };
            TempData[_basketState] = BasketState.Deleted;
            TempData[_name] = buyer;
            await _basketService.Delete(basketDeleteDto);
            return RedirectToAction("Index");
        }
    }
}
