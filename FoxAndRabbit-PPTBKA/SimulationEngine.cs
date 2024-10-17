using EntitesLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxAndRabbit_PPTBKA
{
    public class SimulationEngine
    {
        private readonly Cell[,] grid;
        private readonly int width;
        private readonly int height;

        public SimulationEngine(int width, int height)
        {
            this.width = width;
            this.height = height;
            grid = new Cell[width, height];

            // A grid betöltése
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    grid[i, j] = new Cell
                    {
                        Grass = GrassState.Young, // a fű alapállapota
                        Rabbit = null,
                        Fox = null
                    };
                }
            }
            grid[0, 0].Rabbit = new Rabbit(); // A (0, 0)-rá új nyúl betöltése

        }
    }
}
