using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense.View
{
    class Particle
    {
        public Vector2 m_pos;
        public Vector2 m_speed;
        public float m_rot;
        public float m_rotspeed;


        public Particle(ref Random a_rand)
        {
            int x = a_rand.Next(1200);
            int y = a_rand.Next(800);
            m_pos = new Vector2(600,400);
            x = a_rand.Next(360) - 180;
            y = a_rand.Next(360) - 180;
            m_speed = new Vector2(x, y);
            m_speed.Normalize();
            m_speed *= (float)a_rand.NextDouble() * 100.0f+10.0f;
            m_rot = (float)(a_rand.NextDouble() * 2.0 * Math.PI);
            m_rotspeed = (float)(a_rand.NextDouble() * 2.0 * Math.PI) * 10;
        }

        public void Update(float a_elapsedtime)
        {

            m_speed += new Vector2(0, 9.82f) * a_elapsedtime * 10.0f;
            m_pos += m_speed * a_elapsedtime;
            m_rot += a_elapsedtime * m_rotspeed;
        }
    }
}
