using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace Featherick.View
{
    class Camera
    {
        public float m_scale = 48.0f;        
        public Vector2 m_topLeft = new Vector2(0, 0);

        public void Update(Vector2 a_pos, float a_elapsedTime, int a_width, int a_height)
        {
            m_topLeft.X = a_pos.X * m_scale - a_width / 2;
            if (m_topLeft.X < 0)
            {
                m_topLeft.X = 0;
            }
            if (m_topLeft.X > Model.Level.WIDTH * m_scale - a_width)
            {
                m_topLeft.X = Model.Level.WIDTH * m_scale - a_width;
            }

            m_topLeft.Y = a_pos.Y * m_scale - a_height / 5;
            if (m_topLeft.Y > Model.Level.HEIGHT * m_scale - a_height)
            {
                m_topLeft.Y = Model.Level.HEIGHT * m_scale - a_height;
            }
        }

    }
}
