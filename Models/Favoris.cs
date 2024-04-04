using System;
using System.Globalization;

namespace S_tp1 {
    public class Favoris {//

        private string userId;
        private string mediaId;


        public Favoris(string idUser, string idMedia) {
            UserId = idUser;
            MediaId = idMedia;
        }

        public Favoris(Utilisateur user, Media media)
        {
            UserId = user.getId();
            MediaId = media.getId();
        }

        public Favoris() : this($"defautUser", "defautMedia")
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

        public override string ToString() {
            return $"evaluation: {UserId}, mediaId: {MediaId}";
        }

    }
}
