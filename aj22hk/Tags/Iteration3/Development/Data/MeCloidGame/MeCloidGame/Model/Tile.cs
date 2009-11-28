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

        public const float WIDTH = 1;//4.0f/3.0f;// 64.0f / 48.0f;

        public const float HEIGHT = 1;

        public Tile(TileType a_tileType)
        {
            m_tileType = a_tileType;
        }
    }
}
