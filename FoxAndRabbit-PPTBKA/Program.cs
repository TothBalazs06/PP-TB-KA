using System;
using EntitesLib;
using FoxAndRabbit_PPTBKA;

namespace PTPB_FoxAndRabbits
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Üdvözöllek a rókák és nyulak szimulációban!");
            Console.Write("Add meg a rács szélességét: ");
            int width = int.Parse(Console.ReadLine());
            Console.Write("Add meg a rács magasságát: ");
            int height = int.Parse(Console.ReadLine());

            SimulationEngine engine = new SimulationEngine(width, height);

            Console.WriteLine("Nyomj Entert a következő körhöz!");
            //ide kerülnek majd a rókák és nyulak meghívásai
            engine.AddRabbit(1, 1);
            engine.AddRabbit(1, 1);
            engine.AddRabbit(1, 2);
            engine.AddFox(2, 2);
            engine.AddFox(2, 3);
            engine.AddFox(3, 4);
            while (true)
            {
                Console.ReadLine();
                //engine.NextTurn()
                engine.DisplayGrid();
            }
        }
    }
}