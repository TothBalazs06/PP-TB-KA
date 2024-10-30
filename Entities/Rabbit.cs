namespace EntitesLib
{
    public class Rabbit
    {
        private const int MaxAge = 5; // Maximum életkor
        private const int MinimumAgeToReproduce = 1; // Minimum életkor a szaporodáshoz
        private const int ReproductionEnergyThreshold = 8; // Ennyi energiára van szüksége a nyúlnak a szaporodáshoz
        private const int MinimumEnergyToSurvive = 0; // Ha a nyúlnak energiája kisebb mint ez, akkor meghal
        public int Age { get; private set; } 
        public bool IsAlive { get; private set; }
        public int Energy; // Azért publikus mert a tesztesetekhez szükséges


        public Rabbit()
        {
            Energy = 3; // Alap energia szint
            Age = 0;
            IsAlive = true; // Élőként indul
        }

        public void Move(Cell[,] grid, int x, int y)
        {
            Random rand = new Random();
            int newX = x + rand.Next(-1, 2); // -1, 0, vagy 1
            int newY = y + rand.Next(-1, 2); // -1, 0, vagy 1

            if (newX >= 0 && newX < grid.GetLength(0) && newY >= 0 && newY < grid.GetLength(1))
            {
                // Csak akkor mozog, ha üres a cella
                if (grid[newX, newY].Rabbit == null && grid[newX, newY].Fox == null)
                {
                    grid[newX, newY].Rabbit = this;
                    grid[x, y].Rabbit = null; // Cella elhagyása
                }
            }
        }


        public void Eat(GrassState grass)
        {
            if (grass == GrassState.Young)
            {
                Energy += 1; //Növeli az energiát
            }
            else if (grass == GrassState.Mature)
            {
                Energy += 2; //Növeli az energiát
            }
            else if (grass == GrassState.Old)
            {
                Energy += 3; //Növeli az energiát
            }
        }


        public bool Survive()
        {
            // Ellenőrzi hogy élhet-e a nyúl
            Age++;
            Energy--; // Csökkenti a nyúl energiaszintjét
            if (Energy < MinimumEnergyToSurvive || Age > MaxAge)
            {
                IsAlive = false; // Meghal
            }
            return IsAlive;
        }

        public Rabbit Reproduce()
        {
            if (IsAlive && Age > MinimumAgeToReproduce && Energy > ReproductionEnergyThreshold) // Szapordik a nyúl, ha megfelel a feltételnek 
            {
                Energy -= 2;
                Console.WriteLine("Egy nyúl szaporodott.");
                return new Rabbit(); // Visszaad egy új nyulat
            }
            return null; // Nincs szaporodás
        }
    }
}
