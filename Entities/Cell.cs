using System;

namespace EntitesLib
{
    public class Cell
    {
        public GrassState Grass { get; set; }
        public Rabbit? Rabbit { get; set; }
        public Fox? Fox { get; set; }

        public Cell()
        {
            Grass = GrassState.Young; // Alapállapot
            Rabbit = null; // Alapállapot
            Fox = null; // Alapállapot
        }

        // A fű frissítése az előző állapot alapján
        public void UpdateGrass()
        {
            if (Grass == GrassState.Empty)
            {
                Grass = GrassState.Young;
            }
            if (Grass == GrassState.Young)
            {
                Grass = GrassState.Mature;
            }
            else if (Grass == GrassState.Mature)
            {
                Grass = GrassState.Old;
            }
            else if (Grass == GrassState.Old)
            {
                Grass = GrassState.Empty;
            }
        }
    }
}
