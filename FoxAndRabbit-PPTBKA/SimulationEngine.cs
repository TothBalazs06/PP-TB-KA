﻿using EntitesLib;
using System;

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
                        Grass = GrassState.Young, // A fű alapállapota
                        Rabbit = null,
                        Fox = null
                    };
                }
            }
            grid[0, 0].Rabbit = new Rabbit(); // A (0, 0)-ra új nyúl betöltése

        }

        //Ez a metódus hozzáad egy nyulat X,Y koordinátára
        public void AddRabbit(int x, int y)
        {
            if (IsWithinBounds(x, y) && grid[x, y].Rabbit == null)
            {
                grid[x, y].Rabbit = new Rabbit();
            }
        }

        //Ez a metódus hozzáad egy rókát X,Y koordinátára
        public void AddFox(int x, int y)
        {
            if (IsWithinBounds(x, y) && grid[x, y].Fox == null)
            {
                grid[x, y].Fox = new Fox();
            }
        }

        //Új kör
        public void NextTurn()
        {
            try
            {
                //Mozgás és lépések
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        var cell = grid[i, j];

                        // Nyúl logikája
                        if (cell.Rabbit != null) // Csak akkor történjen bármi ha a cellában van egy nyúl
                        {
                            // Nyúl evési mechanikája
                            if (cell.Grass == GrassState.Empty)
                            {
                                Console.WriteLine($"Nyúl a ({i}, {j}) koordinátán enni próbál füvet, de nincs mit ennie.");
                            }
                            else if (cell.Grass == GrassState.Young)
                            {
                                Console.WriteLine($"Nyúl a ({i}, {j}) koordinátán fűkezdeményt eszik.");
                            }
                            else if (cell.Grass == GrassState.Mature)
                            {
                                Console.WriteLine($"Nyúl a ({i}, {j}) koordinátán zsenge füvet eszik.");
                            }
                            else if (cell.Grass == GrassState.Old)
                            {
                                Console.WriteLine($"Nyúl a ({i}, {j}) koordinátán kifejlett füvet eszik.");
                            }

                            if (cell.Grass == GrassState.Young || cell.Grass == GrassState.Mature || cell.Grass == GrassState.Old) // Csak akkor eszik a nyúl, ha a fű megfelel a feltételnek
                            {
                                cell.Rabbit.Eat(cell.Grass); // Evés
                                cell.Grass = GrassState.Empty; // Az evés után megváltoztatjuk a füvet
                                Console.WriteLine($"Nyúl a ({i}, {j}) koordinátán füvet evett.");
                            }
                            else
                            {
                                Console.WriteLine($"Nyúl a ({i}, {j}) koordinátán nem tudott enni füvet, mivel nincs mit ennie.");
                            }

                            // A nyúl mozgatása
                            cell.Rabbit.Move(grid, i, j); 

                            // Meghívjuk a Survive() metódust mely megnézi hogy él-e a nyúl
                            if (cell.Rabbit != null && !cell.Rabbit.Survive())
                            {
                                Console.WriteLine($"Nyúl a ({i}, {j}) koordinátán elpusztult.");
                                cell.Rabbit = null; // A nyulat töröljük
                            }
                            else if (cell.Rabbit != null) // Ha viszont még él akkor megpróbál reprodukálódni 
                            {
                                Rabbit newRabbit = cell.Rabbit.Reproduce(); // Ez amit visszaad a Reproduce() metódus
                                if (newRabbit != null)
                                {
                                    // Megpróbáljuk lehelyezni egy közeli cellába
                                    AddRabbitToAdjacentCell(i, j, newRabbit);
                                }
                            }

                            cell.UpdateGrass(); // Frissítjük a füvet a celllán
                        }

                        if (cell.Fox != null) // Csak akkor történik bármi ha van róka a cellában
                        {
                            // Róka mozgatása
                            cell.Fox.Move(grid, i, j);

                            // Az evéshez ellenőrizzük, hogy van-e nyúl a közelben
                            if (TryFindRabbitInAdjacentCells(i, j, out int rabbitX, out int rabbitY) && cell.Fox != null)
                            {
                                cell.Fox.EatRabbit(); // A róka megeszi a nyulat
                                Console.WriteLine($"Róka a ({i}, {j}) koordinátán megevett egy nyulat ({rabbitX}, {rabbitY}).");
                                grid[rabbitX, rabbitY].Rabbit = null; //Nyulat töröljük, ha megették
                            }
                            else
                            {
                                Console.WriteLine($"Róka a ({i}, {j}) koordinátán nem talált nyulat az evéshez.");
                            }

                            // Meghívjuk a Survive() metódust mely megnézi, hogy él-e a róka
                            if (cell.Fox != null && !cell.Fox.Survive())
                            {
                                Console.WriteLine($"Róka a ({i}, {j}) koordinátán elpusztult");
                                cell.Fox = null; // A rókát töröljük, ha már nem él
                            }
                            else if (cell.Fox != null) // Ha viszont még él akkor megpróbál reprodukálódni 
                            {
                                Fox newFox = cell.Fox.Reproduce(); // Ez amit visszaad a Reproduce() metódus
                                if (newFox != null)
                                {
                                    // Megpróbáljuk lehelyezni egy közeli cellába
                                    AddFoxToAdjacentCell(i, j, newFox);
                                }
                            }
                        }

                        // Frissítsjük a füvet az adott cellán
                        cell.UpdateGrass();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
            }
        }

        // Ellenőrizzük hogy van-e nyúl a közelben
        private bool TryFindRabbitInAdjacentCells(int x, int y, out int rabbitX, out int rabbitY)
        {
            // Megnézzük, hogy van-e a közelben nyúl (fel, le, balra, jobbra)
            int[,] directions = new int[,] { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } };
            for (int d = 0; d < directions.GetLength(0); d++)
            {
                int newX = x + directions[d, 0];
                int newY = y + directions[d, 1];

                if (IsWithinBounds(newX, newY) && grid[newX, newY].Rabbit != null)
                {
                    rabbitX = newX;
                    rabbitY = newY;
                    return true; // Találtunk nyulat
                }
            }

            rabbitX = -1;
            rabbitY = -1;
            return false; // Nem találtunk
        }


        private void AddFoxToAdjacentCell(int x, int y, Fox newFox)
        {
            // Megnézzük, hogy le tudunk-e rakni rókát (fel, le, balra, jobbra)
            int[,] directions = new int[,] { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } };
            for (int d = 0; d < directions.GetLength(0); d++)
            {
                int newX = x + directions[d, 0];
                int newY = y + directions[d, 1];

                if (IsWithinBounds(newX, newY) && grid[newX, newY].Fox == null)
                {
                    grid[newX, newY].Fox = newFox; // A Reproduce() metódus által visszaadott rókát tesszük le
                    Console.WriteLine($"Új rójka a ({newX}, {newY}) koordinátán");
                    return;
                }
            }
        }
        private void AddRabbitToAdjacentCell(int x, int y, Rabbit newRabbit)
        {
            // Ugyanaz mint a rókánál
            int[,] directions = new int[,] { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } };
            for (int d = 0; d < directions.GetLength(0); d++)
            {
                int newX = x + directions[d, 0];
                int newY = y + directions[d, 1];

                if (IsWithinBounds(newX, newY) && grid[newX, newY].Rabbit == null)
                {
                    grid[newX, newY].Rabbit = newRabbit; // A Reproduce() metódus által visszaadott nyulat tesszük le
                    Console.WriteLine($"Új nyúl született a ({newX}, {newY}) koordinátán.");
                    return;
                }
            }
        }
        // A nyúl és a rókák hozzáadásához szükséges, ugyanis, ha a felh. megadott számok kisebbek mint az alap akkor hibas lenne a kod
        public bool IsWithinBounds(int x, int y)
        {
            return x >= 0 && x < width && y >= 0 && y < height;
        }

        // Teszteléshez használt metódus, visszadja az adott cellát.
        public Cell GetCell(int x, int y)
        {
            if (IsWithinBounds(x, y))
            {
                return grid[x, y];
            }
            throw new ArgumentOutOfRangeException($"A koordináták ({x}, {y}) nincsenek határon belül.");
        }


        // A mátrix megjelenítése, a nyulak, rókák és fű megjelenítése
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
                    else if (cell.Grass == GrassState.Empty)
                    {
                        Console.Write(". ");
                    }
                    else if (cell.Grass == GrassState.Young)
                    {
                        Console.Write(": ");
                    }
                    else if (cell.Grass == GrassState.Mature)
                    {
                        Console.Write("; ");
                    }
                    else if (cell.Grass == GrassState.Old)
                    {
                        Console.Write("* ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
