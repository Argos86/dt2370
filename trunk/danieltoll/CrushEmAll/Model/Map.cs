using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace ZombieHoards.Model
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
        public const int WIDTH = 48;
        public const int HEIGHT = 32;

        public Map()
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

        public AStar.SearchResult InitGetPath(Vector2 a_from, Vector2 a_to, ref AStar a_searcher) {
            

            //Om målet finns i ett rum utan ingångar så gör vi en mindre sökning baklänges
            AStar back = new AStar(this);
            back.InitSearch(a_to, a_from, false, 0.0f);
            if (back.Update(50) == AStar.SearchResult.SearchFailedNoPath) {
                return AStar.SearchResult.SearchFailedNoPath;
            }

            //initiera sökningen
            a_searcher.InitSearch(a_from, a_to, false, 0.0f);
            return AStar.SearchResult.SearchNotDone;
        }

      

        //Helper method for LineOfSight
        private bool IsBlocked(float a_x, float a_y, float a_dx, float a_dy)
        {
            if (IsClear(new Vector2(a_x, a_y)) == false)
            {
                return true;
            }
            if (IsClear(new Vector2((float)a_x - a_dx, a_y - a_dy)) == false)
            {
                return true;
            }

            return false;
        }

        public bool LineOfSight(Vector2 a_from, Vector2 a_to)
        {
            Vector2 dir = a_to - a_from;
	        dir.Normalize();

            if (dir.X > 0.0f) {
		        for (int x = (int)a_from.X+1; (float)x < a_to.X; x++) {
			        float u = ((float)x - a_from.X) / dir.X;
			        float y = a_from.Y + dir.Y * u;
                    if (IsBlocked(x, y, 0.01f, 0.0f)) {
                        return false;
                    }
			
		        }
	        } else if (dir.X < 0.0f) {
		        for (int x = (int)a_from.X; (float)x > a_to.X; x--) {
			        float u = ((float)x - a_from.X) / dir.X;
			        float y = a_from.Y + dir.Y * u;
                    if (IsBlocked(x, y, 0.01f, 0.0f)) {
                        return false;
                    }
		        }
	        }

	        //y
	        if (dir.Y > 0.0f) {
		        for (int y = (int)a_from.Y+1; (float)y < a_to.Y; y++) {
			        float u = ((float)y - a_from.Y) / dir.Y;
			        float x = a_from.X + dir.X * u;
                    if (IsBlocked(x, y, 0.0f, 0.01f)) {
                        return false;
                    }
        			
		        }
	        } else if (dir.Y < 0.0f) {
		        for (int y = (int)a_from.Y; (float)y > a_to.Y; y--) {
			        float u = ((float)y - a_from.Y) / dir.Y;
			        float x = a_from.X + dir.X * u;
                    if (IsBlocked(x, y, 0.0f, 0.01f)) {
                        return false;
                    }
		        }
	        }

            return true;
        }

        public bool Test()
        {
            AStar searcher;

            searcher = new AStar(this);
            InitGetPath(new Vector2(5, 0), new Vector2(6, 0), ref searcher);
            searcher.Update(100);
            if (searcher.m_path.Count != 1)
            {
                return false;
            }
            //searcher = new AStar(this);
            InitGetPath(new Vector2(5, 0), new Vector2(7, 0), ref searcher);
            searcher.Update(100);
            if (searcher.m_path.Count != 2)
            {
                return false;
            }

           // searcher = new AStar(this);
            m_tiles[6, 1].m_tileType = Tile.TileType.Blocked;
            InitGetPath(new Vector2(5, 1), new Vector2(7, 1), ref searcher);
            searcher.Update(100);
            if (searcher.m_path.Count != 4)
            {
                return false;
            }
            //searcher = new AStar(this);
            InitGetPath(new Vector2(4, 1), new Vector2(7, 1), ref searcher);
            searcher.Update(100);
            if (searcher.m_path.Count != 4)
            {
                return false;
            }

            if (LineOfSight(new Vector2(5, 0), new Vector2(7, 0)) == false)
            {
                return false;
            }
            if (LineOfSight(new Vector2(5, 1), new Vector2(7, 1)) == true)
            {
                return false;
            }
            if (LineOfSight(new Vector2(5, 0), new Vector2(7, 2)) == true)
            {
                return false;
            }


            return true;
        }

        public void CreateTestMap(Random a_r)
        {
            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    m_tiles[x, y] = new Tile();
                }
            }
           
             
            //create a few buildings
            int minSize = 4;
            int maxSize = 16;
            for (int i = 0; i < 10; i++)
            {
                Rectangle r = new Rectangle(a_r.Next() % (WIDTH - maxSize), a_r.Next() % (HEIGHT - maxSize), minSize + a_r.Next() % (maxSize - minSize), minSize + a_r.Next() % (maxSize - minSize));

                for (int x = r.X; x < r.X + r.Width; x++)
                {
                    for (int y = r.Y; y < r.Y + r.Height; y++)
                    {
                        m_tiles[x, y].m_tileType = Tile.TileType.Blocked;
                    }
                }
                m_tiles[r.X, r.Y + r.Height / 2].m_tileType = Tile.TileType.Clear;
                m_tiles[r.X + r.Width - 1, r.Y + r.Height / 2].m_tileType = Tile.TileType.Clear;
                m_tiles[r.X + r.Width / 2, r.Y].m_tileType = Tile.TileType.Clear;
                m_tiles[r.X + r.Width / 2, r.Y + r.Height -1].m_tileType = Tile.TileType.Clear;
                r.X += 1;
                r.Y += 1;
                r.Width -= 2;
                r.Height -= 2;
                for (int x = r.X; x < r.X + r.Width; x++)
                {
                    for (int y = r.Y; y < r.Y + r.Height; y++)
                    {
                        m_tiles[x, y].m_tileType = Tile.TileType.Clear;
                    }
                }
            }
        }
    }
}
