using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CombatLand.Model
{
    class Bullet
    {
        public Vector2 m_pos;
        public Vector2 m_speed;

        public Bullet(Vector2 a_pos, Vector2 a_speed)
        {
            m_pos = a_pos;
            m_speed = a_speed;
        }
    }
}
