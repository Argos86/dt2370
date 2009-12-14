using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace MeCloidGame.Views
{
    class Particle
    {
        public Vector2 m_pos;
        public Vector2 m_speed;
        public float m_delay;

        public const float m_size = 0.1f;

        static Random rnd = new Random();


        public Particle()
        {
            m_pos = new Vector2(2, 43);

            m_speed = new Vector2((float)rnd.NextDouble(), (float)rnd.NextDouble());
            m_speed -= new Vector2(0.5f);
            m_speed.Normalize();
            m_speed *= 5;

            m_delay = (float)rnd.NextDouble();
        }

        public void Update(float a_elapsedTime)
        {
            if (m_delay < 0)
            {
                m_pos += m_speed * a_elapsedTime;
            }
            else
            {
                m_delay -= 0.01f;
            }
        }
    }
}
