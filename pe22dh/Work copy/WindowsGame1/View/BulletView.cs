using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CombatLand.View
{
    class BulletView
    {
        public void DrawBullets(Model.Character a_player, Core a_core, Camera a_camera)
        {

            float a_layerDepth = 0.0f;
            Rectangle src = new Rectangle(0, 0, 3, 3);
            for (int x = 0; x < a_player.m_bulletIndex; x++)
            {
                Vector2 a_pos = a_camera.Scale(a_player.m_bullets[x].m_pos);
                
                a_core.Draw(a_core.m_assets.m_tileTexture, new Rectangle((int)(a_pos.X),
                (int)(a_pos.Y), 3, 3), src, Color.Black, SpriteEffects.None, a_layerDepth);
                
            }
        }
    }
}
