using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using Microsoft.Xna.Framework;

namespace MeCloidGame.Model
{
    class Level
    {
        private Tile[,] m_tiles;

        public const int WIDTH = 20;
        public const int HEIGHT = 15;

        public Tile[,] Tiles
        {
            get { return m_tiles; }
            set { m_tiles = value; }
        }

        public int Width
        {
            get { return m_tiles.GetLength(0); }
        }

        public int Height
        {
            get { return m_tiles.GetLength(1); }
        }

        public Level(string a_level)
        {
            LoadTiles(a_level);
        }

        private void LoadTiles(string a_level)
        {
            int width;
            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader(@"Content\" + Helpers.Paths.LEVELS + a_level))
            {
                string line = reader.ReadLine();
                width = line.Length;
                while (line != null)
                {
                    lines.Add(line);
                    if (line.Length != width)
                    {
                        throw new Exception(String.Format("The length of line {0} is different from all preceeding lines.", lines.Count));
                    }
                    line = reader.ReadLine();
                }
            }

            m_tiles = new Tile[width, lines.Count];

            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    char tileType = lines[y][x];
                    m_tiles[x, y] = LoadTile(tileType, x, y);
                }
            }
        }

        private Tile LoadTile(char a_tileType, int a_x, int a_y)
        {
            switch (a_tileType)
            {
                // Blank space
                case '.':
                    return new Tile(Tile.TileType.Clear);
                case '#':
                    return new Tile(Tile.TileType.Solid);
                case '%':
                    return new Tile(Tile.TileType.Destroyable);
                default:
                    throw new NotSupportedException(String.Format("Unsupported tile type character '{0}' at position {1}, {2}.", a_tileType, a_x, a_y));
            }
        }

        public Tile.TileType GetCollision(int x, int y)
        {
            if (x < 0 || x >= Width || y >= Height)
            {
                return Tile.TileType.Solid;
            }

            if (y < 0)
            {
                return Tile.TileType.Clear;
            }

            return m_tiles[x, y].Type;
        }

        public Rectangle GetBounds(int x, int y)
        {
            return new Rectangle(x * Tile.WIDTH, y * Tile.HEIGHT, Tile.WIDTH, Tile.HEIGHT);
        }

        public bool IsCollidingAt(Vector2 a_pos, Vector2 a_size)
        {
            Vector2 topLeft = new Vector2(a_pos.X - a_size.X / 2.0f, a_pos.Y - a_size.Y);
            Vector2 bottomRight = new Vector2(a_pos.X + a_size.X / 2.0f, a_pos.Y);

            for (int x = 0; x < WIDTH; ++x)
            {
                for (int y = 0; y < HEIGHT; ++y)
                {
                    if (bottomRight.X < (float)x)
                        continue;
                    if (bottomRight.Y < (float)y)
                        continue;
                    if (topLeft.X > (float)x + 1.0f)
                        continue;
                    if (topLeft.Y > (float)y + 1.0f)
                        continue;

                    if (m_tiles[x, y].Type == Tile.TileType.Solid)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
