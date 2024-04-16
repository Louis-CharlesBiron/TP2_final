using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TP2_final.Models;
using System.Text.RegularExpressions;

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

        private string ValidationPseudo(String pseudo) {
            //TODO, vérification && // filtrage

            return !(pseudo.Length >= 5 &&
               pseudo.Length <= 50 &&
               new Regex("[a-z]", RegexOptions.IgnoreCase).IsMatch(pseudo) &&
               new Regex("[0-9]+").IsMatch(pseudo) &&
               !new Regex("[^a-zA-Z0-9]+").IsMatch(pseudo)
               )
               ? pseudo
               : "";
        }

        private string ValidationPassword(String pw) {
            return "!Soleil01";
        }

        private string ValidationPrenom(String prenom) {
            return prenom;
        }


        private string ValidationNom(String nom) {
            return nom;
        }

        [HttpPost]
        public IActionResult Connecte()
        {
            string pseudo = ValidationPseudo(Request.Form["connPseudo"]);
            string mdp = ValidationPassword(Request.Form["connMdp"]);
            //Validation champs
            if (string.IsNullOrEmpty(pseudo)) return RedirectToAction("Erreur", "NonConnecte", new {msg="Le pseudo est invalide"});
            if (string.IsNullOrEmpty(mdp)) return RedirectToAction("Erreur", "NonConnecte", new {msg="Le mot de passe est invalide"});

            Utilisateur user = catalogueUtilisateur.GetUtilisateurByPseudo(pseudo);

            //Validation connection
            if (user is null) return RedirectToAction("Erreur", "NonConnecte", new {msg="L'utilisateur n'existe pas"});
            else if (user.MotDePasse != mdp) return RedirectToAction("Erreur", "NonConnecte", new {msg="Le mot de passe est invalide"});  
            else {
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
            // TODO CHECK IF ALL INPUTS ARE GOOD
            // TODO CHECK IF USER ALREADY EXISTS
            if (false/*TODO*/)
            {
                Console.WriteLine($"TODO, ERREUR INSCRIPTION, inputs invalides:{"'TODO'"}, user existe deja:{"'TODO'"}");
            }
            else
            {
                Utilisateur newUser = new Utilisateur(Request.Form["insPseudo"], Request.Form["insMdp"], Request.Form["insNomFamille"], Request.Form["insPrenom"], Utilisateur.ROLE_DEFAULT);
                //chek si le user existe pas
                String pseudo = Request.Form["insPseudo"];

                Utilisateur user = catalogueUtilisateur.GetUtilisateurByPseudo(pseudo);
                if (user is not null) {
                    return RedirectToAction("Erreur", "NonConnecte", "Un utilisateur ayant le même pseudo existe déja");
                }
                else
                {
                    // TODO SERIALISATION
                    //TODO CHECK IF DESERIALISATION EST PAS CHIÉE PAR LES STATICS _x
                    Console.WriteLine($"no verification, New user: {newUser.ToString()}");

                    TempData.Clear();
                    TempData["username"] = newUser.Pseudo;
                    TempData.Keep("username");

                    return RedirectToAction("Index", newUser.Role.ToString().ToLower());
                }
            }
        }

        public IActionResult Erreur(string msg) {Console.WriteLine("EERRREUR "+msg);
            // give partialViewErreur the error message
            TempData["erreurs"] = msg;
            TempData["isErreurs"] = "true";

            return RedirectToAction("Index", "NonConnecte");   
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}