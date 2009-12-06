using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CombatLand.Model
{
    class Map
    {
        public Tile[,] m_tiles;
        public const int WIDTH = 50;
        public const int HEIGHT = 15;

        public Map()
        {
            m_tiles = new Tile[WIDTH, HEIGHT];
            Clear();
        }

        public bool IsCollidingAt(Vector2 a_pos, Vector2 a_size)
        {

            Vector2 topLeft = new Vector2(a_pos.X, a_pos.Y);
            Vector2 bottomRight = new Vector2(a_pos.X + a_size.X, a_pos.Y + a_size.Y);


            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {

                    if (bottomRight.X < (float)x)
                        continue;
                    if (bottomRight.Y < (float)y)
                        continue;
                    if (topLeft.X > (float)x + 1.0f)
                        continue;
                    if (topLeft.Y > (float)y + 1.0f)
                        continue;

                    if (m_tiles[x, y].m_tileType == Tile.TileType.Blocked)
                    {
                        return true;
                    }


                }
            }


            return false;
        }



        public void Clear()
        {
            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    m_tiles[x, y] = new Tile();
                }
            }
        }
        public void BuildMap1()
        {
            //Build floor
            for (int x = 0; x < WIDTH; x++)
            {
                m_tiles[x, HEIGHT - 1].m_tileType = Tile.TileType.Blocked;
            }
            //Build roof
            for (int x = 0; x < WIDTH; x++)
            {
                m_tiles[x, 0].m_tileType = Tile.TileType.Blocked;
            }
            //Build walls
            for (int y = 0; y < HEIGHT; y++)
            {
                m_tiles[0, y].m_tileType = Tile.TileType.Blocked;
            }
            for (int y = 0; y < HEIGHT; y++)
            {
                m_tiles[WIDTH - 1, y].m_tileType = Tile.TileType.Blocked;
            }

            for (int x = 5; x < 7; x++)
            {
                m_tiles[x, HEIGHT - 5].m_tileType = Tile.TileType.Blocked;
            }
            for (int x = 8; x < 10; x++)
            {
                m_tiles[x, HEIGHT - 7].m_tileType = Tile.TileType.Blocked;
            }
        }
    }
}
