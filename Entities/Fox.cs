using System;

namespace EntitesLib
{
    public class Fox
    {

        public int Energy;
        public int Age { get; private set; }
        public bool IsAlive { get; private set; }
        private const int MaxAge = 6; // maximum életkor
        private const int ReproductionEnergyThreshold = 15; // ennyi energiára van szüksége a rókának a reprodukáláshoz
        private const int MinimumEnergyToSurvive = 0; // ha a róka energiája kisebb mint ez akkor meghal

        public Fox()
        {
            Age = 0;
            IsAlive = true; // élőként indul 
            Energy = 10; // alap energia
        }

        // Mozgás
        public void Move(Cell[,] grid, int x, int y)
        {
            Random rand = new Random();
            int newX = x + rand.Next(-1, 2);
            int newY = y + rand.Next(-1, 2);

            if (newX >= 0 && newX < grid.GetLength(0) && newY >= 0 && newY < grid.GetLength(1))
            {
                // csak akkor mozog ha a cella üres
                if (grid[newX, newY].Fox == null && grid[newX, newY].Rabbit == null)
                {
                    grid[newX, newY].Fox = this;
                    grid[x, y].Fox = null; // cella elhagyása
                }
            }
        }

        public void EatRabbit()
        {
            Energy += 10; // növeli az energiát
        }

        public bool Survive()
        {
            // ellenőrzi a róka élhet-e
            Age++;
            Energy--; // csökketi az energiaszintet

            if (Energy < MinimumEnergyToSurvive || Age > MaxAge)
            {
                IsAlive = false; // meghal a róka
            }
            return IsAlive;
        }

        public Fox Reproduce()
        {
            // akkor szaporodhat ha megfelel a feltételnek
            if (IsAlive && Age > 2 && Energy > ReproductionEnergyThreshold)
            {
                Energy -= 5; // energia csökkenése a szaporodás miatt
                return new Fox(); // visszad egy új rókát
            }
            return null; // nincs szaporodás
        }
    }
}
