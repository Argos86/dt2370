using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Featherick.Model
{
    class Game : IModel
    {
        public Player m_player;

        public Level m_level;

        public int m_time = 0;

        public float GetTimeSeconds(GameTime a_gameTime)
        {
            return 1.0f * (float)a_gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;
        }

        public Featherick.IEventTarget m_view;

        public Game(Featherick.IEventTarget a_view)
        {
            m_view = a_view;
            Init();
        }

        private void Init()
        {
            m_level = new Level();
            m_player = new Player();
            
        }

        public bool IsGameOver()
        {
            if(!m_player.IsAlive())
            {
                return true;
            }
            return false;            
        }

        public bool HasWon()
        {
            return false;
        }

        public bool Update(float a_gameTime)
        {

            UpdatePlayer(a_gameTime);
            
            return true;
        }
        
        public void UpdatePlayer(float a_gameTime)
        {

            Vector2 gravity = new Vector2(0, 25.0f);
            m_player.m_velocity += a_gameTime * gravity;
            Vector2 m_newPos = m_player.m_pos + m_player.m_velocity * a_gameTime;
            
            m_player.m_collideGround = false;
            m_player.m_pos = Collide(m_player.m_pos, m_newPos, new Vector2(1, 2), ref m_player.m_velocity, ref m_player.m_collideGround); 


            if (m_player.WalksToRight())
            {
                m_newPos.X += a_gameTime * 10.0f;
            }
            else if (m_player.WalksToLeft())
            {
                m_newPos.X -= a_gameTime * 10.0f;
            }
            else
            {
                m_player.m_velocity.X *= 0.85f;
            }

        }

        public Vector2 Collide(Vector2 a_oldPos, Vector2 a_newPos, Vector2 a_size, ref Vector2 a_velocity, ref bool a_outCollideGround)
        {
           if(m_level.IsCollidingAt(a_newPos, a_size))
           {

                //X movement
               Vector2 xMove = new Vector2(a_newPos.X, a_oldPos.Y);
               if(m_level.IsCollidingAt(xMove, a_size) == false)
               {
                   a_velocity.Y = 0.0f;
                   a_outCollideGround = true;
                   return xMove;
               }

               //Y movement
               Vector2 yMove = new Vector2(a_oldPos.X, a_newPos.Y);
               if(m_level.IsCollidingAt(yMove, a_size) == false)
               {
                   a_velocity.X = 0.0f;
                   return yMove;
               }

               a_velocity.Y = 0.0f;
               a_velocity.X = 0.0f;
               return a_oldPos;
           }

           return a_newPos;
        }

        private void Attack(Player a_player, Player a_enemy, bool a_isEnemy)
        {
            if (a_isEnemy)
            {
                a_enemy.m_health -= 1;
            }
            else
            {

                a_player.m_health -= 1;
            }

            m_view.Attack(a_player.m_pos, a_enemy.m_pos, a_isEnemy);
        }



        
    }
}
