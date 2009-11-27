using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PacmanClone.Model
{
    class Character
    {
        public Vector2 m_movement;
        public Vector2 m_velocity;
        public float m_speed;
        public float m_timer;
        public int m_hitPoints;
        public int m_xPos;
        public int m_yPos;
        

        public bool IsAlive()
        {
            return m_hitPoints > 0;
        }

        public Character(float a_speed)
        {
            m_hitPoints = 4;
            m_xPos = 9;
            m_yPos = 6;
            m_timer = 0.0f;
            m_speed = a_speed;
            m_movement = new Vector2(0, 0);
            m_velocity = new Vector2(0, 0);
        }

        public void SetMovement(Vector2 a_movement)
        {
            m_movement = a_movement;
        }
    }
}
