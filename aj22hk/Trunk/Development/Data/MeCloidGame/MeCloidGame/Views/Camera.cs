using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MeCloidGame.Views
{
    class Camera
    {
        public Vector2 m_pos;

        public Vector2 TopLeft;

        public Vector2 m_velocity;
        public Vector2 m_movement;

        public int m_zoom = 48;

        private const float MaxMoveSpeed = 5.0f;
        private const float Acceleration = 10.0f;

        public Camera()
        {
            m_pos = Vector2.Zero;
        }

        public bool UpdateCamera(float a_elapsedTime, int a_width, int a_height)
        {
            m_velocity += m_movement * Acceleration * a_elapsedTime;

            if (m_movement == Vector2.Zero)
            {
                m_velocity = Vector2.Zero;
            }

            m_velocity.X = MathHelper.Clamp(m_velocity.X, -MaxMoveSpeed, MaxMoveSpeed);
            m_velocity.Y = MathHelper.Clamp(m_velocity.Y, -MaxMoveSpeed, MaxMoveSpeed);

            m_pos = m_pos + m_velocity * a_elapsedTime;

            // Calculates top left in screen coordinates and therefore
            // must have the position in screen coordinates as well which
            // is why we multiply the model coordinate postition with zoom.
            TopLeft.X = m_pos.X * m_zoom - a_width / 2;
            TopLeft.Y = m_pos.Y * m_zoom - a_height / 2;

            return true;
        }

        public Vector2 Translate(Vector2 a_pos)
        {
            
            return a_pos * m_zoom - TopLeft;
        }
    }
}