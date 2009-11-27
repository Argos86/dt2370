using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PacmanClone.Views
{
    class CharacterView
    {
        public void DrawPlayer(Core a_core, Model.Character a_character, int a_scale)
        {
            DrawCharacter(a_core, a_character, a_scale, new Rectangle(0, 0, 32, 32));
        }

        private void DrawCharacter(Core a_core, Model.Character a_character, int a_scale, Rectangle a_src)
        {
            if (a_character.IsAlive())
            {
                int xPos = a_character.m_xPos;
                int yPos = a_character.m_yPos;
                Rectangle dest = new Rectangle(xPos * a_scale, yPos * a_scale, a_scale, a_scale);
                a_core.Draw(a_core.m_assets.m_sprites, dest, a_src, Color.White);
            }
        }
    }
}
