using System;
using System.Globalization;
using System.Text.Json.Serialization;

namespace TP2_final.Models {
    public class Favoris {//

        [JsonIgnore]
        private Utilisateur user;
        [JsonIgnore]
        private Media media;

        private string userName;
        private string mediaName;

        public Favoris(Utilisateur user2, Media media2)
        {
            UserName = (user=user2).Nom;
            MediaName = (media=media2).Nom;
        }

        public Favoris() : this(new Utilisateur(), new Media())
        {

        }

        public string UserName
        {
            get { return userName; }
            set {userName = value;}
        }

        public string MediaName
        {
            get { return mediaName; }
            set {mediaName = value;}
        }

        public override string ToString() {
            return $"evaluation: {UserName}, mediaId: {MediaName}";
        }

    }
}
