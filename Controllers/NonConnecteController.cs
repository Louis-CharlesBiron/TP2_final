using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TP2_final.Models;

namespace TP2_final.Controllers
{
    public class NonConnecteController : Controller
    {
        private readonly ILogger<NonConnecteController> _logger;

        private const string pathMedias = "s_medias.json", pathUtilisateurs = "s_utilisateurs.json", pathEvaluations = "s_evalutations.json", pathFavoris = "s_favoris.json";
        private static string pathDossierSerial = @$"{Environment.CurrentDirectory}\Donnees";

        private Catalogue catalogue;
        private CatalogueUtilisateur catalogueUtilisateur;
        private CatalogueEvaluation catalogueEvaluation;
        private CatalogueFavoris catalogueFavoris;

        public NonConnecteController(ILogger<NonConnecteController> logger)
        {
            _logger = logger;
            catalogue = new Catalogue();
            catalogueUtilisateur = new CatalogueUtilisateur();
            catalogueEvaluation = new CatalogueEvaluation();
            catalogueFavoris = new CatalogueFavoris();

            catalogue.Ajouter(pathMedias, pathDossierSerial);
            catalogueUtilisateur.Ajouter(pathUtilisateurs, pathDossierSerial);
            catalogueEvaluation.Ajouter(pathEvaluations, pathDossierSerial);
            catalogueFavoris.Ajouter(pathFavoris, pathDossierSerial);
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