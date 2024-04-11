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

        private CatalogueUtilisateur catalogueUtilisateur;
        private bool isSerializationToDo = true;

        public NonConnecteController(ILogger<NonConnecteController> logger)
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

        [HttpPost]
        public IActionResult Connecte() {
            String pseudo =  Request.Form["connPseudo"];
            String mdp = Request.Form["connMdp"];

            Utilisateur user = catalogueUtilisateur.GetUtilisateurByPseudo(pseudo);
            if (user is null || user.MotDePasse != mdp) {
                Console.WriteLine($"ERREUR DE CONN, user is null:{user is null} | mauvais pw: {(user?.MotDePasse != mdp)}");
                return View("MARCHE PAS, JAR SA EXPLOSE C SUR");
            } else
            {
                Console.WriteLine($"CONN pseudo:{pseudo}, mdp:{mdp}, id:{user.getId()}");
                TempData.Clear();
                TempData["user_id"] = user.getId();
                TempData["username"] = user.Pseudo;
                TempData.Keep();

                return RedirectToAction("Index", user.Role.ToString().ToLower(), catalogueUtilisateur);
            }
        }

        [HttpPost]
        public IActionResult Inscrire() {



            return RedirectToAction("Index", "user", catalogueUtilisateur);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}