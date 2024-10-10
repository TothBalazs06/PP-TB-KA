using System;

namespace EntitesLib
{
    public class Cell
    {
        public void UpdateGrass()
        {
			if (Grass == GrassState.Empty)
            {
                Grass = GrassState.Young; // young fű lesz ha empty volt
            }
            if (Grass == GrassState.Young)
            {
                Grass = GrassState.Mature; // mature fű lesz ha young volt
            }
            else if (Grass == GrassState.Mature)
            {
                Grass = GrassState.Old; // old lesz ha mature volt
            }
            else if (Grass == GrassState.Old)
            {
                Grass = GrassState.Empty; // visszaall uresre
            }
        }
    }
}
