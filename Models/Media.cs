using System;


namespace S_tp1
{
    public class Media
    {

        //Types de musiques
        public enum Types
        {
            RAP, POP, JAZZ, ROCK, ELECTRO, COUNTRY, RELAXATION, INSTRUMENTAL, CONCEPTUALSYNTH, PARTY, CLASSIQUE, OST
        };

        private string? identifiantMedia;
        private Types? type;
        private List<Evaluation>? evaluations;
        private long? dateRealisation;
        private int? duree;
        private string? auteur;
        private string? producteur;
        private string? extrait;
        private string? complet;
        private string? image;


        public Media()
        {

        }

        public Media(string identifiantMedia)
        {
            this.identifiantMedia = identifiantMedia;
        }

        public Media(string identifiantMedia, Types type, long dateRealisation, int duree, string auteur, string producteur, string extrait, List<Evaluation> evaluations, string complet, string image)
        {
            this.identifiantMedia = identifiantMedia;
            this.type = type;
            this.dateRealisation = dateRealisation;
            this.duree = duree;
            this.auteur = auteur;
            this.producteur = producteur;
            this.complet = complet;
            this.extrait = extrait;
            this.image = image;
            this.evaluations = evaluations;
        }

        //overrides
        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is Media))
                return false;
            else
                return this.identifiantMedia == ((Media)obj).identifiantMedia;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        // redéfinition des opérateurs
        public static bool operator ==(Media m1, Media m2) => m1.Equals(m2);

        public static bool operator !=(Media m1, Media m2) => !(m1.identifiantMedia == m2.identifiantMedia);

        //getters
        public string IdentifiantMedia {
            get { return identifiantMedia; }
            set { identifiantMedia = value; }
        }

        public string? GetIdentifiantMedia() { return this.identifiantMedia; }
        public Types? GetType() { return this.type; }
        public byte? GetCote() { return 1; }// TODO
        public List<Evaluation>? GetEvaluations() { return this.evaluations; }
        public long? GetDateRealisation() { return this.dateRealisation; }
        public int? GetDuree() { return this.duree; }
        public string? GetAuteur() { return this.auteur; }
        public string? GetProducteur() { return this.producteur; }
        public string? GetExtrait() { return this.extrait; }
        public string? GetComplet() { return this.complet; }
        public string? GetImage() { return this.image; }
        public string? GetNom() { return this.identifiantMedia?.Split("_")[0]??"'Nom non définit'"; }

        //setters
        public void SetType(Types type) { this.type = type; }
        public void SetDateRealisation(long dateRealisation) { this.dateRealisation = dateRealisation; }
        public void SetDuree(int duree) { this.duree = duree; }
        public void SetAuteur(string auteur) { this.auteur = auteur; }
        public void SetProducteur(string producteur) { this.producteur = producteur; }
        public void SetExtrait(string extrait) { this.extrait = extrait; }
        public void SetComplet(string complet) { this.complet = complet; }
        public void SetImage(string image) { this.image = image; }

        public override string ToString()
        {
            return $" Name: {this.GetNom()}, Type: {this.type}, Cote: {this.GetCote()}/100, Date de realisation`{this.dateRealisation}, Duree: {this.duree}, Auteur: {this.auteur}, Producteur: {this.producteur}, Path: {this.complet}";
        }

    }
}