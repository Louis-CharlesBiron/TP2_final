using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using static S_tp1.Types;

namespace S_tp1
{
    public class Media
    {
        [JsonIgnore]
        public const string NOM_DEFAULT = "nom default";
        [JsonIgnore]
        public const Types TYPE_DEFAULT = Types.JAZZ;
        [JsonIgnore]
        public const long DATE_DEFAULT = 1;
        [JsonIgnore]
        public const int DUREE_DEFAULT = 1;
        [JsonIgnore]
        public const string AUTEUR_DEFAULT = "Auteur default";
        [JsonIgnore]
        public const string PRODUCTEUR_DEFAULT = "Productueur default";
        [JsonIgnore]
        public const string EXTRAIT_DEFAULT = "ressources/extraits/default.mp3";
        [JsonIgnore]
        public const string COMPLET_DEFAULT = "ressources/complets/default.mp3";
        [JsonIgnore]
        public const string IMAGE_DEFAULT = "ressources/images/default.png";

        [JsonIgnore]
        private static int nombreIncremente = 0;

        [JsonIgnore]
        private string id;

        private string nom;
        private Types type;
        private long dateRealisation;
        private int duree;
        private string auteur;
        private string producteur;
        private string extrait;
        private string complet;
        private string image;


        [JsonConstructor]
        public Media(string nom, Types type, long dateRealisation, int duree, string auteur, string producteur, string extrait, string complet, string image)
        {
            Nom = nom;
            Type = type; 
            DateRealisation = dateRealisation;
            Duree = duree;
            Auteur = auteur;
            Producteur = producteur;
            Complet = complet;
            Extrait = extrait;
            Image = image;
            this.id = $"{Nom}_{nombreIncremente++}";
        }


        public Media(string nom) : this(nom, TYPE_DEFAULT, DATE_DEFAULT, DUREE_DEFAULT, AUTEUR_DEFAULT, PRODUCTEUR_DEFAULT, EXTRAIT_DEFAULT, COMPLET_DEFAULT, IMAGE_DEFAULT)
        {
            this.Nom = nom;
        }

        //{NOM_DEFAULT}_{nombreIncremente}", 
        public Media() : this($"nom default", TYPE_DEFAULT, DATE_DEFAULT, DUREE_DEFAULT, AUTEUR_DEFAULT, PRODUCTEUR_DEFAULT, EXTRAIT_DEFAULT, COMPLET_DEFAULT, IMAGE_DEFAULT)
        {

        }


        //overrides
        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is Media))
                return false;
            else
                return this.getId() == ((Media)obj).getId();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.getId(), this.Nom);
        }

        // redéfinition des opérateurs
        public static bool operator ==(Media m1, Media m2) => m1.Equals(m2);

        public static bool operator !=(Media m1, Media m2) => !m1.Equals(m2);

        //getters setter
        public string getId()
        {
            return this.id;
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value.Length > 0 && value.Length <= 100 && !(new Regex("[^a-z._ 0-9]", RegexOptions.IgnoreCase).IsMatch(value)) ? value : NOM_DEFAULT; }
        }
        public Types Type
        {
            get { return type; }
            set { this.type = value; }
        }

        public long DateRealisation
        {
            get { return dateRealisation; }
            set {
                this.dateRealisation = value < DateTimeOffset.Now.ToUnixTimeMilliseconds() ? value : DATE_DEFAULT;
            }
        }

        public int Duree
        {
            get { return duree; }
            set { this.duree = value>0 ? value : DUREE_DEFAULT; }
        }

        public string Auteur
        {
            get { return auteur; }
            set { this.auteur = value!="" && value.Length<50 ? value : AUTEUR_DEFAULT; }
        }

        public string Producteur
        {
            get { return producteur; }
            set { this.producteur = value != "" && value.Length < 50 ? value : PRODUCTEUR_DEFAULT; }
        }

        public string Extrait
        {
            get { return extrait; }
            set { this.extrait = value.IndexOf(".mp3") >= 0 ? value : EXTRAIT_DEFAULT;}
        }

        public string Complet
        {
            get { return complet; }
            set {this.complet = value.IndexOf(".mp3") >= 0 ? value : COMPLET_DEFAULT; }
        }

        public string Image
        {
            get { return image; }
            set { this.image = value.IndexOf(".png") >= 0 ? value : IMAGE_DEFAULT;}
        }


        /// <summary>
        /// Calcule la cote (moyenne) à partir des évaluations associées à cet objet (this) média
        /// </summary>
        /// <returns>La cote calculée à partir des évaluations</returns>
        /*public byte GetCote()
        {
            return evaluations.Count > 0 ? (byte)Math.Round((double)(evaluations.Select(x => x.Cote).Aggregate((a, b) => a += b) / evaluations.Count)) : (byte)0;
        }*/

        /// <summary>
        /// Récupère le nom de l'objet média à partir de son identifiant unique
        /// </summary>
        /// <returns>Le nom de l'objet média extrait de son identifiant. Si l'identitfant n'est pas défini, retourne "Nom non défini"</returns>

        public override string ToString()
        {
            return $"id:{this.getId()}\n nom:{this.Nom}\n type:{this.Type}\n complet:{this.Complet}\n image:{this.Image}\n Name: {this.Nom}\n Cote: {/*this.GetCote()*/"TODO maybe"}/100\n Date de realisation: {this.DateRealisation}\n Duree: {this.Duree}\n Auteur: {this.Auteur}\n Producteur: {this.Producteur}";
        }

    }
}