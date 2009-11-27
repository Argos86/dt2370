using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Level()
        {
            m_tiles = new Tile[WIDTH, HEIGHT];
            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    m_tiles[x,y] = new Tile();

                    if (x == 0 || y == 0 || x == WIDTH - 1 || y == HEIGHT - 1 ||
                        (y == WIDTH - 9 && x == 5) || (y == WIDTH - 9 && x == 6) || y == WIDTH - 7 || (x ==  6 && y == WIDTH - 8))
                    {
                        m_tiles[x, y].m_tileType = Tile.TileType.Platform;
                    }
                    
                }
            }
        }

        public bool IsCollidingAt(Vector2 a_pos, Vector2 a_size)
        {
            
            Vector2 topLeft = new Vector2(a_pos.X, a_pos.Y);
            Vector2 bottomRight = new Vector2(a_pos.X + a_size.X, a_pos.Y + a_size.Y);

            //TODO: fixa spelarens position pixelposition vs. gridposition <-- SE HIT! OBSERVERA DETTA! 
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
                    if (topLeft.Y < (float)y + 1.0f)
                        continue;

                    if (m_tiles[x, y].m_tileType == Tile.TileType.Platform)
                    {
                        return true;
                    }
                }
            }
            return false;
        }



        /*public void CreateLevel(Random a_r)
        {
            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    m_tiles[x, y] = new Tile();
                }
            }


            //create a few platforms
            int minSize = 4;
            int maxSize = 16;
            for (int i = 0; i < 10; i++)
            {
                Rectangle r = new Rectangle(a_r.Next() % (WIDTH - maxSize), a_r.Next() % (HEIGHT - maxSize), minSize + a_r.Next() % (maxSize - minSize), minSize + a_r.Next() % (maxSize - minSize));

                for (int x = r.X; x < r.X + r.Width; x++)
                {
                    for (int y = r.Y; y < r.Y + r.Height; y++)
                    {
                        m_tiles[x, y].m_tileType = Tile.TileType.Platform;
                    }
                }
                m_tiles[r.X, r.Y + r.Height / 2].m_tileType = Tile.TileType.Air;
                m_tiles[r.X + r.Width - 1, r.Y + r.Height / 2].m_tileType = Tile.TileType.Air;
                m_tiles[r.X + r.Width / 2, r.Y].m_tileType = Tile.TileType.Air;
                m_tiles[r.X + r.Width / 2, r.Y + r.Height - 1].m_tileType = Tile.TileType.Air;
                r.X += 1;
                r.Y += 1;
                r.Width -= 2;
                r.Height -= 2;
                for (int x = r.X; x < r.X + r.Width; x++)
                {
                    for (int y = r.Y; y < r.Y + r.Height; y++)
                    {
                        m_tiles[x, y].m_tileType = Tile.TileType.Air;
                    }
                }
            }
        }*/

    }
}
