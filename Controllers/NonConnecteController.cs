using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TP2_final.Models;
using System.Text.RegularExpressions;
using System.Text.Encodings.Web;
using System.Web;
using Newtonsoft.Json.Linq;

namespace TP2_final.Controllers
{
    public class NonConnecteController : Controller
    {
        private readonly ILogger<NonConnecteController> _logger;

        private const string pathMedias = "s_medias.json", pathUtilisateurs = "s_utilisateurs.json", pathEvaluations = "s_evalutations.json", pathFavoris = "s_favoris.json";
        private static string pathDossierSerial = @$"{Environment.CurrentDirectory}\Donnees";

        private CatalogueUtilisateur catalogueUtilisateur;
        public NonConnecteController(ILogger<NonConnecteController> logger)
        {
            _logger = logger;

            // Déserialisation
            catalogueUtilisateur = new CatalogueUtilisateur();
            catalogueUtilisateur.Deserialiser(pathUtilisateurs, pathDossierSerial);
        }

        /// <summary>
        /// Aller à la page index
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Tentative de connection, vérifie si le pseudo et mot de passe sont valides.
        /// Affiche les erreurs ou connecte l'utilisateur
        /// </summary>
        [HttpPost]
        public IActionResult Connecte()
        {
            string pseudo = Utilitaire.ValidationPseudo((string)(TempData["temp_cPseudo"] = (string)Request.Form["connPseudo"]));
            string mdp = Utilitaire.ValidationPassword((string)(TempData["temp_cMdp"] = (string)Request.Form["connMdp"]));
            Utilisateur? user = catalogueUtilisateur.GetUtilisateurByPseudo(pseudo);

            //Validation champs
            string erreurs = "";
            if (string.IsNullOrEmpty(pseudo)) erreurs += "\nLe pseudo est invalide";
            if (string.IsNullOrEmpty(mdp)) erreurs += "\nLe mot de passe est invalide";
            if (!string.IsNullOrEmpty(erreurs)) return RedirectToAction("Erreur", "NonConnecte", new { msg = erreurs });

            //Validation connection
            if (user is null) return RedirectToAction("Erreur", "NonConnecte", new { msg = "L'utilisateur n'existe pas" });
            else if (user.MotDePasse != mdp) return RedirectToAction("Erreur", "NonConnecte", new { msg = "Le mot de passe est invalide" });
            else
            {
                Console.WriteLine($"CONN pseudo:{pseudo}, mdp:{mdp}, id:{user.getId()}");
                TempData.Clear();
                TempData["username"] = user.Pseudo;
                TempData.Keep();

                return RedirectToAction("Index", user.Role.ToString().ToLower());
            }
        }

        /// <summary>
        /// Tentative de connection, vérifie si le prenom, le nom, le pseudo et mot de passe sont valides.
        /// Affiche les erreurs ou crée et connecte l'utilisateur
        /// </summary>
        [HttpPost]
        public IActionResult Inscrire()
        {
            string prenom = Utilitaire.ValidationPrenom((string)(TempData["temp_iPrenom"] = (string)Request.Form["insPrenom"]));
            string nom = Utilitaire.ValidationNom((string)(TempData["temp_iNomFamille"] = (string)Request.Form["insNomFamille"]));
            string pseudo = Utilitaire.ValidationPseudo((string)(TempData["temp_iPseudo"] = (string)Request.Form["insPseudo"]));
            string mdp = Utilitaire.ValidationPassword((string)(TempData["temp_iMdp"] = (string)Request.Form["insMdp"]));

            string erreurs = "";
            // si champs sont valides
            if (string.IsNullOrEmpty(pseudo)) erreurs += "\nLe pseudo est invalide";
            if (string.IsNullOrEmpty(mdp)) erreurs += "\nLe mot de passe est invalide";
            if (string.IsNullOrEmpty(prenom)) erreurs += "\nLe prénom est invalide";
            if (string.IsNullOrEmpty(nom)) erreurs += "\nLe nom de famille est invalide";
            if (!string.IsNullOrEmpty(erreurs)) return RedirectToAction("Erreur", "NonConnecte", new { msg = erreurs });

            // si user existe déja
            if (catalogueUtilisateur.GetUtilisateurByPseudo(pseudo) is not null) return RedirectToAction("Erreur", "NonConnecte", "Un utilisateur ayant le même pseudo existe déja");
            else
            {
                //Création new user avec données d'inscription
                Utilisateur newUser = new Utilisateur(pseudo, mdp, nom, prenom, Utilisateur.ROLE_DEFAULT);
                catalogueUtilisateur.Ajouter(newUser);
                catalogueUtilisateur.Sauvegarder(pathUtilisateurs, pathDossierSerial);
                TempData.Clear();
                TempData["username"] = newUser.Pseudo;
                TempData.Keep("username");
                return RedirectToAction("Index", newUser.Role.ToString().ToLower());
            }
        }

        /// <summary>
        /// Affiche les messages d'erreurs détecté sur les champs, par le serveur
        /// </summary>
        /// <param name="msg"></param>
        public IActionResult Erreur(string msg)
        {
            TempData["erreurs"] = msg;
            return RedirectToAction("Index", "NonConnecte");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}