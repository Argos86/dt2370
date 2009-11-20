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
        
        public float m_veli = 0;
        float m_acceleration = 0.7f;
        public float m_movement = 0.0f;
        float a_speed = 600.0f;
        bool m_isjumping = false;
        bool m_ismoving = false;
        public bool m_onground = true;



        public Vector2 m_position = new Vector2(400, 200);

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

            m_position.Y += m_veli * (float)theGameTime.ElapsedGameTime.TotalSeconds;

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
            }

            if (m_ismoving == true)
            {
                float elapsed = (float)theGameTime.ElapsedGameTime.TotalSeconds;
                m_position.X += a_speed * elapsed * m_movement;
                m_movement = 0.0f;
            }
        }

    }

}
