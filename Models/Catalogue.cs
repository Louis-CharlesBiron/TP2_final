using Newtonsoft.Json;

namespace TP2_final.Models
{
    /*
     * Regroupe les Médias et s'occupe de la sérialisation de l'objet catalogue
     */
    public class Catalogue
    {

        private static List<Media> catalogue;

        

        // Constructeur par défaut
        public Catalogue()
        {
            catalogue = new List<Media>();
        }

        /// <summary>
        /// Ajoute un objet média dans le catalogue
        /// </summary>
        /// <param name="media">L'objet média à ajouter</param>
        public void Ajouter(Media media)
        {
            catalogue.Add(media);
        }

        /// <summary>
        /// remplace un média déjà présent dans le catalogue par un nouveau média, si le média a remplacer n'existe pas, ajoute le nouveau media quand même
        /// </summary>
        /// <param name="mediaToAdd">Le nouvel objet média à ajouter dans le catalogue</param>
        /// <param name="mediaToRemove">L'objet média à remplacer dans le catalogue</param>
        public void Remplacer(Media mediaToAdd, Media mediaToRemove)
        {
            catalogue.Remove(mediaToRemove); 
            catalogue.Add(mediaToAdd);
        }

        /// <summary>
        /// Supprime le Media en paramètre du catalogue
        /// </summary>
        /// <param name="media">L'objet Media à supprimer</param>
        /// <returns>True si le Media a été supprimé, false si le Media n'a pas été supprimé</returns>
        public bool Supprimer(Media media)
        {
            return catalogue.Remove(media);
        }

        /// <summary>
        /// Supprime tous les médias du catalogue
        /// </summary>
        public void Supprimer()
        {
            catalogue.Clear();
        }

        /// <summary>
        /// Lit in fichier JSON du chemin spécifié et ajoute les médias à la liste des médias du catalogue
        /// </summary>
        /// <param name="nomFichierSauvegarde">Le chemin du fichier JSON</param>
        /// <returns>Le catalogue rempli d'objet média</returns>
        public bool Ajouter(string nomFichierSauvegarde, string pathSource)
        {
            bool ok = true;
            try {
                catalogue = JsonConvert.DeserializeObject<List<Media>>(File.ReadAllText(@$"{pathSource}\{nomFichierSauvegarde}"));
            }
            catch (Exception e) {
                ok = false;
            }

            return ok;
        }


        /// <summary>
        /// Sauvegarde l'état actuel du catalogue dans un fichier JSON.
        /// </summary>
        /// <param name="nomFichierSauvegarde">Le nom du fichier à sauvegarder</param>
        /// <returns>True si la sauvegarde est réussi, false autrement</returns>
        public bool Sauvegarder(string nomFichierSauvegarde, string pathSource)
        {
            bool isSauvegarde = true;
            try
            {
                File.WriteAllText(@$"{pathSource}\{nomFichierSauvegarde}", JsonConvert.SerializeObject(catalogue, Formatting.Indented));
            }
            catch (Exception err) {
                isSauvegarde = false;
            }
            
            return isSauvegarde;
        }

        // Méthode Override

        public override string ToString()
        {
            string retVal = "";
            foreach (Media media in catalogue)
            {
                retVal += media.ToString()+"\n\n";
            }
            return retVal;
        }


        public List<Media> GetCatalogue() { return catalogue; }



        /// <summary>
        /// Récupère un média à partir de son identifiant
        /// </summary>
        /// <param name="id">l'indentifiantMedia du Media à récuprérer</param>
        /// <returns>Le média coresspondant à l'identififant spécifié</returns>
        public Media? GetMedia(string id)
        {
            List<Media> list = catalogue.Where(x => x.getId() == id).ToList();
            return list.Count > 0 ? list[0] : null;
        }

    }


}

