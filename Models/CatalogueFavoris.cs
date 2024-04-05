using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

namespace TP2_final
{
    public class CatalogueFavoris
    {
        private List<Favoris> listeFav;

        public CatalogueFavoris()
        {
            listeFav = new List<Favoris>();
        }

        /// <summary>Lit un fichier JSON et retourne une liste d'objet userId</summary>
        /// <param name="nomFichierSauvegarde">Le nom du fichier à lire</param>
        /// <returns>si la déserialisation à fonctionner</returns>
        public bool Ajouter(string nomFichierSauvegarde, string pathSource)
        {
            bool ok = true;
            try
            {
                listeFav = JsonConvert.DeserializeObject<List<Favoris>>(File.ReadAllText(@$"{pathSource}\{nomFichierSauvegarde}"));
            }
            catch (Exception err)
            {
                ok = false;
            }

            return ok;
        }

        /// <summary>Sauvegarde la liste des utilisateurs dans un fichier JSON</summary>
        /// <param name="nomFichierSauvegarde">Le nom du fichier de sauvegarde</param>
        /// <returns>True si la sauvegarde est réussi, false autrement</returns>
        /// <exception cref="">Lancée si le dossier de sauvegarde n'existe pas</exception>
        /// <exception cref="Exception"> Lancée en cas d'erreur inattendue</exception>
        public bool Sauvegarder(string nomFichierSauvegarde, string pathSource)
        {
            bool ok = true;
            try
            {
                File.WriteAllText(@$"{pathSource}\{nomFichierSauvegarde}", JsonConvert.SerializeObject(listeFav, Formatting.Indented));
            }
            
            catch (Exception err)
            {
                ok = false;
            }
            return ok;
        }


        public override string ToString()
        {
            string retVal = "";
            foreach (Favoris fav in listeFav)
            {
                retVal += fav.ToString() + "\n\n";
            }
            return retVal;
            //return listeFav.Select(x => x.ToString()).Aggregate((a, b) => a += b + "\n\n");
        }

        /// <summary>Ajout un objet favoris dans le catalogue favoris</summary>
        /// <param name="fav">L'userId à ajouter</param>
        public void Ajouter(Favoris fav)
        {
            listeFav.Add(fav);
        }

        /// <summary>Permet d'obtenir la liste des favoris</summary>
        /// <returns>La liste des favoris</returns>
        public List<Favoris> GetCatalogue() { return listeFav; }


        public void Supprimer() {
            listeFav.Clear();
        }
        

        public bool Supprimer(Favoris fav) {
            return listeFav.Remove(fav);
        }


        public List<Favoris> GetFavoris(Media media)
        {
            return listeFav.Where(x => x.MediaId == media.getId()).ToList();
        }


        public List<Favoris> GetFavoris(Utilisateur user)
        {
            return listeFav.Where(x => x.UserId == user.getId()).ToList();
        }
    }
}
