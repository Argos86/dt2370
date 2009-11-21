using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PacmanClone.Model
{
    class Character
    {
        public int m_hitPoints;

        public float m_movement;
        public float m_speed;

        public Vector2 m_pos;
        public Vector2 m_velocity;

        public bool IsAlive()
        {
            return m_hitPoints > 0;
        }

        public Character(float a_speed)
        {
            m_hitPoints = 4;
            m_pos = new Vector2(9.0f, 6.5f);
            m_speed = a_speed;
            m_movement = 0.0f;
            m_velocity = new Vector2(0, 0);
        }

        public void SetMovement(float a_movement)
        {
            m_movement = a_movement;
        }

        public void DoMovement(float a_elapsedTime)
        {
            Vector2 oldPos;
            Vector2 newpos;
            oldPos = m_pos;

            m_velocity.X = (m_movement * m_speed);
            newpos.X = oldPos.X + (m_velocity.X * a_elapsedTime);

            m_velocity.Y = (m_movement * m_speed);
            newpos.Y = oldPos.Y + (m_velocity.Y * a_elapsedTime);
        }
    }
}
