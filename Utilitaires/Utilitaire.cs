using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;
using System.Web;

namespace TP2_final
{
    /*
     * Regroupe les Médias et s'occupe de la sérialisation de l'objet catalogue
     */
    public class Utilitaire
    {

        public static string getDureeFormat(int duree) 
        {
            int secs = duree % 60,
                mins = duree / 60;

            return $"{pad0(mins)}:{pad0(secs)}";
        }
        public static string pad0(int num) { return (num<10?"0":"")+num; }
        public static string pad0(string num) { return (int.Parse(num)<10?"0":"")+num; }


        public static string getDateFormat(long dateMs)
        {
            DateTime date = new DateTime(1970, 1, 1).AddMilliseconds(dateMs);
            return $"{date.Year}-{pad0(date.Month)}-{pad0(date.Day)}";
        }

        /*
         * conversion entité html
         * enlever les echappements
         * enlever les espaces blancs avant et apres
         */
        public static string Filtrage(string input)
        {
            return HttpUtility.HtmlEncode(Regex.Unescape(input.Trim()));
        }

        /// <summary>
        /// Filtre et valide le pseudo passé en paramètre
        /// </summary>
        /// <param name="pseudo"></param>
        /// <returns>le pseudo valide ou string vide ("")</returns>
        public static string ValidationPseudo(string pseudo)
        {
            pseudo = Filtrage(pseudo);
            return !(pseudo.Length >= 5 &&
               pseudo.Length <= 50 &&
               new Regex("[a-z]", RegexOptions.IgnoreCase).IsMatch(pseudo) &&
               new Regex("[0-9]+").IsMatch(pseudo) &&
               !new Regex("[^a-zA-Z0-9]+").IsMatch(pseudo)) ? "" : pseudo;
        }

        public static string ValidationPassword(string pw)
        {
            pw = Filtrage(pw);
            return !(pw.Length >= 5 && pw.Length <= 100 &&
                new Regex("[0-9]+").IsMatch(pw) &&
                new Regex("[a-z]").IsMatch(pw) &&
                new Regex("[A-Z]").IsMatch(pw) &&
                new Regex("[^a-zA-Z0-9\\&><]+").IsMatch(pw)) ? "" : pw;
        }
        public static string ValidationPrenom(string prenom)
        {
            prenom = Filtrage(prenom);
            return !(prenom.Length > 1 && prenom.Length <= 50 &&
                new Regex("[a-z -]", RegexOptions.IgnoreCase)
                .IsMatch(prenom)) ? "" : prenom;
        }
        public static string ValidationNom(string nom)
        {
            nom = Filtrage(nom);
            return !(nom.Length > 1 && nom.Length <= 50 &&
                new Regex("[a-z -]", RegexOptions.IgnoreCase)
                .IsMatch(nom)) ? "" : nom;
        }



    }
}

