namespace CA220920
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //beolvasás
            List<Kuldetes> kuldetesek = new List<Kuldetes>();

            using StreamReader sr = new StreamReader(@"..\..\..\res\kuldetesek.csv"); 
            while (!sr.EndOfStream)
                kuldetesek.Add(new Kuldetes(sr.ReadLine()));

            //f3
            Console.WriteLine($"3. feladat:\n\tÖsszesen {kuldetesek.Count} alkalommal indítottak űrhajót.");

            //f4
            int utasokSzamanakOsszege = kuldetesek.Sum(k => k.UtasokSzama);
            Console.WriteLine($"4. feladat:\n\t{utasokSzamanakOsszege} utas indult az űrbe összesen.");

            //f5
            int kevesebbMint5 = kuldetesek
                .Count(k => k.UtasokSzama < 5);
            Console.WriteLine($"5. feladat:\n\tÖsszesen {kevesebbMint5} alkalommal küldtek kevesebb, mint 5 embert az űrbe.");

            //alternatívan:
            var x = (from k in kuldetesek
                     where k.UtasokSzama < 5
                     select k.UtasokSzama).Count();

            //f6
            int cuuusz = kuldetesek
                .Where(k => k.SikloNeve == "Columbia")
                .OrderByDescending(k => k.KilovesNapja)
                .First()
                .UtasokSzama;

            Console.WriteLine($"6. feladat:\n\t{cuuusz} asztronauta volt a Columbia fedélzetén annak utolsó útján.");

            //f7
            Kuldetes litf = kuldetesek
                .OrderByDescending(k => k.KuldetesHossza)
                .First();

            Console.WriteLine(
                $"7. feladat:\n" +
                $"\tA leghosszabb ideig a {litf.SikloNeve} volt az űrben a {litf.Kod} küldetés során.\n" +
                $"\tÖsszesen {litf.KuldetesHossza} órát volt távol a Földttől.");

            //f8
            Console.Write(
                "8. feladat:\n" +
                "\tÉvszám: ");
            int evszam = int.Parse(Console.ReadLine());
            int kuldetesekSzamaAdottEvben = kuldetesek
                .Count(k => k.KilovesNapja.Year == evszam);

            if (kuldetesekSzamaAdottEvben > 0)
                Console.WriteLine($"\tEbben az évben {kuldetesekSzamaAdottEvben} küldetés volt.");
            else
                Console.WriteLine("Ebben az évben nem indult küldetés.");

            //f9
            float kuldetesekKennedyUrkozponton = kuldetesek
                .Where(k => k.ErkezesHelye == "Kennedy")
                .Count();

            Console.WriteLine(
                $"9. feladat:\n" +
                $"\tA küldetések {(kuldetesekKennedyUrkozponton / kuldetesek.Count * 100):0.00}%-a fejeződött be a Kennedy űrközpontban.");

            //f10
            var sikloPerNap = kuldetesek
                .GroupBy(k => k.SikloNeve);

            using StreamWriter sw = new(@"..\..\..\res\ursiklok.txt");
            foreach (var s in sikloPerNap)
                sw.WriteLine(s.Key + "\t" + Math.Round(s.Sum(s => s.KuldetesHossza) / 24f, 2));
        }
    }
}