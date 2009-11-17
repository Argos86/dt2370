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

        public Map m_map;

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
            m_map = new Map();
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

            Vector2 newPos = new Vector2(m_player.m_pos.X, m_player.m_pos.Y);
            if (m_player.WalksToRight())
            {
                newPos.X += a_gameTime * 8.0f;
            }
            if (m_player.WalksToLeft())
            {
                newPos.X -= a_gameTime * 8.0f;
            }
            if (m_player.IsJumping())
            {
 
            }

            newPos.Y += a_gameTime * 5.0f;



            
            if (m_map.Collide(newPos) == false)
            {
                m_player.m_pos = newPos;
            } else if (m_map.Collide(new Vector2(newPos.X, m_player.m_pos.Y)) == false)
            {
                m_player.m_pos = newPos;
            }

            /*else
            {
                m_player.m_pos.Y = m_map.GetTileTop(m_player.m_pos);
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
