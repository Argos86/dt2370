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
            Platform,
            FloorOfDeath,
            GiantStar
        };

        public TileType m_tileType;

        public Tile(TileType a_tileType)
        {
            m_tileType = a_tileType;
        }
    }
}
