using Newtonsoft.Json;

namespace TP2_final.Models
{
    public class CatalogueEvaluation
    {

        private List<Evaluation> listeEvaluations;

        public CatalogueEvaluation()
        {
            listeEvaluations = new List<Evaluation>();
        }


        /// <summary>
        /// Supprime le Evaluation en paramètre du catalogue
        /// </summary>
        /// <param name="eval">L'objet Evaluation à supprimer</param>
        /// <returns>True si le Evaluation a été supprimé, false si le Evaluation n'a pas été supprimé</returns>
        public bool Supprimer(Evaluation eval)
        {
            return listeEvaluations.Remove(eval);
        }

        public List<Evaluation> GetCatalogue() { return listeEvaluations; }

        /// <summary>
        /// Ajoute une évaluation dans la liste d'évaluations
        /// </summary>
        /// <param name="eval">L'évaluation à ajouter</param>
        public void Ajouter(Evaluation eval)
        {
            listeEvaluations.Add(eval);
        }

        /// <summary>
        /// Supprime tous les evaluations du catalogue
        /// </summary>
        public void Supprimer()
        {
            listeEvaluations.Clear();
        }

        public List<Evaluation> GetEvalutations(Media media)
        {
            return listeEvaluations.Where(x => x.MediaId == media.getId()).ToList();
        }

        public List<Evaluation> GetEvalutations(Utilisateur user)
        {
            return listeEvaluations.Where(x => x.UserId == user.getId()).ToList();
        }

        /// <summary>
        /// Lit une liste d'évaluations d'un fichier JSON, try catch non inclus
        /// </summary>
        /// <param name="nomFichierSauvegarde"><Le nom du fichier à lire/param>
        /// <returns>Une liste d'évaluations</returns>
        public bool Ajouter(string nomFichierSauvegarde, string pathSource)
        {
            bool ok = true;
            try
            {
                listeEvaluations = JsonConvert.DeserializeObject<List<Evaluation>>(File.ReadAllText(@$"{pathSource}\{nomFichierSauvegarde}"));
            }
            catch (Exception err)
            {
                ok = false;
            }

            return ok;
        }

        /// <summary>
        /// Sauvegarde la liste d'évaluations dans un fichier JSON, try catch non inclus
        /// </summary>
        /// <param name="nomFichierSauvegarde">Le nom du fichier de sauvegarde</param>
        /// <returns>True si le sauvegarde est réussi, false autrement</returns>
        public bool Sauvegarder(string nomFichierSauvegarde, string pathSource)
        {
            bool isSauvegarde = true;
            try
            {
                File.WriteAllText(@$"{pathSource}\{nomFichierSauvegarde}", JsonConvert.SerializeObject(listeEvaluations, Formatting.Indented));
            }
            catch (Exception err)
            {
                isSauvegarde = false;
            }

            return isSauvegarde;
        }

        public byte getCote(Media media)
        {
            List<Evaluation> evals = this.GetEvalutations(media);
            return evals.Count > 0 ? (byte)Math.Round((double)(evals.Select(x => x.Cote).Aggregate((a, b) => a += b) / evals.Count)) : (byte)0;
        }

        public override string ToString()
        {
            string retVal = "";
            foreach (Evaluation eval in listeEvaluations)
            {
                retVal += eval.ToString() + "\n\n";
            }
            return retVal;
        }


    }
}
