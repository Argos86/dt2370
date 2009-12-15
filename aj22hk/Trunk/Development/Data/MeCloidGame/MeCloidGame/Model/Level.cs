using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using Microsoft.Xna.Framework;

namespace MeCloidGame.Model
{
    public struct Portal
    {
        public Point ToLvl;
        public Point ToCoord;
    }

    class Level
    {
        // TODO: Create a system for portals.
        private Tile[,] m_tiles;
        public Dictionary<Point, Portal> m_portals;

        public const int WIDTH = 48;
        public const int HEIGHT = 48;

        public Tile[,] Tiles
        {
            get { return m_tiles; }
            set { m_tiles = value; }
        }

        public Level(string a_level)
        {
            m_tiles = new Tile[WIDTH, HEIGHT];
            m_portals = new Dictionary<Point, Portal>();

            LoadTiles(a_level);
        }

        private void LoadTiles(string a_level)
        {
            if (File.Exists(@"Content\" + Helpers.Paths.LEVELS + a_level))
            {
                LoadLvlFromFile(a_level);
            }
            else
            {
                CreateNewLvl(a_level);
            }
        }

        private void LoadLvlFromFile(string a_level)
        {
            List<string> lines = new List<string>();

            using (StreamReader reader = new StreamReader(@"Content\" + Helpers.Paths.LEVELS + a_level))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    lines.Add(line);
                    line = reader.ReadLine();
                }
            }

            List<string> portals = new List<string>();
            using (StreamReader reader = new StreamReader(@"Content\" + Helpers.Paths.LEVELS + "0_0.ptl"))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    portals.Add(line);
                    line = reader.ReadLine();
                }
            }

            for (int y = 0; y < HEIGHT; ++y)
            {
                for (int x = 0; x < WIDTH; ++x)
                {
                    char tileType = lines[y][x];
                    if (tileType == '@')
                    {
                        LoadPortal(new Point(x, y), portals);
                    }
                    m_tiles[x, y] = LoadTile(tileType, x, y);
                }
            }
        }

        private void LoadPortal(Point a_point, List<string> a_portals)
        {
            foreach (string s in a_portals)
            {
                string[] elements = s.Split('|');
                Point pt = GetPoint(elements[0]);
                if (a_point == pt)
                {
                    Portal ptl = new Portal();
                    ptl.ToLvl = GetPoint(elements[1]);
                    ptl.ToCoord = GetPoint(elements[2]);
                    m_portals.Add(pt, ptl);
                    continue;
                }
            }
        }

        private Point GetPoint(string a_str)
        {
            string[] coords = a_str.Split(',');
            int x, y;
            int.TryParse(coords[0], out x);
            int.TryParse(coords[1], out y);

            return new Point(x, y);
        }

        private void CreateNewLvl(string a_level)
        {
            for (int y = 0; y < HEIGHT; ++y)
            {
                for (int x = 0; x < WIDTH; ++x)
                {
                    m_tiles[x, y] = LoadTile('.', x, y);
                }
            }

            SaveLevel(a_level);
            using (StreamWriter writer = new StreamWriter(@"Content\" + Helpers.Paths.LEVELS + "0_0.ptl"));
        }

        public void SaveLevel(string a_level)
        {
            List<string> lines = new List<string>();

            using (StreamWriter writer = new StreamWriter(@"Content\" + Helpers.Paths.LEVELS + a_level))
            {
                for (int y = 0; y < HEIGHT; ++y)
                {
                    string line = string.Empty;
                    for (int x = 0; x < WIDTH; ++x)
                    {
                        switch (m_tiles[x, y].Type)
                        {
                            case Tile.TileType.Clear:
                                line += ".";
                                break;
                            case Tile.TileType.Solid:
                                line += "#";
                                break;
                            case Tile.TileType.Destroyable:
                                line += "%";
                                break;
                            case Tile.TileType.Portal:
                                line += "@";
                                break;
                            case Tile.TileType.PortalFill:
                                line += "$";
                                break;
                        }
                    }
                    lines.Add(line);
                }

                foreach (string l in lines)
                {
                    writer.WriteLine(l);
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
                case '@':
                    return new Tile(Tile.TileType.Portal);
                case '$':
                    return new Tile(Tile.TileType.PortalFill);
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

        public bool IsCollidingAt(Vector2 a_pos, Tile.TileType a_type)
        {
            Vector2 topLeft = new Vector2(a_pos.X - Model.Player.WIDTH / 2.0f, a_pos.Y - Model.Player.HEIGHT);
            Vector2 bottomRight = new Vector2(a_pos.X + Model.Player.WIDTH / 2.0f, a_pos.Y);

            for (int x = -1; x <= WIDTH; ++x)
            {
                for (int y = -1; y <= HEIGHT; ++y)
                {
                    if (bottomRight.X < (float)x)
                        continue;
                    if (bottomRight.Y <= (float)y)
                        continue;
                    if (topLeft.X > (float)x + 1.0f)
                        continue;
                    if (topLeft.Y > (float)y + 1.0f)
                        continue;

                    if (GetCollision(x, y) == a_type)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
