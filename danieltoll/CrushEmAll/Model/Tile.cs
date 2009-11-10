using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZombieHoards.Model
{
    class Tile
    {
        public enum TileType
        {
            Clear,
            Blocked
        };

        public TileType m_tileType;
        public bool m_destroyed; //opended doors, crushed windows

        public Tile()
        {
            m_tileType = TileType.Clear;
            m_destroyed = false;
        }
    }
}
