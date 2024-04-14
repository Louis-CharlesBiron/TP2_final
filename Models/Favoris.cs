using System;
using System.Globalization;
using System.Text.Json.Serialization;

namespace TP2_final.Models {
    public class Favoris {//

        private string userName;
        private string mediaName;

        public Favoris(string username, string mediaName)
        {
            UserName = username;
            MediaName = mediaName;
        }

        public Favoris() : this("", "")
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
