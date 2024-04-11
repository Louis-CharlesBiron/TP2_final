using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TP2_final.Models;

namespace TP2_final.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private const string pathMedias = "s_medias.json", pathUtilisateurs = "s_utilisateurs.json", pathEvaluations = "s_evalutations.json", pathFavoris = "s_favoris.json";
        private static string pathDossierSerial = @$"{Environment.CurrentDirectory}\Donnees";

        private CatalogueUtilisateur catalogueUtilisateur;
        private bool isSerializationToDo;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
            if (isSerializationToDo) {
                isSerializationToDo = false;
                catalogueUtilisateur = new CatalogueUtilisateur();

                catalogueUtilisateur.Ajouter(pathUtilisateurs, pathDossierSerial);
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GestionUtilisateurs()
        {
            return View(catalogueUtilisateur);
        }

        public IActionResult Catalogue()
        {
            return View();
        }

        public IActionResult Fiche()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void confirmationSupprimer(String idk)
        {
            Console.WriteLine("fonction confirmationSupprimer appelé " + idk);
        }
    }
}