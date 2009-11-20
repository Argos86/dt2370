using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tower_Defense.Model
{
    class Map
    {
        public enum Direction
        {
            North,
            East,
            South,
            West
        };

        public Tile[,] m_tiles;
        public const int WIDTH = 120;
        public const int HEIGHT = 80;

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

        public void PlacedTower(Vector2 a_pos)
        {
            m_tiles[(int)a_pos.X, (int)a_pos.Y].m_tileType = Tile.TileType.Blocked;
        }

        public bool CanPlaceTower(Vector2 a_pos)
        {
            if (a_pos.X >= WIDTH || a_pos.X < 0)
            {
                return false;
            }
            if (a_pos.Y >= HEIGHT || a_pos.Y < 0)
            {
                return false;
            }

            if (m_tiles[(int)a_pos.X, (int)a_pos.Y].m_tileType == Tile.TileType.Clear)
            {
                return true;
            }
            return false;
        }

        public List<Vector2> GetFreeMapPos(Direction a_dir)
        {
            List<Vector2> free = new List<Vector2>();

            if (a_dir == Direction.North || a_dir == Direction.South)
            {
                int y = 0;
                if (a_dir == Direction.South)
                {
                    y = HEIGHT - 1;
                }

                for (int x = 0; x < WIDTH; x++)
                {
                    if (m_tiles[x, y].m_tileType == Tile.TileType.Clear)
                    {
                        free.Add(new Vector2(x, y));
                    }
                }


            }
            else
            {
                int x = 0;
                if (a_dir == Direction.East)
                {
                    x = WIDTH - 1;
                }
                for (int y = 0; y < HEIGHT; y++)
                {
                    if (m_tiles[x, y].m_tileType == Tile.TileType.Clear)
                    {
                        free.Add(new Vector2(x, y));
                    }
                }

            }

            return free;
        }
    }
}
