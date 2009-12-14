using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CombatLand.View
{
    class Camera
    {
        public const int m_scale = 48;

        public Vector2 m_pos;
        
        public Camera(Vector2 a_pos)
        {
            m_pos = a_pos;
          
        }

        public void ResetCamera()
        {
            m_pos = new Vector2(0);
        }

        public Vector2 Scale(Vector2 a_pos)
        {
            a_pos = (a_pos * m_scale) - m_pos;
            
            return a_pos;
        }

        public void UpdateCamera(CombatLand.Model.Game a_game, float a_elapsedTime) 
        {
            float a_width = Model.Map.WIDTH * m_scale;
            float m_playerPos = a_game.m_player.m_pos.X * m_scale;
            
            if (m_playerPos > (600.0f + m_pos.X) && a_game.m_player.m_movement > 0) 
            {
                if (m_playerPos < (a_width - 680.0f))
                {
                    m_pos.X += ((a_game.m_player.m_velocity.X * (float)m_scale) * a_elapsedTime);
                }
            }
            if (m_playerPos < (400.0f + m_pos.X) && a_game.m_player.m_movement < 0)
            {
                m_pos.X += ((a_game.m_player.m_velocity.X * (float)m_scale) * a_elapsedTime);
            }
            if (m_pos.X <= 0.0f)
            {
                m_pos.X = 0.0f;
            }
            
        }
    }
}
