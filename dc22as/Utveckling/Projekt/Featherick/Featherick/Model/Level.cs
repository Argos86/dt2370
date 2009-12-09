using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.IO;

namespace Featherick.Model
{
    class Level
    {
        public Tile[,] m_tiles;
        public const int WIDTH = 200;
        public const int HEIGHT = 20;

        public Rectangle[,] m_tileRect = new Rectangle[WIDTH,HEIGHT];
        
        //en tile är ~0.5 m 

        public Vector2 m_startPos;

        /// Width of level measured in tiles.
        public int Width
        {
            get { return m_tiles.GetLength(0); }
        }

        /// Height of the level measured in tiles.
        public int Height
        {
            get { return m_tiles.GetLength(1); }
        }

        public Level(string a_path)
        {            
            LoadTiles(a_path);
        }

        public void ResetLevel()
        {
 
        }

        public Vector2 GetStartPos()
        {
            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    if (m_tiles[x, y].m_tileType == Tile.TileType.Start)
                    {
                        m_startPos = new Vector2(x, y);
                        return m_startPos;
                    }
                }
            }
            return new Vector2(1, 1);
        }

        public bool GetTileAt(Vector2 a_pos, Vector2 a_size)
        {
            Vector2 TopLeft = new Vector2(a_pos.X + 0.1f, a_pos.Y + 0.2f);
            Vector2 BottomRight = new Vector2(a_pos.X + a_size.X - 0.1f, a_pos.Y + a_size.Y - 0.01f);

            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    if (BottomRight.X < (float)x)
                        continue;
                    if (BottomRight.Y < (float)y)
                        continue;
                    if (TopLeft.X > (float)x + 1.0f)
                        continue;
                    if (TopLeft.Y > (float)y + 1.0f)
                        continue;

                    if (m_tiles[x, y].m_tileType == Tile.TileType.Sticky)
                    {
                        return true;
                    }
                }
            }
            return false;
            
        }

        public bool ReachedExit(Vector2 a_pos, Vector2 a_size)
        {
            Vector2 TopLeft = new Vector2(a_pos.X + 0.1f, a_pos.Y + 0.2f);
            Vector2 BottomRight = new Vector2(a_pos.X + a_size.X - 0.1f, a_pos.Y + a_size.Y - 0.01f);


            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    if (BottomRight.X < (float)x)
                        continue;
                    if (BottomRight.Y < (float)y)
                        continue;
                    if (TopLeft.X > (float)x + 1.0f)
                        continue;
                    if (TopLeft.Y > (float)y + 1.0f)
                        continue;

                    if (m_tiles[x-1, y].m_tileType == Tile.TileType.Exit)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        public bool IsCollidingAt(Vector2 a_pos, Vector2 a_size) 
        {
            Vector2 TopLeft = new Vector2(a_pos.X + 0.1f, a_pos.Y + 0.2f);          
            Vector2 BottomRight = new Vector2(a_pos.X + a_size.X - 0.1f, a_pos.Y + a_size.Y - 0.01f);


            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    if (BottomRight.X < (float)x)
                        continue;
                    if (BottomRight.Y < (float)y)
                        continue;
                    if (TopLeft.X > (float)x + 1.0f)
                        continue;
                    if (TopLeft.Y > (float)y + 1.0f)
                        continue;

                    if (m_tiles[x,y].m_tileType == Tile.TileType.Blocked)
                    {
                        return true;
                    }
                    
                    if (m_tiles[x, y].m_tileType == Tile.TileType.Sticky)
                    {
                        return true;
                    }
                }                
            }
            return false;
        }

        private void LoadTiles(string a_path)
        {
            // Load the level and ensure all of the lines are the same length.
            int width;
            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader(@"Content\levels\" + a_path))
            {
                string line = reader.ReadLine();
                width = line.Length;
                while (line != null)
                {
                    lines.Add(line);
                    if (line.Length != width)
                        throw new Exception(String.Format("The length of line {0} is different from all preceeding lines.", lines.Count));
                    line = reader.ReadLine();
                }
            }

            // Allocate the tile grid.
            m_tiles = new Tile[width, lines.Count];

            // Loop over every tile position,
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    // to load each tile.
                    char tileType = lines[y][x];
                    m_tiles[x, y] = LoadTile(tileType, x, y);
                }
            }         
        }

        private Tile LoadTile(char a_tileType, int x, int y)
        {
            switch (a_tileType)
            {
                // Blank space
                case '.':
                    return new Tile(Tile.TileType.Clear);

                case '#':
                    return new Tile(Tile.TileType.Blocked);

                case 'X':
                    return new Tile(Tile.TileType.Exit);

                case 'S':
                    return new Tile(Tile.TileType.Start);

                case '-':
                    return new Tile(Tile.TileType.Platform);

                case 'W':
                    return new Tile(Tile.TileType.Water);

                case '"':
                    return new Tile(Tile.TileType.Sticky);

                // Unknown tile type character
                default:
                    throw new NotSupportedException(String.Format("Unsupported tile type character '{0}' at position {1}, {2}.", a_tileType, x, y));
            }
        }


        /*public void Test()
        {
            Clear();
            CreateTestMap();
            IsCollidingAt(new Vector2(31, 8.1f), new Vector2(1, 1));
        }

        public void CreateTestMap()
        {
            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    m_tiles[x, y] = new Tile();
                }
                if (x > 30 && x < 70)
                {
                    // test tiles
                    m_tiles[x, 7].m_tileType = Tile.TileType.Blocked;
                }
            }

            //test tiles
            CreateWall(0);
            CreateWall(WIDTH - 1);
            CreateFloor(HEIGHT - 1);
            m_tiles[60, 7].m_tileType = Tile.TileType.Clear;
            m_tiles[59, 8].m_tileType = Tile.TileType.Blocked;
            m_tiles[59, 9].m_tileType = Tile.TileType.Blocked;
            m_tiles[59, 10].m_tileType = Tile.TileType.Blocked;
            m_tiles[61, 8].m_tileType = Tile.TileType.Blocked;
            m_tiles[61, 9].m_tileType = Tile.TileType.Blocked;
            m_tiles[61, 10].m_tileType = Tile.TileType.Blocked;
            m_tiles[55, 5].m_tileType = Tile.TileType.Blocked;
            m_tiles[55, 6].m_tileType = Tile.TileType.Blocked;
            m_tiles[52, 3].m_tileType = Tile.TileType.Blocked;
            m_tiles[48, 3].m_tileType = Tile.TileType.Blocked;
            m_tiles[43, 3].m_tileType = Tile.TileType.Blocked;
            m_tiles[35, 4].m_tileType = Tile.TileType.Blocked;
        }*/

    }
}
