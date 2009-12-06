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
        public void DrawPlayer(Core a_core, Model.Character a_character, Camera a_camera, int a_scale)
        {
            DrawCharacter(a_core, a_character, a_camera, a_scale, new Rectangle(0, 0, 48, 96));
        }
        
        private void DrawCharacter(Core a_core, Model.Character a_character, Camera a_camera, int a_scale, Rectangle a_src)
        {
            if (a_character.IsAlive())
            {

                Vector2 pos = a_character.m_pos;
                pos = (pos * a_scale) - a_camera.m_pos;
                Rectangle dest = new Rectangle((int)pos.X, (int)pos.Y, a_scale, (int)(a_scale*1.8f));
                a_core.Draw(a_core.m_assets.m_playerTexture, dest, a_src, Color.White);
            }
        }
    }
}
