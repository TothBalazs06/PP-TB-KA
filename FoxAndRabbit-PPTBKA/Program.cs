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
            //nagyjábol így: engine.AddRabbit(1,1)
            while (true)
            {
                Console.ReadLine();
                //ide kerül majd még a engine.NextTurn()
                //és a engine.DisplayGrid() ha azok elkészülnek a SimEngine-ben.
            }
        }
    }
}