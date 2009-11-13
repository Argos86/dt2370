using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PacmanClone.Model
{
    class Character
    {
        public int m_hitPoints = 0;
        public Vector2 m_pos;
        public Vector2 m_oldPos;
        public float m_timer = 0.0f;

        public bool IsAlive()
        {
            return m_hitPoints > 0;
        }

        public Character()
        {
            m_hitPoints = 0;
            m_pos = new Vector2(0, 0);
            m_oldPos = new Vector2(0, 0);
            m_timer = 0.0f;
        }

        public bool Update(float a_elapsedTime)
        {
            m_timer -= a_elapsedTime;
            if (IsAlive() == false)
            {
                return false;
            }
            if (m_timer > 0)
            {
                return false;
            }

            return true;
        }

    }
}
