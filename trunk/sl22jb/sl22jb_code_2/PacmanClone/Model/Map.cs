using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacmanClone.Model
{
    class Map
    {
        public Tile[,] m_tiles;
        public int WIDTH = 0;
        public int HEIGHT = 0;

        public Map()
        {
            m_tiles = new Tile[WIDTH, HEIGHT];
        }
    }
}
