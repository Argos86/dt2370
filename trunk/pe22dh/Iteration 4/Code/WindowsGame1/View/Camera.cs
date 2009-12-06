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
        public Vector2 m_pos;
      

        public Camera(Vector2 a_pos)
        {
            m_pos = a_pos;
          
        }

        public void UpdateCamera(CombatLand.Model.Game a_game, float a_elapsedTime, int a_scale) 
        {
            float m_playerPos = a_game.m_player.m_pos.X * a_scale;
            
            if (m_playerPos > (800.0f + m_pos.X) && a_game.m_player.m_movement > 0) 
            {
                m_pos.X += ((a_game.m_player.m_velocity.X * (float)a_scale)* a_elapsedTime);
            }
            if (m_playerPos < (200.0f + m_pos.X) && a_game.m_player.m_movement < 0)
            {
                m_pos.X += ((a_game.m_player.m_velocity.X * (float)a_scale) * a_elapsedTime);
            }
        }
    }
}
