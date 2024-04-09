using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

namespace TP2_final.Models
{
    public class CatalogueUtilisateur
    {

        private List<Utilisateur> listeUtilisateurs;

        public CatalogueUtilisateur()
        {
            listeUtilisateurs = new List<Utilisateur>();
        }

        /// <summary>
        /// Ajoute un nouvel utilisateur dans la liste d'utilisateurs
        /// </summary>
        /// <param name="utilisateur">L'utilisateur à ajouter</param>
        public void Ajouter(Utilisateur utilisateur)
        {
            listeUtilisateurs.Add(utilisateur);
        }


        public List<Utilisateur> GetCatalogue() { return listeUtilisateurs; }

        /// <summary>
        /// Lit un fichier JSON et retourne une liste d'objet userId
        /// </summary>
        /// <param name="nomFichierSauvegarde">Le nom du fichier à lire</param>
        /// <returns>Une liste d'objet userId</returns>
        /// <exception cref="FileNotFoundException"> Lancée lorsque le fichier n'est pas trouvé</exception>
        /// <exception cref="Exception"> Lancée en cas d'erreur inattendue </exception>
        public bool Ajouter(string nomFichierSauvegarde, string pathSource)
        {
            bool ok = true;
            try
            {
                listeUtilisateurs = JsonConvert.DeserializeObject<List<Utilisateur>>(File.ReadAllText(@$"{pathSource}\{nomFichierSauvegarde}"));
            }
            catch (Exception e)
            {
                ok = false;
            }

            return ok;
        }

        /// <summary>
        /// Sauvegarde la liste des utilisateurs dans un fichier JSON
        /// </summary>
        /// <param name="nomFichierSauvegarde">Le nom du fichier de sauvegarde</param>
        /// <returns>True si la sauvegarde est réussi, false autrement</returns>
        /// <exception cref="">Lancée si le dossier de sauvegarde n'existe pas</exception>
        /// <exception cref="Exception"> Lancée en cas d'erreur inattendue</exception>
        public bool Sauvegarder(string nomFichierSauvegarde, string pathSource)
        {
            bool isSauvegarde = true;
            try
            {
                File.WriteAllText(@$"{pathSource}\{nomFichierSauvegarde}", JsonConvert.SerializeObject(listeUtilisateurs, Formatting.Indented));
            }
            catch (Exception err)
            {
                isSauvegarde = false;
            }
            return isSauvegarde;
        }

        public override string ToString()
        {
            string retVal = "";
            foreach (Utilisateur user in listeUtilisateurs)
            {
                retVal += user.ToString() + "\n\n";
            }
            return retVal;
        }

        /// <summary>Retire un utilisateur du catalogue d'utilisateurs</summary>
        /// <param name="user">L'utilisateur à retirer</param>
        /// <returns>Vrai si l'utilisateur est bien retiré, faux autrement</returns>
        public bool Supprimer(Utilisateur user)
        {
            return listeUtilisateurs.Remove(user);
        }

        /// <summary>Retire tout les utilisateurs de la liste d'utilisateurs</summary>
        public void Supprimer()
        {
            listeUtilisateurs.Clear();
        }

        /// <summary>Permet d'accèder à un utilisateur en connaissant son id</summary>
        /// <param name="id">L'identifiant de l'utilisateur</param>
        /// <returns>L'utilisateur spécifié en param ou null s'il n'existe pas</returns>
        public Utilisateur? GetUtilisateur(string id)
        {
            List<Utilisateur> list = listeUtilisateurs.Where(x => x.getId() == id).ToList();
            return list.Count > 0 ? list[0] : null;
        }
        

    }
}
