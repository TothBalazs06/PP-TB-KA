using System;

namespace EntitesLib
{
    public class Fox
    {
        private const int MaxAge = 8; // Maximum életkor
        private const int MinimumAgeToReproduce = 2; // Minimum életkor a szaporodáshoz
        private const int ReproductionEnergyThreshold = 12; // Ennyi energiára van szüksége a rókának a szaporodáshoz
        private const int MinimumEnergyToSurvive = 0; // Ha a nyúlnak energiája kisebb mint ez, akkor meghal

        public int Energy; // Azért publikus mert a tesztesetekhez szükséges
        public int Age { get; private set; }
        public bool IsAlive { get; private set; }

        public Fox()
        {
            Age = 0;
            IsAlive = true; // Élőként indul 
            Energy = 10; // Alap energia
        }

        // Mozgás
        public void Move(Cell[,] grid, int x, int y)
        {
            Random rand = new Random();
            int newX = x + rand.Next(-1, 2);
            int newY = y + rand.Next(-1, 2);

            if (newX >= 0 && newX < grid.GetLength(0) && newY >= 0 && newY < grid.GetLength(1))
            {
                // Csak akkor mozog ha a cella üres
                if (grid[newX, newY].Fox == null && grid[newX, newY].Rabbit == null)
                {
                    grid[newX, newY].Fox = this;
                    grid[x, y].Fox = null; // Cella elhagyása
                }
            }
        }

        public void EatRabbit()
        {
            Energy += 4; // Növeli az energiát
        }

        public bool Survive()
        {
            // Ellenőrzi a róka élhet-e
            Age++;
            Energy--; // Csökketi az energiaszintet

            if (Energy < MinimumEnergyToSurvive || Age > MaxAge)
            {
                IsAlive = false; // meghal a róka
            }
            return IsAlive;
        }

        public Fox Reproduce()
        {
            // Akkor szaporodhat, ha megfelel a feltételnek
            if (IsAlive && Age > MinimumAgeToReproduce && Energy > ReproductionEnergyThreshold)
            {
                Energy -= 5; // Energia csökkenése a szaporodás miatt
                return new Fox(); // Visszad egy új rókát
            }
            return null; // Nincs szaporodás
        }
    }
}
