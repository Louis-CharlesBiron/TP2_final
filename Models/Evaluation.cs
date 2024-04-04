using System;

namespace S_tp1 {
    public class Evaluation {//

        private Utilisateur utilisateur;
        private Media media;
        private byte cote;


        public Evaluation(Utilisateur utilisateur, Media media, byte cote) {
            this.utilisateur = utilisateur;
            this.media = media;
            this.cote = cote;
        }

        public Utilisateur getUtilisateur() { return utilisateur; }
        public Media getIdentifiantMedia() {  return media; }
        public byte getCote() {  return cote; }  

    }
}
