using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace MeCloidGame.Model
{
    class Tile
    {
        public enum TileType
        {
            Clear = 0,
            Solid = 1,
            Destroyable = 2
        }

        private TileType m_tileType;
        public TileType Type
        {
            get { return m_tileType; }
            set { m_tileType = value; }
        }

        public Tile(TileType a_tileType)
        {
            m_tileType = a_tileType;
        }
    }
}
