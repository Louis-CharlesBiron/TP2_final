using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TP2_final.Models;
using System.Text.RegularExpressions;
using System.Text.Encodings.Web;
using System.Web;

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
            catalogueUtilisateur = new CatalogueUtilisateur();
            catalogueUtilisateur.Ajouter(pathUtilisateurs, pathDossierSerial);
        }

        public IActionResult Index()
        {
            return View();
        }

        private string ValidationPseudo(String pseudo)
        {
            //TODO, vérification && // filtrage
            pseudo = Filtrage(pseudo);
            return !(pseudo.Length >= 5 &&
               pseudo.Length <= 50 &&
               new Regex("[a-z]", RegexOptions.IgnoreCase).IsMatch(pseudo) &&
               new Regex("[0-9]+").IsMatch(pseudo) &&
               !new Regex("[^a-zA-Z0-9]+").IsMatch(pseudo)
               )
               ? ""
               : pseudo;
        }

        private string ValidationPassword(String pw)
        {
            //TODO
            pw = Filtrage(pw);

            return pw;
        }

        private string ValidationPrenom(String prenom)
        {
            //TODO
            return prenom;
        }


        private string ValidationNom(String nom)
        {
            //TODO
            return nom;
        }

        [HttpPost]
        public IActionResult Connecte()
        {
            string pseudo = ValidationPseudo(Request.Form["connPseudo"]);
            string mdp = ValidationPassword(Request.Form["connMdp"]);
            Utilisateur? user = catalogueUtilisateur.GetUtilisateurByPseudo(pseudo);

            //Validation champs
            if (string.IsNullOrEmpty(pseudo)) return RedirectToAction("Erreur", "NonConnecte", new { msg = "Le pseudo est invalide" });
            if (string.IsNullOrEmpty(mdp)) return RedirectToAction("Erreur", "NonConnecte", new { msg = "Le mot de passe est invalide" });

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
            string prenom = ValidationPrenom(Request.Form["insPrenom"]);
            string nom = ValidationNom(Request.Form["insNomFamille"]);
            string pseudo = ValidationPseudo(Request.Form["insPseudo"]);
            string mdp = ValidationPassword(Request.Form["insMdp"]);

            // si champs sont valides
            if (string.IsNullOrEmpty(pseudo)) return RedirectToAction("Erreur", "NonConnecte", new { msg = "Le pseudo est invalide" });
            if (string.IsNullOrEmpty(mdp)) return RedirectToAction("Erreur", "NonConnecte", new { msg = "Le mot de passe est invalide" });
            if (string.IsNullOrEmpty(prenom)) return RedirectToAction("Erreur", "NonConnecte", new { msg = "Le prénom est invalide" });
            if (string.IsNullOrEmpty(nom)) return RedirectToAction("Erreur", "NonConnecte", new { msg = "Le nom de famille est invalide" });

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