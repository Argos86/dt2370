using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace TileDemo.Model
{
    

    class Level
    {

        public struct Tile
        {
            public bool m_isSet;
        }

        public static int WIDTH = 10;
        public static int HEIGHT = 10;
        public Tile[,] m_grid = new Tile[WIDTH, HEIGHT];

        public Tile GetTile(int a_x, int a_y)
        {
            a_x = (int)MathHelper.Clamp(a_x, 0.0f, WIDTH-1);
            a_y = (int)MathHelper.Clamp(a_y, 0.0f, HEIGHT-1);

            return m_grid[a_x, a_y];
        }
        

        public Level()
        {

            Random r = new Random();
            for (int x = 0; x < WIDTH; x++) 
            {
                for (int y = 0; y < HEIGHT; y++) 
                {
                    m_grid[x, y].m_isSet =  r.Next() % 4 == 0;
                }

            }

            m_grid[4, 4].m_isSet = true;
        }
    }
}
