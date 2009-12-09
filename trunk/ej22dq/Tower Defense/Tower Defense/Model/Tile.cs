using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tower_Defense.Model
{
    class Tile
    {
        public enum TileType
        {
            Clear,
            Blocked,
            Towerground
        };

        public TileType m_tileType;
        public bool m_destroyed; //opended doors, crushed windows

        public Tile(int a_isBlocked)
        {
            if (a_isBlocked == 1)
            {
                m_tileType = TileType.Blocked;
            }
            else
            {
                m_tileType = TileType.Clear;
            }

            m_destroyed = false;
        }
    }
}
