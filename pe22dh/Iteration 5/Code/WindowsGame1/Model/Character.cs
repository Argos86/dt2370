using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CombatLand.Model
{
    class Character
    {
        public const int MAX_HP = 10;
        public const float GRAVITY = 20.0f;
        public const float ACCELERATION = 200.0f;
        public const float MAX_FALL_SPEED = 800.0f;
        public const float JUMP_SPEED = -15.0f;
        
        
        public int m_hitPoints;

        public float m_movement;
        public float m_maxSpeed;
        public float m_jumpTime;

        public bool m_isJumping;
        public bool m_isOnGround;
        public bool m_isFacingRight;

        public Vector2 m_velocity;
        public Vector2 m_pos;
        public Vector2 m_size = new Vector2(0.9f,1.8f);
        
        public bool IsAlive()
        {
            return m_hitPoints > 0;
        }

        public Character(float a_maxSpeed)
        {
            m_hitPoints = 10;
            m_pos = new Vector2(10.0f, 10.0f);
            m_maxSpeed = a_maxSpeed;
            m_movement = 0.0f;
            m_velocity = new Vector2(0, 0);
            m_jumpTime = 0.0f;
            m_isFacingRight = true;
        }

        public void DoJump()
        {
            m_velocity.Y += JUMP_SPEED;
        }

        public void SetMovement(float a_movement, bool a_isJumping)
        {
            m_movement = a_movement;
            m_isJumping = a_isJumping;
        }
        public void UpdateMovement(float a_elapsedTime)
        {
            m_velocity.X = (m_movement * ACCELERATION);
            m_velocity.X = MathHelper.Clamp(m_velocity.X, -m_maxSpeed, m_maxSpeed);
            
            m_velocity.Y += (GRAVITY * a_elapsedTime);
            m_velocity.Y = MathHelper.Clamp(m_velocity.Y, -MAX_FALL_SPEED, MAX_FALL_SPEED);


            if (m_isJumping && m_isOnGround)
            {
                DoJump();
                m_jumpTime = 1.0f;
            }
        
            else if (m_jumpTime > 0.0f && m_jumpTime <= 1.0f)
            {
                m_jumpTime -= 0.02f;
            }
            else 
            {
                m_jumpTime = 0.0f;
            }
            
            //Determine facing

            if (m_movement > 0) 
            {
                m_isFacingRight = true;
            }
            else if(m_movement < 0)
            {
                m_isFacingRight = false;
            }
            
        }
        public Vector2 CalculateNewPosition(float a_elapsedTime)
        {
            Vector2 newPos;
            newPos.X = m_pos.X + (m_velocity.X * a_elapsedTime);
            newPos.Y = m_pos.Y + (m_velocity.Y * a_elapsedTime);

            return newPos;
        }


    }
}
