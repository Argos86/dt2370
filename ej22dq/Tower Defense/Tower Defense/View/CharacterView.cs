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

            // nya pos - gamla för att få riktning, stega igenom en sprite för animationer. olika per typ, riktning på gubben, timer för animationen.

            DrawCharacter(a_core, a_character, a_scale, a_id, a_elapsedTime);

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

       
        private void DrawCharacter(Core a_core, Model.Enemy a_character, int a_scale, int a_enemyIndex, float a_elapsedTime)
        {

            Texture2D sprite = a_core.m_assets.m_knight;
            switch (a_character.CurrentType)
            {
                case Model.Enemy.Type.Normal: sprite = a_core.m_assets.m_knight; break;
                case Model.Enemy.Type.Earth: sprite = a_core.m_assets.m_knight; break;
                case Model.Enemy.Type.Fire: sprite = a_core.m_assets.m_knight; break;
                case Model.Enemy.Type.Water: sprite = a_core.m_assets.m_knight; break;
                case Model.Enemy.Type.Wind: sprite = a_core.m_assets.m_knight; break;
                case Model.Enemy.Type.Undead: sprite = a_core.m_assets.m_knight; break;
            }

            int size = 128;
            switch (a_character.CurrentType)
            {
                case Model.Enemy.Type.Normal: size = 128; break;
                case Model.Enemy.Type.Earth: size = 128; break;
                case Model.Enemy.Type.Fire: size = 128; break;
                case Model.Enemy.Type.Water: size = 128; break;
                case Model.Enemy.Type.Wind: size = 128; break;
                case Model.Enemy.Type.Undead: size = 128; break;
            }
            Vector2 pos = a_character.m_pos;//time * a_character.m_pos + (1.0f - time) * a_character.m_oldPos;
            pos = pos * a_scale - new Vector2(a_scale , a_scale );
            Rectangle dest = new Rectangle((int)pos.X, (int)pos.Y, a_scale * 2, a_scale * 2);

            if (a_character.IsAlive())
            {
                //float time = 2.0f * (0.5f - a_character.m_timer);

                
                Color color = Color.White;
                

                Vector2 dir = a_character.m_pos - a_character.m_oldPos;
                int ycoord = 0;
                if (dir.X*dir.X > dir.Y*dir.Y)
                {
                    if(dir.X>0)
                    {
                        //höger
                        ycoord = 1 * size;
                    }
                    else 
                    {
                        ycoord = 4 * size;
                    }
                }
                else 
                {
                    if (dir.Y>0)
                    {
                        //neråt
                        ycoord = 3 * size;
                    }
                    else 
                    {
                        //uppåt
                        ycoord = 2 * size;
                    }
                }
                float framenumber = 12.0f;
                m_animationTimer[a_enemyIndex] += a_elapsedTime;


                int yFrame = ((int)(m_animationTimer[a_enemyIndex] * framenumber + (float)a_enemyIndex * 1.5f)) % (int)framenumber;

                
                Rectangle source = new Rectangle(yFrame * size, ycoord, size, size);

                a_core.Draw(sprite, dest, source, color);
            }
            else
            {

                m_animationTimer[a_enemyIndex] += a_elapsedTime;

                if (m_animationTimer[a_enemyIndex] < 1.0f)
                {
                    
                    



                    float framedeath = 13.0f;
                    switch (a_character.CurrentType)
                    {
                        case Model.Enemy.Type.Normal: framedeath = 13.0f; break;
                        case Model.Enemy.Type.Earth: framedeath = 13.0f; break;
                        case Model.Enemy.Type.Fire: framedeath = 13.0f; break;
                        case Model.Enemy.Type.Water: framedeath = 13.0f; break;
                        case Model.Enemy.Type.Wind: framedeath = 13.0f; break;
                        case Model.Enemy.Type.Undead: framedeath = 13.0f; break;
                    }
  

                    int xFrame = (int)(m_animationTimer[a_enemyIndex] * framedeath);


                    Rectangle source = new Rectangle(xFrame * size, 0, size, size);
                    //draw dying knight
                    
                    a_core.Draw(sprite, dest, source, Color.White);
                }
            }
        }

        public float[] m_animationTimer = new float[Model.Game.MAX_ENEMIES];

        public CharacterView()
        {
            for (int i = 0; i < Model.Game.MAX_ENEMIES; i++)
            {
                m_animationTimer[i] = 10.0f;
            }
        }



    }
}
