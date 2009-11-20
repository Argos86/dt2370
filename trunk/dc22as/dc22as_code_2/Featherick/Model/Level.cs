using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Featherick.Model
{
    class Level
    {
        public Tile[,] m_tiles;
        public const int WIDTH = 100;
        public const int HEIGHT = 10;

        //TODO: ska jag använda width som maxstorlek på arrayen?
        public Rectangle[,] m_tileRect = new Rectangle[WIDTH,HEIGHT];
        
        //en tile är ~0.5 m 


        public Level()
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
                    // test tiles
                    m_tiles[x, 8].m_tileType = Tile.TileType.Blocked;
                }
            }

            // en test tile
            m_tiles[55, 7].m_tileType = Tile.TileType.Blocked;

        }


        public bool Collide(Vector2 a_pos, Player a_player) 
        {            
            //returnera olika för vertikal och horisontell kollision   
            a_player.m_playerRect = new Rectangle((int)a_pos.X, (int)a_pos.Y, a_player.m_frameSize.X, a_player.m_frameSize.Y);
            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {                    
                    if (m_tiles[x, y].m_tileType == Tile.TileType.Blocked)
                    {
                        //vad ska jag skicka in för variabel till m_tileRect?
                        m_tileRect[x,y] = new Rectangle(x, y, 1/2, 1/2);                                                
                    }
                }
            }

            foreach (Rectangle tileRect in  m_tileRect)
            {
                if (a_player.m_playerRect.Intersects(tileRect))
                {
                    return true;
                }
            }

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
