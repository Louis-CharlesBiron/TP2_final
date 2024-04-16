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

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
            catalogueUtilisateur = new CatalogueUtilisateur();
            catalogueUtilisateur.Ajouter(pathUtilisateurs, pathDossierSerial);
        }

        public IActionResult Index()
        {
            TempData.Keep("username");

            Utilisateur user = catalogueUtilisateur.GetUtilisateurByPseudo((string)TempData["username"]); // sketchy
            return user is null ? RedirectToAction("Index", "NonConnecte") : user.Role != Role.ADMIN ? RedirectToAction("Index", "User") : View();
        }

        public IActionResult GestionUtilisateurs()
        {
            TempData.Keep("username");
            Utilisateur user = catalogueUtilisateur.GetUtilisateurByPseudo((string)TempData["username"]); // sketchy
            return user is null ? RedirectToAction("Index", "NonConnecte") : user.Role != Role.ADMIN ? RedirectToAction("Index", "User") : View(catalogueUtilisateur);
        }

        public IActionResult Catalogue()
        {
            TempData.Keep("username");
            Utilisateur user = catalogueUtilisateur.GetUtilisateurByPseudo((string)TempData["username"]); // sketchy
            return user is null ? RedirectToAction("Index", "NonConnecte") : user.Role != Role.ADMIN ? RedirectToAction("Index", "User") : View();
        }

        public IActionResult Deco()
        {
            TempData.Clear();
            return RedirectToAction("Index", "NonConnecte");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ConfirmerDelete(string username)
        {
            //afficher message confirmation

            TempData["isConfirmation"] = "true";
            TempData["usernameToDelete"] = username;

            return RedirectToAction("gestionUtilisateurs", "admin");
        }

        public IActionResult DeleteUser(string username)
        {
            catalogueUtilisateur.Supprimer(catalogueUtilisateur.GetUtilisateurByPseudo(username));
            catalogueUtilisateur.Sauvegarder(pathUtilisateurs, pathDossierSerial);

            return RedirectToAction("gestionUtilisateurs", "admin");
        }
    }
}