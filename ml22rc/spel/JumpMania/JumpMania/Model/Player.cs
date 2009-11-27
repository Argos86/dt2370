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
    class Player 
    {

        /*public Vector2 m_veli = new Vector2(0, 0);
        float m_acceleration = 0.7f;*/
        public float m_movement = 0.0f;
        float a_speed = 600.0f;
        bool m_isjumping = false;
        bool m_ismoving = false;
        public bool m_onground = true;


        public int m_hitPoints = 0;
        public Vector2 m_position;
        public Vector2 m_oldPos;
        public Vector2 m_velocity;
        public bool m_collideGround;
        
        public bool IsAlive() {
            return m_hitPoints > 0;
        }

        

        public Player()
        {
            m_hitPoints = 0;
            m_position = new Vector2(4, 2);
            m_oldPos = new Vector2(0, 0);
            m_velocity = new Vector2(0, 0);
                
        }

        public Player(Vector2 a_pos)
        {
            m_hitPoints = 10;
            m_position = a_pos;
            m_oldPos = m_position;
                  
        }


        //int m_hitPoints = 0;
        //public Vector2 m_oldPos = new Vector2(0, 0);
        //public Vector2 m_velocity = new Vector2(0, 0);

        //public Vector2 m_position = new Vector2(400, 200);

        public void UpdateJump(GameTime theGameTime)
        {
            m_isjumping = true;
        }



        public void UpdateWalkRight(GameTime theGameTime)
        {
            m_ismoving = true;
        }


        public void UpdateWalkLeft(GameTime theGameTime)
        {
            m_ismoving = true;
        }

        public void Update(GameTime theGameTime)
        {

           /* m_position.Y += m_veli * (float)theGameTime.ElapsedGameTime.TotalSeconds;

            m_veli = 25; 

            if (m_isjumping == true)
            {
                m_veli = m_veli - m_acceleration;
                m_position.Y = m_position.Y - m_veli;

                if (m_onground == true)
                {
                    m_isjumping = false;
                    m_veli = 0;
                }
            }*/

            /*Vector2 gravity = new Vector2(0, 9.82f);
            m_veli += theGameTime * gravity;
            Vector2 newPos = m_position + m_veli * theGameTime;*/


            if (m_ismoving == true)
            {
                float elapsed = (float)theGameTime.ElapsedGameTime.TotalSeconds;
                m_position.X += a_speed * elapsed * m_movement;
                m_movement = 0.0f;
            }
        }

    }

}
