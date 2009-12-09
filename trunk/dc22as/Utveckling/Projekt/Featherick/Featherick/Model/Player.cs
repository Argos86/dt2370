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
        private const float MoveStickScale = 1.0f;
        public const float MaxFallSpeed = 500.0f;
        public const float MaxRunSpeed = 500.0f;
        private const Buttons JumpButton = Buttons.A;

        public int m_health;

        public Vector2 m_pos;
        public Vector2 m_velocity;
        public Vector2 m_size = new Vector2(1, 2);
  
        public float m_acceleration = 20.0f;

        public bool m_collideGround;
        public bool m_isJumping;

        public Point m_frameSize = new Point(200, 200);
        public Point m_currentFrame = new Point(0, 0);
        public Point m_sheetSize = new Point(8, 1);

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
        
        public Player(Vector2 a_startPos)
        {
            m_health = 3;
            m_pos = a_startPos;
        }       

        public void Update()
        {
            float length = m_velocity.Length();
            float maxRunSpeed = 16;

            if (length >= maxRunSpeed)
            {
                m_velocity /= length;
                m_velocity *= maxRunSpeed;
            }
        }

        public void ResetPlayer(Vector2 a_startPos)
        {
            m_health = 3;
            m_pos = a_startPos;
            m_velocity.X = 0.0f;
            m_velocity.Y = 0.0f;
        }

        public void WalkLeft(float a_elapsedTime)
        {
            m_velocity.X += -m_acceleration * a_elapsedTime;
            m_walkLeft = true;
        }

        public void WalkRight(float a_elapsedTime)
        {
            m_velocity.X += m_acceleration * a_elapsedTime;
            m_walkRight = true;
        }


        //TODO: dubbelhopp? YES!
        public void DoJump()
        {
            if (m_collideGround == true)
            {                

                m_velocity.Y = -16.0f;
                m_isJumping = false;
            }            
        }
    }
}
