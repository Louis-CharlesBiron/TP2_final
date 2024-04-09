using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace TP2_final.Models
{
    public class Utilisateur
    {
        [JsonIgnore]
        public const string PASSWORD_PAR_DEFAUT_PAS_BON = "mot_de_passe_default";
        [JsonIgnore]
        public const string PSEUDO_DEFAULT = "pseudoDefaut";
        [JsonIgnore]
        public const string NOM_DEFAULT = "nomDefaut";
        [JsonIgnore]
        public const string PRENOM_DEFAULT = "prenomDefaut";
        [JsonIgnore]
        public const Role ROLE_DEFAULT = Role.UTILISATEUR;

        [JsonIgnore]
        private static int nombreIncremente = 0;

        //attributs
        [JsonIgnore]
        private string id;

        private string pseudo;
        private string motDePasse;
        private string nom;
        private string prenom;
        private Role role;


        [JsonConstructor]
        public Utilisateur(String pseudo, String motDePasse, String nom, String prenom, Role role)
        {
            Pseudo = pseudo;
            MotDePasse = motDePasse;
            Nom = nom;
            Prenom = prenom;
            Role = role;
            this.id = $"{Pseudo}_{nombreIncremente++}";
        }


        public Utilisateur(string pseudo, string motDePasse) : this(pseudo, motDePasse, NOM_DEFAULT, PRENOM_DEFAULT, ROLE_DEFAULT)
        {
            Pseudo = pseudo;
            MotDePasse = motDePasse;
        }

        //Constructeur par défaut
        public Utilisateur() : this(PSEUDO_DEFAULT, PASSWORD_PAR_DEFAUT_PAS_BON, NOM_DEFAULT, PRENOM_DEFAULT, ROLE_DEFAULT)
        {

        }

        //getters & setters

        public string getId() {
            return this.id;
        }

        public string Pseudo
        {
            get { return pseudo; }
            // Pseudo doit contenir seulement des chiffres et des lettres et doit avoir une longueur minimale de 5 caractères et une longueur maximale de 50 caractère
            set { pseudo = value.Length >= 5 && value.Length <= 50 && new Regex("[a-z]", RegexOptions.IgnoreCase).IsMatch(value) && new Regex("[0-9]+").IsMatch(value) && !new Regex("[^a-zA-Z0-9]+").IsMatch(value) ? value : PSEUDO_DEFAULT; }
        }

        public string MotDePasse
        {
            get { return motDePasse; }
            set
            {
                //doit avoir lettres et chiffres, minimum 5 de long, au moins un maj, un char non alphanumérique
                motDePasse = value.Length >= 5 && new Regex("[0-9]+").IsMatch(value) && new Regex("[a-z]").IsMatch(value) && new Regex("[A-Z]").IsMatch(value) && new Regex("[^a-zA-Z0-9]+").IsMatch(value) ? value : PASSWORD_PAR_DEFAUT_PAS_BON;
            }
        }
        public string Nom
        {
            get { return nom; }
            set { nom = value.Length > 1 && new Regex("[a-z -]", RegexOptions.IgnoreCase).IsMatch(value) ? value : NOM_DEFAULT; }
        }

        public string Prenom
        {
            get { return prenom; }
            set { prenom = value.Length > 1 && new Regex("[a-z -]", RegexOptions.IgnoreCase).IsMatch(value) ? value : PRENOM_DEFAULT; }
        }

        public Role Role
        {
            get { return role; }
            set { role = value; }
        }


        /*
         * méthode qui retourne l'objet UserId en une chaine de charactères lisible pour l'homme
         */
        public override string ToString()
        {
            return $"Identifiant unique : {this.getId()}\nPseudonyme : {pseudo}\nMot de Passe : {motDePasse}\nNom : {nom} \nPrenom : {prenom}\nRôle : {role}";
        }


    }
}

