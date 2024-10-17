using System;

namespace EntitesLib
{
    public class Fox
    {

        private int energy;
        public int Age { get; private set; }
        public bool IsAlive { get; private set; }
        private const int MaxAge = 6; // maximum életkor
        private const int ReproductionEnergyThreshold = 15; // ennyi energiára van szüksége a rókának a reprodukáláshoz
        private const int MinimumEnergyToSurvive = 0; // ha a róka energiája kisebb mint ez akkor meghal

        public Fox()
        {
            Age = 0;
            IsAlive = true; // élőként indul 
            energy = 10; // alap energia
        }

    }
}
