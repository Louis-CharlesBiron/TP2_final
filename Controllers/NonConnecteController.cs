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

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Filtre et valide le pseudo passé en paramètre
        /// </summary>
        /// <param name="pseudo"></param>
        /// <returns>le pseudo valide ou string vide ("")</returns>
        private string ValidationPseudo(string pseudo)
        {
            pseudo = Filtrage(pseudo);
            return !(pseudo.Length >= 5 &&
               pseudo.Length <= 50 &&
               new Regex("[a-z]", RegexOptions.IgnoreCase).IsMatch(pseudo) &&
               new Regex("[0-9]+").IsMatch(pseudo) &&
               !new Regex("[^a-zA-Z0-9]+").IsMatch(pseudo)) ? "" : pseudo;
        }


        private string ValidationPassword(string pw)
        {
            pw = Filtrage(pw);
            return !(pw.Length >= 5 && pw.Length <= 100 &&
                new Regex("[0-9]+").IsMatch(pw) &&
                new Regex("[a-z]").IsMatch(pw) &&
                new Regex("[A-Z]").IsMatch(pw) &&
                new Regex("[^a-zA-Z0-9\\&><]+").IsMatch(pw)) ? "" : pw;
        }
        private string ValidationPrenom(string prenom)
        {
            prenom = Filtrage(prenom);
            return !(prenom.Length > 1 && prenom.Length <= 50 &&
                new Regex("[a-z -]", RegexOptions.IgnoreCase)
                .IsMatch(prenom)) ? "" : prenom;
        }
        private string ValidationNom(string nom)
        {
            nom = Filtrage(nom);
            return !(nom.Length > 1 && nom.Length <= 50 &&
                new Regex("[a-z -]", RegexOptions.IgnoreCase)
                .IsMatch(nom)) ? "" : nom;
        }

        [HttpPost]
        public IActionResult Connecte()
        {
            string pseudo = ValidationPseudo((string)(TempData["temp_cPseudo"] = (string)Request.Form["connPseudo"]));
            string mdp = ValidationPassword((string)(TempData["temp_cMdp"] = (string)Request.Form["connMdp"]));
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

        [HttpPost]
        public IActionResult Inscrire()
        {
            string prenom = ValidationPrenom((string)(TempData["temp_iPrenom"] = (string)Request.Form["insPrenom"]));
            string nom = ValidationNom((string)(TempData["temp_iNomFamille"] = (string)Request.Form["insNomFamille"]));
            string pseudo = ValidationPseudo((string)(TempData["temp_iPseudo"] = (string)Request.Form["insPseudo"]));
            string mdp = ValidationPassword((string)(TempData["temp_iMdp"] = (string)Request.Form["insMdp"]));

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

        public string Filtrage(string input)
        {
            /*
             * conversion entité html
             * enlever les echappements
             * enlever les espaces blancs avant et apres
             */
            return HttpUtility.HtmlEncode(Regex.Unescape(input.Trim()));
        }
    }
}