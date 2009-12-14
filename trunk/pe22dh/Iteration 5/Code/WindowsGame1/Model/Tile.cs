using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombatLand.Model
{
    class Tile
    {
        public enum TileType
        {
            Empty,
            Blocked,
            Win
        };
        
        public TileType m_tileType;

        public Tile()
        {
            m_tileType = TileType.Empty;
        }
    }
}
