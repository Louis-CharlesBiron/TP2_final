using Newtonsoft.Json;
using System;

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
    }
}

