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
        public const int HEIGHT = 20;//höjde från 10

        public Rectangle[,] m_tileRect = new Rectangle[WIDTH,HEIGHT];
        
        //en tile är ~0.5 m 


        public Level()
        {
            m_tiles = new Tile[WIDTH, HEIGHT];
            Clear();

        }

        public void Clear()
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
            CreateWall(WIDTH-1);
            CreateFloor(HEIGHT-1);
            m_tiles[60, 7].m_tileType = Tile.TileType.Clear;
            m_tiles[55, 5].m_tileType = Tile.TileType.Blocked;
            m_tiles[55, 6].m_tileType = Tile.TileType.Blocked;
            m_tiles[52, 3].m_tileType = Tile.TileType.Blocked;
            m_tiles[48, 3].m_tileType = Tile.TileType.Blocked;
            m_tiles[43, 3].m_tileType = Tile.TileType.Blocked;
            m_tiles[35, 4].m_tileType = Tile.TileType.Blocked;

        }

        public void CreateFloor(int a_level)
        {
            for (int x = 0; x < WIDTH; x++)
            {
                m_tiles[x, a_level].m_tileType = Tile.TileType.Blocked;
            }
        }

        public void CreateWall(int a_level)
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                m_tiles[a_level, y].m_tileType = Tile.TileType.Blocked;
            }
        }

        public bool IsCollidingAt(Vector2 a_pos, Vector2 a_size) 
        {
            //a_pos.Y + 0.1f minskar höjden på karaktären
            Vector2 TopLeft = new Vector2(a_pos.X, a_pos.Y + 0.1f);

            //...a_size.X - 0.1f gör att jag blir smalare än en tile och kan trilla i hål
            //...a_size.Y - 0.05f gör att jag har "kontakt med marken" och att jag kan gå under en blockerad tile som är 3 tiles hög            
            Vector2 BottomRight = new Vector2(a_pos.X + a_size.X - 0.1f, a_pos.Y + a_size.Y - 0.05f);


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
                }
                
            }

            return false;
        }

        public void Test()
        {
            Clear();
            IsCollidingAt(new Vector2(31, 8.1f), new Vector2(1, 1));
        }
    }
}
