using EntitesLib;

namespace Entities
{
    public class Cell
    {
        public GrassState Grass { get; set; }
        public Rabbit Rabbit { get; set; }
        public Fox Fox { get; set; }

        public Cell()
        {
            Grass = GrassState.Young; // Alapállapota a fűnek
            Rabbit = null; // Alapállapota a nyúlnak. vagyis nincs
            Fox = null; // Alapállapota a rókának, vagyis nincs
        }
    }
}
