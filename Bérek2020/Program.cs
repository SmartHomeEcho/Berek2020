using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bérek2020
{
    class Ceg
    {
        //Név;Neme;Részleg;Belépés;Bér
        public string Nev { get; set; }
        public string Neme { get; set; }
        public string Reszleg { get; set; }
        public int Belepes { get; set; }
        public double Ber { get; set; }

        public Ceg(string Sor)
        {
            string[] sorelemek = Sor.Split(';');
            this.Nev = sorelemek[0];
            this.Neme = sorelemek[1];
            this.Reszleg = sorelemek[2];
            this.Belepes = Convert.ToInt32(sorelemek[3]);
            this.Ber = Convert.ToDouble(sorelemek[4]);
            
        }
    }
    
    class Program
    {
        public static string reszleg_neve;
        public static List<Ceg> adatok = new List<Ceg>();
        public static void F2MO() //Beolvasás
        {
            StreamReader Olvas = new StreamReader("berek2020.txt");
            string Fejlec = Olvas.ReadLine();
            while (!Olvas.EndOfStream)
            {
                adatok.Add(new Ceg(Olvas.ReadLine()));
            }
        }
        public static void F3MO() //Dolgozók száma
        {
            Console.WriteLine($"3. feladat: Dolgozók száma:{adatok.Count} fő");
        }
        public static double atlagszamitas() //4-es feladathoz a példa kedvéért készítettem egy fügvényt
        {
            double darab = 0;
            double osszesber = 0;
            for (int i = 0; i < adatok.Count; i++)
            {
                darab = adatok.Count;
                osszesber += adatok[i].Ber;
            }
            double atlagber = Math.Round((osszesber / darab) / 1000, 1);
            return atlagber;
        }
        public static void F4MO() //Határozza meg és írja ki a képernyőre a 2020-as átlagbéreket. Az eredményt ezer forintban, egy tizedesjegyre kerekítve jelenítse meg!
        {
            Console.WriteLine($"4. feladat: A bérek átlaga: {atlagszamitas()} eFt");
        }
        public static void F5MO() //Névbekérés
        {
            Console.Write("5. Feladat: Kérem a részleg nevét: ");
            reszleg_neve = Console.ReadLine();
        }
        public static void F6MO() //Linq-val kikeressük azt az indexü elemet ahol egyezik a reszleg neve a bevittel, ezeket csökkenő sorba tesszük és az ber szerint elsőt kiíratjuk
        {
            try
            {
                int keresett = adatok.IndexOf(adatok.Where(x => x.Reszleg == reszleg_neve).OrderByDescending(x => x.Ber).First());
                Console.WriteLine("6. Feladat: A legtöbbet kereső dolgozó a megadott részlegen");
                Console.WriteLine($"\tNév: {adatok[keresett].Nev}");
                Console.WriteLine($"\tNem: {adatok[keresett].Neme}");
                Console.WriteLine($"\tBelépés: {adatok[keresett].Belepes}");
                Console.WriteLine($"\tBér: {adatok[keresett].Ber:### ###} Forint");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("6. Feladat: A megadott részleg nem létezik a cégnél!");
            }
        }
        public static void F7MO()// Linq-val talán a legegyszerűbb a statisztika, de persze lehet 3 for ciklussal is csinálni:)
        {
            Console.WriteLine($"7. Feladat: Statisztika");
            adatok.GroupBy(x => x.Reszleg).ToList().ForEach(x => Console.WriteLine($"\t{x.Key} - {x.Count()} fő"));
        }

        static void Main(string[] args)
        {
            F2MO();
            F3MO();
            F4MO();
            F5MO();
            F6MO();
            F7MO();
            
            Console.ReadKey();
        }
    }
}
