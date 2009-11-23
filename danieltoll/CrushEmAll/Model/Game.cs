using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ZombieHoards.Model
{
    class Game : IModel
    {
        public ZombieHoards.Model.Map m_map;
        public Character m_player;// = new Character();
        public Character[] m_enemies;
        
        
        public const int MAX_ENEMIES = 200;
        

        public ZombieHoards.IEventTarget m_view;

        public Game(ZombieHoards.IEventTarget a_view)
        {
            m_view = a_view;
            Init();
        }

        

        private void Init()
        {
            m_map = new Map();
            
            m_enemies = new Character[MAX_ENEMIES];
            for (int i = 0; i < MAX_ENEMIES; i++)
            {
                m_enemies[i] = new Character();
            }

            m_player = new Character();

           
        }

       

        
        private int GetDeadest(int a_max, Character[] a_arr)
        {
            
            for (int i = 0; i < a_max; i++)
            {
                if (a_arr[i].IsAlive() == false)
                {
                    return i;
                }
            }

            return -1;
        }
        

       

       
        public void Draw(bool a_blocked, Vector2 a_at)
        {
            a_at = Model.MathHelper.Clamp(new Vector2(0, 0), new Vector2(Map.WIDTH-1, Map.HEIGHT-1), a_at);
            m_map.m_tiles[(int)a_at.X, (int)a_at.Y].m_tileType = a_blocked ? Tile.TileType.Blocked : Tile.TileType.Clear;
        }

        public bool Update(float a_gameTime) {

            //no civilians alive return false
            if (IsGameOver() == true || HasWon() == true)
            {
                return false;
            }

           
            //Update enemies
            for (int i = 0; i < MAX_ENEMIES; i++)
            {
                UpdateEnemy(i, a_gameTime);
            }

            UpdatePlayer(a_gameTime);


          

            return true;
        }

        public bool IsGameOver()
        {
            return false;
        }

        public bool HasWon()
        {
            return false;
        }

       

        public void UpdateEnemy(int a_enemy, float a_gameTime)
        {
            if (m_enemies[a_enemy].Update(a_gameTime) == false)
            {
                return;
            }
            

            
        }

        public void UpdatePlayer(float a_gameTime)
        {
            Vector2 gravity = new Vector2(0, 9.82f);
            m_player.m_velocity += a_gameTime * gravity ;
            Vector2 newPos = m_player.m_pos + m_player.m_velocity * a_gameTime;


            m_player.m_collideGround = false;
            m_player.m_pos = Collide(m_player.m_pos, newPos, new Vector2(1, 1), ref m_player.m_velocity, ref m_player.m_collideGround);
            

            if (m_player.Update(a_gameTime) == false)
            {
                return;
            }

           
            
        }

        public Vector2 Collide(Vector2 a_oldPos, Vector2 a_newPos, Vector2 a_size, ref Vector2 a_velocity, ref bool a_outCollideGround)
        {
            if (m_map.IsCollidingAt(a_newPos, a_size))
            {

                //try X movement
                Vector2 xMove = new Vector2(a_newPos.X, a_oldPos.Y);
                if (m_map.IsCollidingAt(xMove, a_size) == false)
                {
                    a_velocity.Y = 0.0f;
                    //a_velocity.X *= 0.90f;// friction should be time-dependant
                    a_outCollideGround = true;
                    return xMove;
                }

                //try Y movement
                Vector2 yMove = new Vector2(a_oldPos.X, a_newPos.Y);
                if (m_map.IsCollidingAt(yMove, a_size) == false)
                {
                    a_velocity.X = 0.0f;
                    //a_velocity.Y *= 0.90f; // friction should be time-dependant
                    return yMove;
                }

                return a_oldPos;
            }

            return a_newPos;
        }
       
        

        public bool Test()
        {

            m_map.Clear();
            m_player.m_pos.Y = 10.0f;

            UpdatePlayer(0.5f);
            //FALLER VI 
            if (m_player.m_pos.Y <= 10.0f)
            {
                return false;
            }

            m_map.CreateFloor(9);
            m_player.m_velocity = new Vector2();
            m_player.m_pos.X = 4.0f;
            m_player.m_pos.Y = 9.0f;
            UpdatePlayer(0.05f);

            //INTE RAMLA GENOM GOLVET
            if (m_player.m_pos.Y != 9.0f)
            {
                return false;
            }

            
            m_player.m_velocity = new Vector2(1, 0);
            m_player.m_pos.X = 4.0f;
            m_player.m_pos.Y = 8.99f;
            UpdatePlayer(0.05f);
            //inte ramla snett genom golvet
            //och glida längs med golvet
            if (m_player.m_pos.Y != 8.99f || m_player.m_pos.X <= 4.0f && m_player.m_velocity.Y == 0.0f)
            {
                return false;
            }

            m_map.CreateWall(5);
            m_player.m_velocity = new Vector2(1, 0);
            m_player.m_pos.X = 4.49f;
            m_player.m_pos.Y = 5.0f;
            UpdatePlayer(0.05f);

            if (m_player.m_pos.Y <= 5.0f || m_player.m_pos.X != 4.49f && m_player.m_velocity.X == 0.0f)
            {
                return false;
            }
            


            //Character target = null;
            //GetClosestVisibleHuman(m_enemies[0], 1, target);

            return true;
        }

        public void SetupTestGameNoMap()
        {

            m_enemies = new Character[MAX_ENEMIES];
            for (int i = 0; i < MAX_ENEMIES; i++)
            {
                m_enemies[i] = new Character();
            }

           
        }

        public void SetupTestGame()
        {
            Random p = new Random();

            Init();
            //Load a map
            m_map.CreateTestMap(p);


            m_player.m_hitPoints = 1;
            m_player.m_velocity.X = 5.0f;
            m_player.m_pos = new Vector2(Map.WIDTH / 2, Map.HEIGHT / 2);


            SetupTestGameNoMap();
        }
    }
}
