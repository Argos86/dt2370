﻿using System;
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
        public bool m_over = false;


        public void Init()
        {
            m_player = new JumpMania.Model.Player();
            m_level = new JumpMania.Model.Level();
        }

        public void Update(GameTime theGameTime)
        {
            //_________________________Slutar uppdaterar om man nuddar golvet____________________________
            if (m_over == false)
            {
                m_player.Update(theGameTime);

                m_level.UpdateFloor(theGameTime); // <-- Uppdaterar golvet

                IsGameOver();

                Vector2 gravity = new Vector2(0, 9.82f);
                float elapsed = (float)theGameTime.ElapsedGameTime.TotalSeconds;
                m_player.m_velocity += elapsed * gravity;
                Vector2 newPos = m_player.m_position + m_player.m_velocity * elapsed;


                m_player.m_collideGround = false;
                m_player.m_position = Collide(m_player.m_position, newPos, new Vector2(1, 1), ref m_player.m_velocity, ref m_player.m_collideGround);
            }
        }

        public Vector2 Collide(Vector2 a_oldPos, Vector2 a_newPos, Vector2 a_size, ref Vector2 a_velocity, ref bool a_outCollideGround)
        {
            if (m_level.IsCollidingAt(a_newPos, a_size))
            {
                Vector2 xMove = new Vector2(a_newPos.X, a_oldPos.Y);
                if (m_level.IsCollidingAt(xMove, a_size) == false)
                {
                    a_velocity.Y = 0.0f;
                    if (a_newPos.Y > a_oldPos.Y)
                    {
                        xMove.Y = (int)(a_newPos.Y + a_size.Y) - a_size.Y;
                        a_outCollideGround = true;
                    }
                    return xMove;
                }

                Vector2 yMove = new Vector2(a_oldPos.X, a_newPos.Y);
                if (m_level.IsCollidingAt(yMove, a_size) == false)
                {
                    a_velocity.X = 0.0f;
                    return yMove;
                }

                a_velocity = Vector2.Zero;
                a_outCollideGround = true;
                return a_oldPos;
            }
            return a_newPos;
        }


        public void IsGameOver()
        {
            if (m_level.IsTouchingFloor(m_player.m_position, new Vector2(1, 1)) == false)
            {
                m_over = false;
            }
            else
            {
                m_over = true;
            }
        }

        /*public bool HasWon()
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
