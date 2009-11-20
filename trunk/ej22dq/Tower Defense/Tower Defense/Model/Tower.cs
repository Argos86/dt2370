using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense.Model
{
    class Tower
    {
        public Vector2 m_pos = new Vector2();
        public float m_timer = 0.0f;

        public Tower(Vector2 a_pos)
        {
            m_pos = a_pos;
        }

        public bool Update(float a_elapsedTime)
        {
            m_timer -= a_elapsedTime;
             
            if (m_timer > 0)
            {
                return false;
            }

            return true;
        }
    }
}
