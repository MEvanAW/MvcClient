using Microsoft.AspNetCore.Mvc;
using MvcClient.Application;
using MvcClient.Dtos.Basket;
using MvcClient.Enums;
using MvcClient.Models.Basket;
using MvcClient.Models.Catalog;

namespace MvcClient.Controllers
{
    public class BasketController : Controller
    {

        private readonly IBasketService _basketService;
        private readonly ILogger<BasketController> _logger;
        private const string _basketState = "BasketState";
        private const string _name = "Name";
        private const string _isError = "IsError";

        public BasketController(IBasketService basketService, ILogger<BasketController> logger)
        {
            _basketService = basketService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Basket Index call.");
            IEnumerable<BasketListModel> model;
            try
            {
                var filterTask = _basketService.Filter(new BasketFilterDto());
                if (TempData.ContainsKey(_basketState))
                {
                    ViewData[_basketState] = TempData[_basketState];
                    ViewData[_name] = TempData[_name];
                }
                model = await filterTask;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("Error getting baskets with the following exception: " + ex.Message + Environment.NewLine +
                    ex.InnerException + Environment.NewLine +
                    ex.StackTrace, ex, this);
                model = new List<BasketListModel>();
                ViewData[_isError] = true;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToBasket([FromForm] CatalogAddToBasketModel requestModel)
        {
            _logger.LogInformation("AddToBasket call for buyer {buyer}.", requestModel.Buyer);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _basketService.AddToBasket(new CatalogAddToBasketDto(requestModel));
            TempData[_basketState] = BasketState.Added;
            TempData[_name] = requestModel.ProductName;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Checkout([FromForm] BasketCheckoutRequest requestModel)
        {
            _logger.LogInformation("Basket Checkout call for buyer {buyer}.", requestModel.Buyer);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _basketService.Checkout(new BasketCheckoutDto(requestModel));
            TempData[_name] = requestModel.Buyer;
            return RedirectToAction("Index", "Order");
        }

        public async Task<IActionResult> Delete(string buyer, Guid id)
        {
            _logger.LogInformation("Basket Delete call for buyer {buyer}.", buyer);
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
