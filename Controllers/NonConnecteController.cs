using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TP2.Models;

namespace TP2.Controllers
{
    public class NonConnecteController : Controller
    {
        private readonly ILogger<NonConnecteController> _logger;

        public NonConnecteController(ILogger<NonConnecteController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}