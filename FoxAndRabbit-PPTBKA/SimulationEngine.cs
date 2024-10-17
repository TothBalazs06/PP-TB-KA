﻿using EntitesLib;
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
		
		//ez a metódus hozzáad egy nyulat X,Y koordinátára
        public void AddRabbit(int x, int y)
        {
            if (IsWithinBounds(x, y) && grid[x, y].Rabbit == null)
            {
                grid[x, y].Rabbit = new Rabbit();
            }
        }

        //ez a metódus hozzáad egy rókát X,Y koordinátára
        public void AddFox(int x, int y)
        {
            if (IsWithinBounds(x, y) && grid[x, y].Fox == null)
            {
                grid[x, y].Fox = new Fox();
            }
        }

        // a nyúl és a rókák hozzáadásához szükséges, ugyanis ha a felh. megadott szamok kisebbek mint az alap akkor hibas lenne a kod
        private bool IsWithinBounds(int x, int y)
        {
            return x >= 0 && x < width && y >= 0 && y < height;
        }

        // a grid megjelenítése
        public void DisplayGrid()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var cell = grid[i, j];
                    if (cell.Rabbit != null)
                    {
                        Console.Write("R ");
                    }
                    else if (cell.Fox != null)
                    {
                        Console.Write("F ");
                    }
                    else
                    {
                        Console.Write(". ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
