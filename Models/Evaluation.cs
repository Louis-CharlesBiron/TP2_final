using System;

namespace TP2_final {
    public class Evaluation {//

        private string userId;
        private string mediaId;
        private byte cote;


        public Evaluation(string userId, string mediaId, byte cote) {
            UserId = userId;
            MediaId = mediaId;
            Cote = cote;
        }

        public Evaluation(Utilisateur user, Media media, byte cote)
        {
            UserId = user.getId();
            MediaId = media.getId();
            Cote = cote;
        }

        public Evaluation() : this($"defautUser", "defautMedia", 1)
        {

        }

        public string UserId
        {
            get { return userId; }
            set {userId = value;}
        }

        public string MediaId
        {
            get { return mediaId; }
            set {mediaId = value;}
        }

        public byte Cote
        {
            get { return cote; }
            set { cote = value > 100 ? (byte)100 : value; }
        }

        public override string ToString() {
            return $"user: {UserId}, mediaId: {MediaId}, cote: {cote}";
        }


        public bool Ajouter(){
            return false;
        }

    }
}
