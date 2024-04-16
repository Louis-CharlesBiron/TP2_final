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

            // Déserialisation
            catalogueUtilisateur = new CatalogueUtilisateur();
            catalogueUtilisateur.Ajouter(pathUtilisateurs, pathDossierSerial);
        }

        public IActionResult Index()
        {
            // Redirection vers la View index et vérificaiton des la validité des informations d'identification
            TempData.Keep("username");
            Utilisateur user = catalogueUtilisateur.GetUtilisateurByPseudo((string)TempData["username"]);
            return user is null ? RedirectToAction("Index", "NonConnecte") : user.Role != Role.ADMIN ? RedirectToAction("Index", "User") : View();
        }

        public IActionResult GestionUtilisateurs()
        {
            // Redirection vers la View GestionUtilisateurs avec le model catalogueUtilisateur et vérificaiton des la validité des informations d'identification
            TempData.Keep("username");
            Utilisateur user = catalogueUtilisateur.GetUtilisateurByPseudo((string)TempData["username"]);
            return user is null ? RedirectToAction("Index", "NonConnecte") : user.Role != Role.ADMIN ? RedirectToAction("Index", "User") : View(catalogueUtilisateur);
        }

        public IActionResult Catalogue()
        {
            // Redirection vers la View Catalogue et vérificaiton des la validité des informations d'identification
            TempData.Keep("username");
            Utilisateur user = catalogueUtilisateur.GetUtilisateurByPseudo((string)TempData["username"]);
            return user is null ? RedirectToAction("Index", "NonConnecte") : user.Role != Role.ADMIN ? RedirectToAction("Index", "User") : View();
        }

        public IActionResult Deco()
        {
            // Déconnecte l'utilisateur et le renvoit à la page de connexion
            TempData.Clear();
            return RedirectToAction("Index", "NonConnecte");
        }

        public IActionResult ConfirmerDelete(string username)
        {
            // Affiche message de confirmation de suppression d'utilisateur
            TempData["isConfirmation"] = "true";
            TempData["usernameToDelete"] = username;

            return RedirectToAction("gestionUtilisateurs", "admin");
        }

        public IActionResult DeleteUser(string username)
        {
            // Suppression d'utilisateur à la suite de la confirmation
            catalogueUtilisateur.Supprimer(catalogueUtilisateur.GetUtilisateurByPseudo(username));
            catalogueUtilisateur.Sauvegarder(pathUtilisateurs, pathDossierSerial);

            return RedirectToAction("gestionUtilisateurs", "admin");
        }

        public IActionResult AnnulerDeleteUser()
        {
            // Fermer la boite de confirmation de suppression d'utilisateur
            return RedirectToAction("gestionUtilisateurs", "admin");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}