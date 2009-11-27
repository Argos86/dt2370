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
    class Game
    {

        public Player m_player;
        public Level m_level;


        public void Init()
        {
            m_player = new JumpMania.Model.Player();
            m_level = new JumpMania.Model.Level();
        }

        public void Update(GameTime theGameTime)
        {
            m_player.Update(theGameTime);

            Vector2 gravity = new Vector2(0, 300.0f);
            float elapsed = (float)theGameTime.ElapsedGameTime.TotalSeconds;
            m_player.m_velocity += elapsed * gravity;
            Vector2 newPos = m_player.m_position + m_player.m_velocity * elapsed;

            
            m_player.m_collideGround = false;
            m_player.m_position = Collide(m_player.m_position, newPos, new Vector2(1, 1), ref m_player.m_velocity, ref m_player.m_collideGround);

        }

        public Vector2 Collide(Vector2 a_oldPos, Vector2 a_newPos, Vector2 a_size, ref Vector2 a_velocity, ref bool a_outCollideGround)
        {
            if (m_level.IsCollidingAt(a_newPos, a_size))
            {

                //try X movement
                Vector2 xMove = new Vector2(a_newPos.X, a_oldPos.Y);
                if (m_level.IsCollidingAt(xMove, a_size) == false)
                {
                    a_velocity.Y = 0.0f;
                    //a_velocity.X *= 0.90f;// friction should be time-dependant
                    a_outCollideGround = true;
                    return xMove;
                }

                //try Y movement
                Vector2 yMove = new Vector2(a_oldPos.X, a_newPos.Y);
                if (m_level.IsCollidingAt(yMove, a_size) == false)
                {
                    a_velocity.X = 0.0f;
                    //a_velocity.Y *= 0.90f; // friction should be time-dependant
                    return yMove;
                }
                a_velocity.Y = 0.0f;
                a_velocity.X = 0.0f;

                return a_oldPos;
            }

            return a_newPos;
        }


        /*public bool IsGameOver()
        {
            for (int i = 0; i < MAX_CIVILIANS; i++)
            {
                if (m_civilians[i].IsAlive())
                {
                    return false;
                }
            }
            return true;
        }

        public bool HasWon()
        {
            for (int i = 0; i < MAX_WAVES; i++)
            {
                if (m_waves[i].m_isActive == true)
                {
                    return false;
                }
            }
            for (int i = 0; i < MAX_ENEMIES; i++)
            {
                if (m_enemies[i].IsAlive() == true)
                {
                    return false;
                }
            }
            return true;
        }*/
    }
}
