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
        public const int MAX_BULLETS = 100;
        public const float GRAVITY = 20.0f;
        public const float ACCELERATION = 100.0f;
        public const float MAX_FALL_SPEED = 800.0f;
        public const float JUMP_SPEED = -14.0f;
        
        
        public int m_hitPoints;
        public int m_bulletIndex;
        public float m_bulletDelay;

        public float m_movement;
        public float m_maxSpeed;
        public float m_jumpTime;

        public bool m_isJumping;
        public bool m_isOnGround;
        public bool m_isFacingRight;

        public Vector2 m_velocity;
        public Vector2 m_pos;
        public Vector2 m_size = new Vector2(0.9f,1.8f);

        public Bullet[] m_bullets;

        
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
            m_bullets = new Bullet[MAX_BULLETS];
            m_bulletIndex = 0;
            m_bulletDelay = 0.2f;
        }

        public void Shoot(bool a_isPressed, float a_elapsedTime)
        {
            if (a_isPressed && m_bulletDelay < 0)
            {
                if (m_bulletIndex > 99)
                {
                    m_bulletIndex = 0;
                } 
                m_bullets[m_bulletIndex] = new Bullet(m_pos, Vector2.Zero);
                    m_bulletIndex++;
            }
            else 
            {
                m_bulletDelay -= 0.01f;
            }
        }

        public void DoJump()
        {
            m_velocity.Y += JUMP_SPEED;
        }

        public void SetMovement(float a_movement, bool a_isJumping)
        {
            m_movement = a_movement;
            m_isJumping = a_isJumping;

            if (!m_isOnGround)
            {
                m_movement = m_movement / 3;
            }

        }
        public void UpdateMovement(float a_elapsedTime)
        {
            m_velocity.X += m_movement * ACCELERATION * a_elapsedTime;
            m_velocity.X = MathHelper.Clamp(m_velocity.X, -m_maxSpeed, m_maxSpeed);
            
            m_velocity.Y += (GRAVITY * a_elapsedTime);
            m_velocity.Y = MathHelper.Clamp(m_velocity.Y, -MAX_FALL_SPEED, MAX_FALL_SPEED);

            if (m_movement == 0 && m_isOnGround)
            {
                m_velocity.X -= m_velocity.X * (float)Math.Pow(3.0f, 2.0f) * a_elapsedTime;
                if (Math.Abs(m_velocity.X) < 1.0f)
                {
                    m_velocity.X = 0;
                }
            }

            if (m_movement == 0 && !m_isOnGround)
            {
                m_velocity.X -= m_velocity.X * 1.0f * a_elapsedTime;
            }

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
            //Check if we will shoot

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
