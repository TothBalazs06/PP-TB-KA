namespace EntitesLib
{
    public class Rabbit
    {
        public int Age { get; private set; }
        public bool IsAlive { get; private set; }
        private const int MaxAge = 5; // max eletkor
        private int energy;

        public Rabbit()
        {
            energy = 10; // alap energia szint
            Age = 0;
            IsAlive = true; // élőként indul
        }

        public void Move(Cell[,] grid, int x, int y)
        {
            Random rand = new Random();
            int newX = x + rand.Next(-1, 2); // -1, 0, vagy 1
            int newY = y + rand.Next(-1, 2); // -1, 0, vagy 1

            if (newX >= 0 && newX < grid.GetLength(0) && newY >= 0 && newY < grid.GetLength(1))
            {
                // Move only if the cell is empty
                if (grid[newX, newY].Rabbit == null && grid[newX, newY].Fox == null)
                {
                    grid[newX, newY].Rabbit = this;
                    grid[x, y].Rabbit = null; // cella elhagyása
                }
            }
        }


        public void Eat(GrassState grass)
        {
            if (grass == GrassState.Young)
            {
                Console.WriteLine("Egy nyúl evett.");
                grass = GrassState.Empty;
                energy += 5; //növeli az energiát
            }
            else
            {
                Console.WriteLine("Egy nyúl nem tudott enni.");
            }
        }


        public bool Survive()
        {
            // ellenorzi hogy elhet-e a nyul
            Age++;
            energy--; // csokkenti a nyul energia szintjet
            if (energy < 0 || Age > MaxAge)
            {
                IsAlive = false; // meghal
            }
            return IsAlive;
        }

        public Rabbit Reproduce()
        {
            if (IsAlive && Age > 1 && energy > 5) // szapordik a nyúl ha megfelel
            {
                energy -= 2;
                Console.WriteLine("Egy nyúl szaporodott.");
                return new Rabbit(); // visszad egy új nyulat
            }
            return null; // nincs szaporodás
        }
    }
}
