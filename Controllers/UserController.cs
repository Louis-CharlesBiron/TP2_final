using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TP2_final.Models;

namespace TP2_final.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private const string pathMedias = "s_medias.json", pathUtilisateurs = "s_utilisateurs.json", pathEvaluations = "s_evalutations.json", pathFavoris = "s_favoris.json";
        private static string pathDossierSerial = @$"{Environment.CurrentDirectory}\Donnees";

        private static Catalogue catalogue;
        private static CatalogueUtilisateur catalogueUtilisateur;
        private static CatalogueFavoris catalogueFavoris;
        private static Catalogues catalogues;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
            catalogue = new Catalogue();
            catalogueUtilisateur = new CatalogueUtilisateur();
            catalogueFavoris = new CatalogueFavoris();

            catalogue.Deserialiser(pathMedias, pathDossierSerial);
            catalogueUtilisateur.Deserialiser(pathUtilisateurs, pathDossierSerial);
            catalogueFavoris.Deserialiser(pathFavoris, pathDossierSerial);

            catalogues = new Catalogues
            {
                Favoris = catalogueFavoris,
                Medias = catalogue,
                Users = catalogueUtilisateur
            };
        }
   
        /// <summary>
        /// déconnecte l'utilisateur et supprime ses données de session
        /// </summary>
        public IActionResult Deco()
        {
            TempData.Clear();
            return RedirectToAction("Index", "NonConnecte");
        }

        /// <summary>
        /// Aller à la page index
        /// </summary>
        public IActionResult Index()
        {
            TempData.Keep("username");
            Utilisateur user = catalogueUtilisateur.GetUtilisateurByPseudo((string)TempData["username"]);
            return user is null || user.Role != Role.USER ? RedirectToAction("Index", "NonConnecte") : View(catalogues);
        }

        /**
         * @return -> la page des favoris si retrouve l'utilisateur, sinon retourne la page non connecté
         */
        public IActionResult Favoris()
        {
            TempData.Keep("username");
            Utilisateur user = catalogueUtilisateur.GetUtilisateurByPseudo((string)TempData["username"]);
            return user is null || user.Role != Role.USER ? RedirectToAction("Index", "NonConnecte") : View(catalogues);
        }

        /**
         * affiche la fiche complète du média choisi
         * @param nom -> le nom du média que l'on veux voir la fiche complète
         * @return -> la fiche complète du média choisi
         */
        public IActionResult Fiche(string nom)
        {
            TempData.Keep("username");
            ViewData["nomMedia"] = nom;
            Utilisateur user = catalogueUtilisateur.GetUtilisateurByPseudo((string)TempData["username"]);
            return user is null || user.Role != Role.USER ? RedirectToAction("Index", "NonConnecte") : View(catalogues);
        }

        /**
         * @param nomMedia -> le nom du média à ajouter aux favoris
         * @return -> la page index
         */
        public IActionResult AjouterFavoris(string nomMedia)
        {
            Favoris fav = new Favoris((string)TempData["username"], nomMedia);
            catalogueFavoris.Ajouter(fav);
            catalogueFavoris.Sauvegarder(pathFavoris, pathDossierSerial);
            TempData["messageOn"] = "true";
            TempData["add"] = "true";
            TempData["favoAdd"] = nomMedia;
            return RedirectToAction("index");
        }

        /**
         * @param nomMedia -> le nom du média à retirer des favoris
         * @return -> la page favoris
         */
        public IActionResult RetirerFavoris(string nomMedia, string page)
        {
            catalogueFavoris.Supprimer(catalogueFavoris.GetFavoris((string)TempData["username"], nomMedia));
            catalogueFavoris.Sauvegarder(pathFavoris, pathDossierSerial);
            TempData["messageOn"] = "true";
            TempData["remove"] = "true";
            TempData["favoRemove"] = nomMedia;

            if (page == "fav")
            {
                return RedirectToAction("Favoris", "User");
            }
            else if (page == "fiche")
            {
                return RedirectToAction("Fiche", "User");
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
            
        }

        /**
         * rafraichie la page des medias avec une confirmation de suppression du media choisi
         * @param media -> media à supprimer eds favoris
         * @return -> la page user/medias
         */
        public IActionResult ConfirmerDelete(string NomMedia, string page)
        {
            // Affiche message de confirmation de suppression d'utilisateur
            TempData["isConfirmation"] = "true";
            TempData["favoToDelete"] = NomMedia;

            if (page == "fav")
            {
                return RedirectToAction("Favoris", "User");
            }
            else if (page == "fiche")
            {
                return RedirectToAction("Fiche", "User");
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }

        /**
         * rafraichie la page des medias pour annuler une suppression de favori
         * @return -> la page utilisateur/index
         */
        public IActionResult AnnulerDeleteUser()
        {
            // Fermer la boite de confirmation de suppression d'utilisateur
            return RedirectToAction("index", "User");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}