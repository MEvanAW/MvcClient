using Microsoft.AspNetCore.Mvc;
using MvcClient.Application;
using MvcClient.Dtos.Catalog;
using MvcClient.Enums;

namespace MvcClient.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _catalogService;
        private const string _catalogState = "CatalogState";
        private const string _catalogName = "CatalogName";

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            if (TempData.ContainsKey(_catalogState))
            {
                ViewData[_catalogState] = TempData[_catalogState];
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
            TempData[_catalogState] = CatalogState.Created;
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
            TempData[_catalogState] = CatalogState.Edited;
            TempData[_catalogName] = catalogEditDto.Name;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var catalogDeleteDto = new CatalogDeleteDto
            {
                Id = id
            };
            string name = await _catalogService.Delete(catalogDeleteDto);
            TempData[_catalogState] = CatalogState.Deleted;
            TempData[_catalogName] = name;
            return RedirectToAction("Index");
        }
    }
}
