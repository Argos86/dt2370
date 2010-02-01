using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace JumpMania.Model
{
    class Level
    {
        public Tile[,] m_tiles;
        public const int WIDTH = 18;
        public const int HEIGHT = 90;
        
        public int m_levelID;

        public Level(string a_path, int a_levelID)
        {
            LoadTiles(a_path);
            m_levelID = a_levelID;
        }

        private void LoadTiles(string a_path)
        {
            // Load the level and ensure all of the lines are the same length.
            int width;
            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader(@"Content\Levels\" + a_path))
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
            for (int y = 0; y < HEIGHT; ++y)
            {
                for (int x = 0; x < WIDTH; ++x)
                {
                    // to load each tile.
                    char tileType = lines[y][x];
                    m_tiles[x, y] = LoadTile(tileType, x, y);
                }
            }
        }
          private Tile LoadTile(char tileType, int x, int y)
        {
            switch (tileType)
            {
                // Air
                case '.':
                    return new Tile(Tile.TileType.Air);

                // Plattform
                case 'o':
                    return new Tile(Tile.TileType.Platform);

                // Gigantisk Stjärna (eller mål)
                case 'X':
                    return new Tile(Tile.TileType.GiantStar);

                // Floor Of Death
                case 'T':
                    return new Tile(Tile.TileType.FloorOfDeath); 

                // Unknown tile type character
                default:
                    throw new NotSupportedException(String.Format("Unsupported tile type character '{0}' at position {1}, {2}.", tileType, x, y));
            }
        }

        public bool IsCollidingAt(Vector2 a_pos, Vector2 a_size)
        {          
            Vector2 topLeft = new Vector2(a_pos.X, a_pos.Y + 0.1f); // <-- 0.1f gör så att ninja kan gå emellan tiles
            Vector2 bottomRight = new Vector2((a_pos.X + a_size.X) - 0.1f, a_pos.Y + a_size.Y);

            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {

                    if (bottomRight.X < (float)x)
                        continue;
                    if (bottomRight.Y <= (float)y)
                        continue;
                    if (topLeft.X > (float)x + 1.0f)
                        continue;
                    if (topLeft.Y > (float)y + 1.0f)
                        continue;

                    if (m_tiles[x, y].m_tileType == Tile.TileType.Platform)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsTouchingFloor(Vector2 a_pos, Vector2 a_size)
        {
            if (a_pos.Y + a_size.Y > m_floorHeight)
            {
                return true;
            }
            return false;
        }

        public bool IsTouchingGStar(Vector2 a_pos, Vector2 a_size)
        {
            Vector2 topLeft = new Vector2(a_pos.X, a_pos.Y);
            Vector2 bottomRight = new Vector2(a_pos.X + a_size.X, a_pos.Y + a_size.Y);

            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {

                    if (bottomRight.X < (float)x)
                        continue;
                    if (bottomRight.Y <= (float)y)
                        continue;
                    if (topLeft.X > (float)x + 1.0f)
                        continue;
                    if (topLeft.Y > (float)y + 1.0f)
                        continue;

                    if (m_tiles[x, y].m_tileType == Tile.TileType.GiantStar)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public float m_floorHeight = HEIGHT, m_floorSpeed = 2.5f;  
        public void UpdateFloor(GameTime theGameTime) 
        {
            m_floorHeight -= (float)theGameTime.ElapsedGameTime.TotalSeconds * m_floorSpeed;
        }
    }
}
