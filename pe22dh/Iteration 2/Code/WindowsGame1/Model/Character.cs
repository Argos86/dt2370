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
        public const float GRAVITY = 100.0f;
        
        public int m_hitPoints;

        public float m_movement;
        public float m_speed;

        public bool m_isJumping;
        public bool m_isOnGround;

        public Vector2 m_velocity;
        public Vector2 m_pos;

        public bool IsAlive()
        {
            return m_hitPoints > 0;
        }

        public Character(float a_speed)
        {
            m_hitPoints = 10;
            m_pos = new Vector2(10.1f, 10.5f);
            m_speed = a_speed;
            m_movement = 0.0f;
            m_velocity = new Vector2(0, 0);
        }
        public void SetMovement(float a_movement, bool a_isJumping)
        {
            m_movement = a_movement;
            m_isJumping = a_isJumping;
        }
        public void DoMovement(float a_elapsedTime)
        {
            Vector2 oldPos;
            Vector2 newPos;
            oldPos = m_pos;
           
            m_velocity.X = (m_movement * m_speed);
            newPos.X = oldPos.X + (m_velocity.X * a_elapsedTime);
            if (newPos.X > 0)
            {
                m_pos.X = newPos.X;
            }
            
        }


    }
}
