using Microsoft.AspNetCore.Mvc;
using MvcClient.Application;
using MvcClient.Dtos.Catalog;

namespace MvcClient.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.Filter(new CatalogFilterDto()));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CatalogCreateDto catalogCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _catalogService.Create(catalogCreateDto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details([FromRoute] Guid id)
        {
            var catalogDetailsDto = new CatalogDetailsDto
            {
                Id = id
            };
            return View(await _catalogService.Details(catalogDetailsDto));
        }
    }
}
