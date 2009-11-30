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
        // TODO: Decide whether or not to use uniform tiles.
        // TODO: Come up with a level creating process that better supports editing and expansion of width and height of level.
        private Tile[,] m_tiles;

        public const int WIDTH = 48;
        public const int HEIGHT = 48;

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
            if (x < 0 || x >= WIDTH || y >= HEIGHT)
            {
                return Tile.TileType.Solid;
            }

            if (y < 0)
            {
                return Tile.TileType.Solid;
            }

            return m_tiles[x, y].Type;
        }

        public bool IsCollidingAt(Vector2 a_pos)
        {
            Vector2 topLeft = new Vector2(a_pos.X - Model.Player.WIDTH / 2.0f, a_pos.Y - Model.Player.HEIGHT);
            Vector2 bottomRight = new Vector2(a_pos.X + Model.Player.WIDTH / 2.0f, a_pos.Y);

            for (int x = -1; x <= WIDTH; ++x)
            {
                for (int y = -1; y <= HEIGHT; ++y)
                {
                    if (bottomRight.X < (float)x)
                        continue;
                    if (bottomRight.Y < (float)y)
                        continue;
                    if (topLeft.X > (float)x + 1.0f)
                        continue;
                    if (topLeft.Y > (float)y + 1.0f)
                        continue;

                    if (GetCollision(x, y) == Tile.TileType.Solid)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
