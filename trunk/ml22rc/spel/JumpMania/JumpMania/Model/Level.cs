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
        public const int WIDTH = 60;
        public const int HEIGHT = 35; 

        public Level()
        {
            m_tiles = new Tile[WIDTH, HEIGHT];
            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    m_tiles[x,y] = new Tile();
                }
            }
        }

        public void CreateLevel(Random a_r)
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
        }

    }
}
