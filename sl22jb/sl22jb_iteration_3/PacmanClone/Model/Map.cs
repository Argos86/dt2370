using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacmanClone.Model
{
    class Map
    {
        public Tile[,] m_tiles;
        public int WIDTH = 48;
        public int HEIGHT = 48;

        public Map()
        {
            m_tiles = new Tile[WIDTH, HEIGHT];
            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    m_tiles[x, y] = new Tile();
                }
            }
        }
    }
}
