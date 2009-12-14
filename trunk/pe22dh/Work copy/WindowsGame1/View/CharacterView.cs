using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CombatLand.View
{
    class CharacterView
    {
        public void DrawPlayer(Core a_core, Model.Character a_character, Camera a_camera)
        {
            DrawCharacter(a_core, a_character, a_camera, new Rectangle(0, 0, 48, 96));
        }
        
        private void DrawCharacter(Core a_core, Model.Character a_character, Camera a_camera, Rectangle a_src)
        {
            float a_layerDepth = 1.0f;

            if (a_character.IsAlive())
            {
                SpriteEffects a_flip;

                if (a_character.m_isFacingRight)
                {
                    a_flip = SpriteEffects.None;
                }
                else
                {
                    a_flip = SpriteEffects.FlipHorizontally;
                }

                Vector2 a_pos = a_camera.Scale(a_character.m_pos);

                Rectangle dest = new Rectangle((int)a_pos.X, (int)a_pos.Y, View.Camera.m_scale, (int)(View.Camera.m_scale * 1.8f));
                a_core.Draw(a_core.m_assets.m_playerTexture, dest, a_src, Color.White, a_flip, a_layerDepth);
            }
        }
    }
}
