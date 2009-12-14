using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace MeCloidGame.Model
{
    class Player
    {
        // TODO: Redo jumping to more mimic platform starter kit with jumphight depending on time button is pressed.
        // TODO: Add weapons.
        // TODO: Make it possible to die.
        public Vector2 m_pos;
        public Vector2 m_velocity;
        public float m_movement;

        public bool m_isOnGround;

        public const float WIDTH = 70.0f / 48.0f;
        public const float HEIGHT = 130.0f / 48.0f;

        private const float Gravity = 9.82f;
        private const float MaxFallSpeed = 15.0f;
        private const float MaxMoveSpeed = 8.0f;
        private const float Acceleration = 20.0f;

        public bool m_isJumping;
        private bool m_wasJumping;
        private float m_jumpTime;
        private const float MaxJumpTime = 1.0f;

        public Player()
        {
            m_pos = new Vector2(2.0f, 47.0f);
        }

        public bool UpdateVelocity(float a_elapsedTime)
        {
            m_velocity.X += m_movement * Acceleration * a_elapsedTime;

            if (m_movement == 0 && m_isOnGround)
            {
                m_velocity.X -= m_velocity.X * (float)Math.Pow(2.0f, 2.0f) * a_elapsedTime;
                if ( Math.Abs(m_velocity.X) < 1.0f)
                {
                    m_velocity.X = 0;
                }
            }

            if (m_movement == 0 && !m_isOnGround)
            {
                m_velocity.X -= m_velocity.X * 1.0f * a_elapsedTime;
            }

            m_velocity.X = MathHelper.Clamp(m_velocity.X, -MaxMoveSpeed, MaxMoveSpeed);
            
            m_velocity.Y += Gravity * a_elapsedTime;
            m_velocity.Y = MathHelper.Clamp(m_velocity.Y, -MaxFallSpeed, MaxFallSpeed);

            if (m_isJumping && m_isOnGround)
            {
                m_velocity.Y = -9.0f;
            }
            //m_velocity.Y = DoJump(m_velocity.Y);

            if (!m_isOnGround)
            {
                m_velocity.X -= m_velocity.X * 0.5f * a_elapsedTime;
            }

            if (m_isOnGround)
            {
                m_velocity.X -= m_velocity.X * 0.5f * a_elapsedTime;
            }

            return true;
        }

        public Vector2 CalculateNewPosition(float a_elapsedTime)
        {
            Vector2 newPos = m_pos + m_velocity * a_elapsedTime;

            return newPos;
        }

        public void SetPos(Vector2 a_pos)
        {
            m_pos = a_pos;
        }

        private float DoJump(float a_velY)
        {
            if (m_isJumping)
            {
                if ((!m_wasJumping && m_isOnGround) || m_jumpTime > 0.0f)
                {
                    m_jumpTime += 0.1f;
                }

                if (m_jumpTime > 0.0f && m_jumpTime <= MaxJumpTime)
                {
                    a_velY = -25 * (1.0f - (float)Math.Pow(m_jumpTime / MaxJumpTime, 0.114f));
                }
                else
                {
                    m_jumpTime = 0.0f;
                }
            }
            else
            {
                m_jumpTime = 0.0f;
            }

            m_wasJumping = m_isJumping;

            return a_velY;
        }
    }
}
