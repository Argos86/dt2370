using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JumpMania.Model
{
    class Tile
    {
        public enum TileType
        {
            Air,
            Platform
        };

        public TileType m_tileType;

        public Tile()
        {
            m_tileType = TileType.Air;
        }
    }
}
