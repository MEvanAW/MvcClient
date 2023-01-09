using Microsoft.AspNetCore.Mvc;
using MvcClient.Application;
using MvcClient.Dtos.Catalog;
using MvcClient.Enums;
using MvcClient.Models.Basket;
using MvcClient.Models.Catalog;

namespace MvcClient.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ILogger<CatalogController> _logger;
        private const string _catalogState = "CatalogState";
        private const string _catalogName = "CatalogName";
        private const string _isError = "IsError";

        public CatalogController(ICatalogService catalogService, ILogger<CatalogController> logger)
        {
            _catalogService = catalogService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Catalog Index call.");
            IEnumerable<CatalogListModel> model;
            try
            {
                var filterTask = _catalogService.Filter(new CatalogFilterDto());
                if (TempData.ContainsKey(_catalogState))
                {
                    ViewData[_catalogState] = TempData[_catalogState];
                    ViewData[_catalogName] = TempData[_catalogName];
                }
                model = await filterTask;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("Error getting catalogs with the following exception: " + ex.Message + Environment.NewLine +
                    ex.InnerException + Environment.NewLine +
                    ex.StackTrace, ex, this);
                model = new List<CatalogListModel>();
                ViewData[_isError] = true;
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            _logger.LogInformation("Catalog Create Get call.");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CatalogCreateDto catalogCreateDto)
        {
            _logger.LogInformation("Catalog Create Post call.");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _catalogService.Create(catalogCreateDto);
            TempData[_catalogState] = CatalogState.Created;
            TempData[_catalogName] = catalogCreateDto.Name;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details([FromRoute] Guid id)
        {
            _logger.LogInformation("Catalog Details call.");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var catalogDetailsDto = new CatalogDetailsDto
            {
                Id = id
            };
            return View(await _catalogService.Details(catalogDetailsDto));
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] Guid id)
        {
            _logger.LogInformation("Catalog Edit Get call.");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var catalogDetailsDto = new CatalogDetailsDto
            {
                Id = id
            };
            var catalogEditDto = new CatalogEditDto(await _catalogService.Details(catalogDetailsDto));
            return View(catalogEditDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] CatalogEditDto catalogEditDto)
        {
            _logger.LogInformation("Catalog Edit Post call.");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _catalogService.Update(catalogEditDto);
            TempData[_catalogState] = CatalogState.Edited;
            TempData[_catalogName] = catalogEditDto.Name;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Catalog Delete {guid} call.", id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var catalogDeleteDto = new CatalogDeleteDto
            {
                Id = id
            };
            string name = await _catalogService.Delete(catalogDeleteDto);
            TempData[_catalogState] = CatalogState.Deleted;
            TempData[_catalogName] = name;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AddToBasket([FromRoute] Guid id)
        {
            _logger.LogInformation("Catalog Get Add {guid} ToBasket call.", id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var catalogDetailsDto = new CatalogDetailsDto
            {
                Id = id
            };
            var catalogAddToBasketModel = new CatalogAddToBasketModel(await _catalogService.Details(catalogDetailsDto));
            return View(catalogAddToBasketModel);
        }
    }
}
