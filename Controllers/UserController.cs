using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TP2_final.Models;

namespace TP2_final.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private Catalogue catalogue;
        private CatalogueFavoris fav = new CatalogueFavoris();

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;

            catalogue = new Catalogue();

        }



        public IActionResult Catalogue()
        {
            return View(catalogue);
        }

        public IActionResult Favoris()
        {
            return View();
        }

        public IActionResult Fiche()
        {
            return View();
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