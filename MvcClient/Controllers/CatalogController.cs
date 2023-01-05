﻿using Microsoft.AspNetCore.Mvc;
using MvcClient.Application;
using MvcClient.Dtos.Catalog;
using MvcClient.Models.Catalog;

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

        public IActionResult Details([FromRoute] Guid id)
        {
            return View(new CatalogDetailsModel
            {
                Id = id
            });
        }
    }
}
