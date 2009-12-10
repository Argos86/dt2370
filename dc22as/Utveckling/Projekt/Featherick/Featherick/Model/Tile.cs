using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Featherick.Model
{
    class Tile
    {
        public enum TileType
        {
            Clear = 0,
            Blocked = 1,            
            //en plattform som man kan stå på, och hoppa igenom
            Platform = 2,
            Start = 3,
            Exit = 4,
            //en tile som man kan hänga sig fast på undersidan.
            Sticky = 5, //awesome
            Water = 6
        }

        public TileType m_tileType;

        public Tile(TileType a_tileType)
        {
            m_tileType = a_tileType;
        }
    }
}
