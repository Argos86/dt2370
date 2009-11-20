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

        public void Draw(bool a_blocked, Vector2 a_at)
        {

        }

        public bool Update(float a_gameTime)
        {

            UpdatePlayer(a_gameTime);
            
            return true;
        }
        
        public void UpdatePlayer(float a_gameTime)
        {
            /*if (m_player.Update(a_gameTime) == false)
            {
                return;
            } */

            Vector2 m_newPos = new Vector2(m_player.m_pos.X, m_player.m_pos.Y);
            if (m_player.WalksToRight())
            {
                m_newPos.X += a_gameTime * 8.0f;
            }
            if (m_player.WalksToLeft())
            {
                m_newPos.X -= a_gameTime * 8.0f;
            }
            if (m_player.IsJumping())
            {
 
            }

            m_newPos.Y += a_gameTime * 5.0f;



            
            if (m_level.Collide(m_newPos, m_player) == false)
            {
                m_player.m_pos = m_newPos;
            } else if (m_level.Collide(new Vector2(m_newPos.X, m_player.m_pos.Y), m_player) == false)
            {
                m_player.m_pos = m_newPos;
            }

            if (m_level.Collide(m_newPos, m_player) == true) 
            {
                
            }

            /*else
            {
                m_player.m_pos.Y = m_level.GetTileTop(m_player.m_pos);
            }*/

        }

        private void Attack(Player a_player, Player a_enemy, bool a_isEnemy)
        {
            if (a_isEnemy)
            {
                a_enemy.m_health -= 1;
            }
            else
            {

                a_enemy.m_health -= 1;
            }

            m_view.Attack(a_player.m_pos, a_enemy.m_pos, a_isEnemy);
        }


    }
}
