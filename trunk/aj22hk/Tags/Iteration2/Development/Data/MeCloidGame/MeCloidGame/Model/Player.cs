using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace MeCloidGame.Model
{
    class Player
    {
        // TODO: Add elapsed time everywhere.
        // TODO: Rethink physics for player. Move away from Platformer Starter Kit...
        // TODO: Add weapons.
        public Vector2 m_pos;
        public Vector2 m_velocity;

        public bool m_isOnGround;
        public float m_prevBottom;

        public readonly int WIDTH = 64;
        public readonly int HEIGHT = 130;

        private const float Gravity = 24.0f;
        private const float MaxFallSpeed = 8.0f;

        public bool m_isJumping;
        private bool m_wasJumping;
        private float m_jumpTime;
        private const float MaxJumpTime = 1.0f;

        public Rectangle BoundingRectangle
        {
            get
            {
                int left = (int)Math.Round(m_pos.X - WIDTH / 2);
                int top = (int)Math.Round(m_pos.Y - HEIGHT);

                return new Rectangle(left, top, WIDTH, HEIGHT);
            }
        }

        public Player()
        {
            m_pos = new Vector2(96.0f, 30.0f);
        }

        public bool UpdateVelocity(float a_movement)
        {
            int tiles = 1280 / 64; // Width of a set screen in tiles      (~72px == 1m)
            float speed = tiles / 3.0f; // Speed determined by how long it would take to traverse all tiles
            m_velocity.X = a_movement * speed;

            m_velocity.Y = MathHelper.Clamp(m_velocity.Y + Gravity, 0, MaxFallSpeed);
            m_velocity.Y = DoJump(m_velocity.Y);

            return true;
        }

        public bool UpdatePosition()
        {
            m_pos += m_velocity;
            m_pos = new Vector2((float)Math.Round(m_pos.X), (float)Math.Round(m_pos.Y));

            return true;
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
                    a_velY = -25 * (1.0f - (float)Math.Pow(m_jumpTime / MaxJumpTime, 4.0f));
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
