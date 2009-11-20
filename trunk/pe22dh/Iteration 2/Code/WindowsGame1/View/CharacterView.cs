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
        public void DrawPlayer(Core a_core, Model.Character a_character, int a_scale)
        {
            DrawCharacter(a_core, a_character, a_scale, new Rectangle(0, 0, 34, 34));
        }
        
        private void DrawCharacter(Core a_core, Model.Character a_character, int a_scale, Rectangle a_src)
        {
            if (a_character.IsAlive())
            {

                Vector2 pos = a_character.m_pos;
                pos = pos * a_scale - new Vector2(a_scale / 2, a_scale / 2);
                Rectangle dest = new Rectangle((int)pos.X, (int)pos.Y, a_scale, a_scale);
                a_core.Draw(a_core.m_assets.m_texture, dest, a_src, Color.White);
            }
        }
    }
}
