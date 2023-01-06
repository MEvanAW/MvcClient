using Microsoft.AspNetCore.Mvc;
using MvcClient.Application;
using MvcClient.Dtos.Catalog;

namespace MvcClient.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _catalogService;
        private const string _isAfterCreate = "IsAfterCreate";
        private const string _catalogName = "CatalogName";

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            if (TempData.ContainsKey(_isAfterCreate))
            {
                ViewData[_isAfterCreate] = TempData[_isAfterCreate];
            }
            if (TempData.ContainsKey(_catalogName))
            {
                ViewData[_catalogName] = TempData[_catalogName];
            }
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
            TempData[_isAfterCreate] = true;
            TempData[_catalogName] = catalogCreateDto.Name;
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

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] Guid id)
        {
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _catalogService.Update(catalogEditDto);
            return RedirectToAction("Index");
        }
    }
}
