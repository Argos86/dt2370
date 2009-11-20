using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense.View
{
    class CharacterView
    {

        public void DrawEnemy(Core a_core, Model.Enemy a_character, float a_elapsedTime, int a_id, int a_scale)
        {
            DrawCharacter(a_core, a_character, a_scale, new Rectangle(51, 68, 20, 20));
        }
        

        public void DrawTower(Core a_core, Vector2 a_pos)
        {
            int size = 16;
            Vector2 pos = a_pos - new Vector2(size / 2, size / 2);
            Rectangle dest = new Rectangle((int)pos.X, (int)pos.Y, size, size);
            a_core.Draw(a_core.m_assets.m_texture, dest, new Rectangle(4, 4, 40, 40), Color.Gray);
        }

        public void DrawCivilian(Core a_core, Model.Enemy a_character, float a_elapsedTime, int a_id, int a_scale)
        {
            DrawCharacter(a_core, a_character, a_scale, new Rectangle(73, 103, 22, 27));
        }

        private void DrawCharacter(Core a_core, Model.Enemy a_character, int a_scale, Rectangle a_src)
        {
            if (a_character.IsAlive())
            {
                //float time = 2.0f * (0.5f - a_character.m_timer);


                Vector2 pos = a_character.m_pos;//time * a_character.m_pos + (1.0f - time) * a_character.m_oldPos;


                pos = pos * a_scale - new Vector2(a_scale / 2, a_scale / 2);


                Rectangle dest = new Rectangle((int)pos.X, (int)pos.Y, a_scale, a_scale);
                a_core.Draw(a_core.m_assets.m_texture, dest, a_src, Color.White);
            }
        }
    }
}
