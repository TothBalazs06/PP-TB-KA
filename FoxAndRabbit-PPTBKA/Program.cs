using System;
using EntitesLib;

namespace FoxAndRabbit_PPTBKA
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Üdvözöllek a rókák és nyulak szimulációban!");
            Console.WriteLine("Készítette: Pesek Patrik, Kovács Ádám és Tóth Balázs\n");
            Console.WriteLine("Információk:");
            Console.WriteLine("\t- A rókák jelölése 'F'" +
                "\n\t- A nyulak jelölése - 'R'" +
                "\n\t- A fű jelölése:" +
                "\n\t\t- Nincs - '.'" +
                "\n\t\t- Fűkezdemény - ':'" +
                "\n\t\t- Zsenge fű - ';'" +
                "\n\t\t- Kifejlett fű - '*'" +
                "\n\t- A következő körhöz nyomj Enter-t" +
                "\n\t- Kilépéshez nyomj CTRL-C-t\n");
            int width;
            do
            {
                Console.Write("Add meg a rács szélességét (5-80): ");
                if (!int.TryParse(Console.ReadLine(), out width) || width < 5 || width > 80)
                {
                    Console.WriteLine("Hiba: A szélességnek 5 és 80 között kell lennie, és számnak kell lennie.");
                }
            } while (width < 5 || width > 80);

            int height;
            do
            {
                Console.Write("Add meg a rács magasságát (5-80): ");
                if (!int.TryParse(Console.ReadLine(), out height) || height < 5 || height > 80)
                {
                    Console.WriteLine("Hiba: A magasságnak 5 és 80 között kell lennie, és számnak kell lennie.");
                }
            } while (height < 5 || height > 80);

            Console.WriteLine($"A megadott rács szélessége: {width}");
            Console.WriteLine($"A megadott rács magassága: {height}");

            SimulationEngine engine = new SimulationEngine(width, height);
            engine.AddRabbit(1, 1);
            engine.AddRabbit(1, 2);
            engine.AddFox(2, 2);
            engine.AddFox(2, 3);
            engine.AddFox(3, 4);
            Console.WriteLine("Kezdődik a szimuláció! Nyomj Entert a következő körhöz.");
            while (true)
            {
                Console.ReadLine();
                engine.NextTurn();
                engine.DisplayGrid();
            }
        }
    }
}
