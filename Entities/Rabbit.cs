namespace EntitesLib
{
    public class Rabbit
    {
        public int Age { get; private set; }
        public bool IsAlive { get; private set; }
        private const int MaxAge = 5; // A nyúl maximum kora
        private int energy;

        public Rabbit()
        {
            energy = 10; // Alap energia szint
            Age = 0;
            IsAlive = true; // Él a nyúl
        }

    }
}
