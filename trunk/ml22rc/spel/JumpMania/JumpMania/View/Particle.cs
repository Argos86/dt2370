using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace JumpMania.View
{
    class Particle
    {
        public Vector2 m_pos;
        public Vector2 m_speed;
        public float m_rot;
        public float m_rotspeed;
        public float m_timetolive;


        public Particle(ref Random a_rand, float a_pos)
        {
            int x = a_rand.Next(18);
            int y = a_rand.Next(900);
            m_pos = new Vector2(x, a_pos);
            x = a_rand.Next(-3,3);
            y = a_rand.Next(-3, 0);
            m_speed = new Vector2(x, y);
            m_speed *= (float)a_rand.NextDouble() * 4.0f + 1.0f;

            m_rot = (float)(a_rand.NextDouble() * 2.0 * Math.PI);
            m_rotspeed = (float)(a_rand.NextDouble() * 2.0 * Math.PI) * 10;

            m_timetolive = (float)a_rand.NextDouble();
        }

        public bool Update(float a_elapsedtime)
        {
            m_speed += new Vector2(0, 9.82f) * a_elapsedtime * 1.0f;
            m_pos += m_speed * a_elapsedtime;
            m_rot += a_elapsedtime * m_rotspeed;

            m_timetolive -= a_elapsedtime;

            if (m_timetolive < 0)
            {
                return false;
            }

            return true;
        }
    }
}
