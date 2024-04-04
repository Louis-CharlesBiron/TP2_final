using Newtonsoft.Json;

namespace S_tp1
{
    /*
     * Regroupe les médias et s'occupe de la sérialisation de l'objet catalogue
     */
    public class Catalogue
    {
        //TODO: 20-02-2024 -> définir le path du fichier de sauvegarde
        private const string MESSAGE_ERREUR = "Erreur! 1D10T-6969: Un événement inattendu s'est produit!";

        //TODO: 20-02-2024 -> maybe singleton, getInstance et le constructeur private maybe
        private static List<Media>? catalogue;

        private string PATH_FICHIER_SAUVEGARDE = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/test.json";

        // Constructeur par défaut
        public Catalogue()
        {
            catalogue = new List<Media>();
        }

        /*
         * ajoute un media passé en paramètre dans le catalogue
         * 
         * @param identifiantMedia -> l'identifiant unique du media à ajouter
         * @return -> retourne vrai si le media a bel et bien été ajouter au catalogue
         */
        // TODO -> check si autre message à retourner
        // TODO -> faire ajouter avec param fichier JSON
        public bool Ajouter(Media media)
        {
            bool isAjouter = false;
            string messageRetour;
            if (!MediaExisteDansCatalogue(media))
            {
                catalogue.Add(media);
                isAjouter = true;
                messageRetour = $"Le media {media.GetNom()} a bel et bien ajouté dans le catalogue";
            }
            else if (MediaExisteDansCatalogue(media))
            {
                messageRetour = $"Le media {media.GetNom()} existe déjà dans le catalogue et n'a pas pu être ajouté";
            }
            else
            {
                ErrorMessage();
                messageRetour = MESSAGE_ERREUR;
            }
            Console.WriteLine(messageRetour);
            return isAjouter;
        }

        /*
         * TODO -> documentation
         */
        public bool Ajouter(string nomDuFichier)
        {
            return true;
        }

        /*
         * remplace un media passé en paramètre par un autre aussi passé en paramètre
         * 
         * @param mediaToAdd -> media à ajouter
         * @param mediaToRemove -> media à remplacer
         * @return -> retourne vrai si le media a bel et bien été remplacé
         */
        public bool Remplacer(Media mediaToAdd, Media mediaToRemove)
        {
            string messageRetour;
            bool isRemplace = false;
            if (!(MediaExisteDansCatalogue(mediaToAdd)) && MediaExisteDansCatalogue(mediaToRemove))
            {
                isRemplace = true;
                messageRetour = $"Le media {mediaToAdd.GetNom()} a été ajouté au catalogue et le media {mediaToRemove.GetNom()} a été supprimé";
                this.Supprimer(mediaToRemove);
                this.Ajouter(mediaToAdd);
            }
            else if (!(MediaExisteDansCatalogue(mediaToRemove)))
            {
                messageRetour = $"Le media {mediaToRemove.GetNom()} n'existe pas dans le catalogue et ne peut donc pas être supprimé!";
            }
            else if (MediaExisteDansCatalogue(mediaToAdd))
            {
                messageRetour = $"Le media {mediaToAdd.GetNom()} existe déjà dans le catalogue et ne peut donc pas être ajouté!";
            }
            else
            {
                ErrorMessage();
                messageRetour = MESSAGE_ERREUR;
            }

            Console.WriteLine(messageRetour);
            return isRemplace;
        }

        /*
         * supprime le media passé en paramètre du catalogue
         * 
         * @param media -> l'identifiant unique du media à supprimer
         * @return -> retourne vrai si le media a bel et bien supprimé
         */
        public bool Supprimer(Media media)
        {
            return true;
        }

        /*
         * supprime le catalogue au complet
         * 
         * @return -> retourne vrai si le catalogue est supprimé correctement
         */
        public bool Supprimer()
        {
            catalogue.Clear();
            bool isSupprime = true;
            string messageRetour = "Le catalogue a été complètement supprimé!";
            if (catalogue.Count != 0)
            {
                isSupprime = false;
                ErrorMessage();
                messageRetour = MESSAGE_ERREUR;
            }

            Console.WriteLine(messageRetour);
            return isSupprime;
        }

        /*
         * sauvegarde le catalogue et le sérialise dans un fichier JSON
         * 
         * @param nomFichierSauvegarde -> le nom du fichier JSON de sauvegarde YOFO
         */
        public void Sauvegarder()
        {
            string test = JsonConvert.SerializeObject(catalogue, Formatting.Indented);
            File.WriteAllText(@PATH_FICHIER_SAUVEGARDE, test);
            Console.WriteLine(test);

        }

        // Méthode Override
        //TODO
        public override string ToString()
        {
            return "(☞ﾟヮﾟ)☞";
        }


        // Méthodes ajoutées

        public bool MediaExisteDansCatalogue(Media media)
        {
            return catalogue.Contains(media);
        }

        //get catalogue
        public List<Media>? getCatalogue() { return catalogue; }


        private void ErrorMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
    }


}

