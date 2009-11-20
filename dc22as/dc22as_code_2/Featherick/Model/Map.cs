using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Featherick.Model
{
    class Map
    {
        public Tile[,] m_tiles;
        public const int WIDTH = 100;
        public const int HEIGHT = 10; 
        //en tile är ~0.5 m 

        public Map()
        {
            m_tiles = new Tile[WIDTH, HEIGHT];
            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    m_tiles[x, y] = new Tile();
                }

                if (x > 30 && x < 70)
                {
                    m_tiles[x, 5].m_tileType = Tile.TileType.Blocked;
                }
            }

            m_tiles[50, 4].m_tileType = Tile.TileType.Blocked;

        }

        //TODO: fixa
        public bool Collide(Vector2 a_pos) 
        {
            return false;
        }

        public bool IsClear(Vector2 a_pos)
        {
            a_pos = MathHelper.Clamp(new Vector2(0, 0), new Vector2(WIDTH - 1, HEIGHT - 1), a_pos);

            if (a_pos.X < 0 || a_pos.Y < 0 || a_pos.X > WIDTH - 1 || a_pos.Y > HEIGHT - 1)
            {
                return false;
            }

            if (m_tiles[(int)a_pos.X, (int)a_pos.Y].m_tileType == Tile.TileType.Clear)
            {
                return true;
            }
            return false;
        }



    }
}
