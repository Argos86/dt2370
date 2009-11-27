using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Featherick.Model
{
    class Player
    {


        // Input configuration
        private const float MoveStickScale = 1.0f;
        private const Buttons JumpButton = Buttons.A;        

        public int m_health;
        public Vector2 m_pos;
        public Vector2 m_velocity;        
        public bool m_collideGround;
        public float m_timer = 0.0f;

        public Point m_frameSize = new Point(200, 200);
        public Point m_currentFrame = new Point(0, 0);
        public Point m_sheetSize = new Point(8, 1);

        // Jumping state
        public bool m_isJumping;

        //movement state
        public bool m_walkRight;
        public bool m_walkLeft;
        
        public bool WalksToRight()
        {
            return m_walkRight;
        }

        public bool WalksToLeft()
        {
            return m_walkLeft;
        }

        public bool IsJumping()
        {
            return m_isJumping;
        }

        public bool IsAlive()
        {
            return m_health > 0;
        }
        
        public Player()
        {
            m_health = 3;            
            m_pos = new Vector2(45, 3);
            m_timer = 0.0f;           
        }       



        public bool Update(float a_elapsedTime)
        {
            m_timer -= a_elapsedTime;
            if (IsAlive() == false)
            {
                return false;
            }

            return true;
        }

        public void DoJump()
        {
            if (m_collideGround == true)
            {
                m_velocity.Y = -13.0f;
                m_isJumping = false;
            }
            
        }
    }
}
