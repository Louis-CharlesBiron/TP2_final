using Newtonsoft.Json;

namespace S_tp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("TP1 d'application web\n");
            /*

            Catalogue catalogue = new Catalogue();

            Media test = new Media("testLOL", Media.Types.POP, 1001032, 10, "ads", "asddsa", "/s/s/s//s", new List<Evaluation>(), "a/f/f/f/f", "1132sad");
            Media test2 = new Media("testLOL2");
            Media test3 = new Media("testLOL3");

            catalogue.Ajouter(test);
            catalogue.Ajouter(test2);
            catalogue.Ajouter(test3);
            Console.WriteLine(test.ToString());

            string json = JsonConvert.SerializeObject(test2, Formatting.Indented);
            Console.WriteLine(json);

            //Console.WriteLine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName);
            Console.WriteLine(catalogue.getCatalogue().Count);

            catalogue.Sauvegarder();
            */

            Utilisateur johnyTest = new Utilisateur("JohnyX", "abc123", "Test", "Johny");
            Utilisateur bob = new Utilisateur("bob", "bob", "bob", "bob");
            Utilisateur singe = new Utilisateur("singe", "singe", "singe", "singe");
            Utilisateur tonTourFelix = new Utilisateur("tonTourFelix", "tonTourFelix", "tonTourFelix", "tonTourFelix");
            Utilisateur hugo = new Utilisateur("hugo", "hugo", "hugo", "hugo");
            Utilisateur hugo2 = new Utilisateur("hugo", "hugo", "hugo", "hugo");


            
            Console.WriteLine(hugo + "\n\n");
            Console.WriteLine(hugo2 + "\n\n");


        }
    }
}