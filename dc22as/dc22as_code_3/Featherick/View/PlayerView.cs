using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Featherick.View
{
    class PlayerView
    {
        public void DrawPlayer(Core a_core, Vector2 a_pos, Point a_currentFrame, Point a_frameSize, Model.Player a_player, float a_scale)
        {
            if (a_player.IsAlive() == true)
            {
                float rescaledImageSize = a_scale * 2.0f;

                
                Rectangle m_dest = new Rectangle((int)(a_pos.X * a_scale - rescaledImageSize / 4.0f), (int)(a_pos.Y * a_scale), (int)rescaledImageSize, (int)rescaledImageSize);
                Rectangle m_source = new Rectangle(0,0,a_core.m_assets.m_playerTexture.Width,a_core.m_assets.m_playerTexture.Height);

                /*
                SpriteEffects m_flip = SpriteEffects.None;
                if (a_player.m_velocity.X < 0)
                {
                    m_flip = SpriteEffects.FlipHorizontally; 
                }
                else if (a_player.m_velocity.X > 0)
                {
                    m_flip = SpriteEffects.None;
                }*/  

                a_core.Draw(a_core.m_assets.m_playerTexture, m_dest, m_source, Color.White);                
            }
            
        }

        public void PlayPlayerSound(Core a_core, Model.Player a_player)
        {
            if (a_player.IsJumping())
            {
                a_core.m_sounds.m_playerJump.Play();
            }
        }

    }
}
