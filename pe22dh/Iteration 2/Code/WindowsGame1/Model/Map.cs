using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CombatLand.Model
{
    class Map
    {
        public Tile[,] m_tileMap;
        public int m_width = 20;
        public int m_height = 15;

        public Map()
        {
            m_tileMap = new Tile[m_height, m_width];
            m_tileMap = LoadTileMap();
        }

        private Tile[,] LoadTileMap()
        {
            return null;
        }
    }
}
