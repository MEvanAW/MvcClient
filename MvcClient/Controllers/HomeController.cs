using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcClient.Models;

namespace MvcClient.Controllers
{
    public class HomeController : Controller
    {
        private const string PRIVACY_LOG = "Privacy policy call.";
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Home Index call.");
            return View();
        }

        [Authorize]
        public IActionResult Login()
        {
            _logger.LogInformation("Login call.");
            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            _logger.LogInformation("Logout call.");
            return SignOut(new AuthenticationProperties
            {
                RedirectUri = ""
            }, "Cookies", "oidc");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation(PRIVACY_LOG);
            return View();
        }
    }
}