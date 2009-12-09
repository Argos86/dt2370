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

        public void DrawHP(Core a_core, Vector2 a_pos, int a_scale, float a_currentHP, int a_maxHP)
        {
            if (a_currentHP < a_maxHP)
            {
                if (a_currentHP > 0)
                {
                int width = (int)(a_scale * a_currentHP / (float)a_maxHP);
                Rectangle dest = new Rectangle((int)(a_pos.X * a_scale) - a_scale / 2, (int)(a_pos.Y * a_scale) - a_scale, a_scale, 6);
                a_core.Draw(a_core.m_assets.m_blank, dest, new Rectangle(0, 0, 22, 8), Color.White);
                dest = new Rectangle((int)(a_pos.X * a_scale) - a_scale / 2, (int)(a_pos.Y * a_scale) - a_scale, width, 6);
                a_core.Draw(a_core.m_assets.m_blank, dest, new Rectangle(1, 1, 20, 6), Color.Red);
                }
            }
        }

        public void DrawTower(Core a_core, Vector2 a_pos, Model.Tower.Type a_type, int a_scale)
        {

            float rel = 288.0f / 128.0f;
            float rel2 = 48.0f / 128.0f;

            Vector2 pos = a_pos * a_scale - new Vector2(a_scale / 2, a_scale * rel - rel2 * a_scale);
            Rectangle dest = new Rectangle((int)pos.X, (int)pos.Y, a_scale, (int)(a_scale * rel));
            Color color = Color.White;
            Byte c = 128;
            switch (a_type)
            {
                case Model.Tower.Type.Normal: color = Color.White; break;
                case Model.Tower.Type.Earth: color = Color.Brown; break;
                case Model.Tower.Type.Fire: color = Color.Red; break;
                case Model.Tower.Type.Water: color = new Color(c, c, 255); break;
                case Model.Tower.Type.Wind: color = Color.Green; break;
                case Model.Tower.Type.Undead: color = Color.Gray; break;
            }
            a_core.Draw(a_core.m_assets.m_tower, dest, new Rectangle(256, 0, 128, 288), color);
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
                Color color = Color.White;

                switch (a_character.CurrentType)
                {
                    case Model.Enemy.Type.Normal: color = Color.White; break;
                    case Model.Enemy.Type.Earth : color = Color.Brown; break;
                    case Model.Enemy.Type.Fire : color = Color.Red; break;
                    case Model.Enemy.Type.Water : color = Color.Blue; break;
                    case Model.Enemy.Type.Wind : color = Color.Green; break;
                    case Model.Enemy.Type.Undead: color = Color.Gray; break;
                }

                a_core.Draw(a_core.m_assets.m_texture, dest, a_src, color);
            }
        }
    }
}
