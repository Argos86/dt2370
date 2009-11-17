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
            Enemy,
            Player,
        }

        public TileType m_tileType;

        public Tile()
        {
            m_tileType = TileType.Clear;
        }
    }
}
