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
            Clear,
            Blocked,
            
            //en plattform som man kan stå på och hoppa igenom
            Platform
        }

        public TileType m_tileType;

        public Tile()
        {
            m_tileType = TileType.Clear;
        }
    }
}
