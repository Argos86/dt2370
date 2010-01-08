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
                                            // <-- välja två vägar, ha tiles som höjer golvet
        public Player m_player;
        public Level m_level;
        public bool m_over = false;
        public bool m_won = false;
        public int m_levelLoaded = 0;

         // skickar med int beroende på bana
        public void LoadLevel(int a_levelnumber)
        {
            switch(a_levelnumber)
            {
                case 1:
                    {
                        m_player = new JumpMania.Model.Player();
                        m_level = new JumpMania.Model.Level("2.txt");
                        m_levelLoaded = 1;
                        break;
                    }
                case 2:
                    {
                        m_player = new JumpMania.Model.Player();
                        m_level = new JumpMania.Model.Level("3.txt");
                        m_levelLoaded = 2;
                        break;
                    }
                case 3:
                    {
                        m_player = new JumpMania.Model.Player();
                        m_level = new JumpMania.Model.Level("1.txt");
                        m_levelLoaded = 3;
                        break;
                    }
                case 4:
                    {
                        m_player = new JumpMania.Model.Player();
                        m_level = new JumpMania.Model.Level("4.txt");
                        m_levelLoaded = 3;
                        break;
                    }
                case 5:
                    {
                        m_player = new JumpMania.Model.Player();
                        m_level = new JumpMania.Model.Level("5.txt");
                        m_levelLoaded = 3;
                        break;
                    }
                default:
                    Console.WriteLine("Default case"); // Bild: Du vann! Tjiho!
                    break;
            }
        }

        public void Update(GameTime theGameTime)
        {
      
                    m_player.Update(theGameTime);
                    

                    m_level.UpdateFloor(theGameTime); // <-- Uppdaterar golvet

                    IsGameOver();
                    HasWon();

                    Vector2 gravity = new Vector2(0, 30.0f);
                    float elapsed = (float)theGameTime.ElapsedGameTime.TotalSeconds;
                    m_player.m_velocity += elapsed * gravity;
                    Vector2 newPos = m_player.m_position + m_player.m_velocity * elapsed;


                    m_player.m_collideGround = false;
                    m_player.m_position = Collide(m_player.m_position, newPos, new Vector2(1, 2), ref m_player.m_velocity, ref m_player.m_collideGround);

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
            if (m_level.IsTouchingFloor(m_player.m_position, new Vector2(1, 1.5f)) == false)
            {
                m_over = false;
            }
            else
            {
                m_over = true;
            }
        }

        public void HasWon()
        {
                if (m_level.IsTouchingGStar(m_player.m_position, new Vector2(1,1)) == true)
                {
                    m_won = true;
                }
                else
                {
                    m_won = false;
                }
        }
    }
}
