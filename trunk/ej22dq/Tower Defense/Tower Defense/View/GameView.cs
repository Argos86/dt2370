using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense.View
{
    class GameView
    {
        public enum PlayState
        {
            NONE,
            BUY_TOWER
        };

        public PlayState m_state = PlayState.NONE;


        private View.CharacterView m_characterView;
        private View.Effects m_effects;
        private View.LevelView m_level;
        private View.Core m_core;

        public GameView(View.Core a_core)
        {
            m_core = a_core;

            m_characterView = new View.CharacterView();
            m_effects = new View.Effects();
            m_level = new View.LevelView();
        }

        public void Attack(Vector2 a_from, Vector2 a_to)
        {
                //m_core.m_sounds.m_fireGun.Play(0.1f, 0.0f, 0.0f);
                m_effects.AddShot(a_from, a_to);
        }


        public Vector2 GetLogicalMousePos(int a_scale)
        {

            return new Vector2((int)(m_core.m_input.m_mousePos / a_scale).X, (int)(m_core.m_input.m_mousePos / a_scale).Y);
        }

        /*public void DrawPath(List<Vector2> a_path, int a_scale)
        {
            foreach (Vector2 v in a_path)
            {
                m_characterView.DrawTower(m_core, v * a_scale);
            }
        }*/

        public void Draw(Tower_Defense.Model.Game a_game, float a_elapsedTime, int a_scale, int a_selectedIndex)
        {

            int scale = a_scale;
            m_level.DrawLevel(m_core);
            m_effects.Update(a_elapsedTime, m_core, scale);

           

            for (int i = 0; i < Model.Game.MAX_ENEMIES; i++)
            {
                m_characterView.DrawEnemy(m_core, a_game.m_enemies[i], a_elapsedTime, i, scale);
            }
            for (int i = 0; i < Model.Game.MAX_TOWERS; i++)
            {
                if (a_game.m_towers[i] != null)
                {
                    m_characterView.DrawTower(m_core, a_game.m_towers[i].m_pos * scale/*, a_game.m_towers[i].CurrentType*/);
                }

                /*if (m_state == PlayState.MOVE && i == a_selectedIndex)
                {
                    m_characterView.DrawSoldier(m_core, a_game.m_soldiers[i].m_pos * scale);
                }*/
            }

            m_core.DrawText(a_game.m_cash.ToString(), m_core.m_fonts.m_baseFont, new Vector2(16, 16), Color.Red);

            if (Settings.Debugging == true)
            {
                //int civiliansAlive = 0;
                int enemiesAlive = 0;
                int currentWave = 0;
                string enemyType = "Waiting For Next Wave";



                int num = 0;
                foreach (Model.Enemy c in a_game.m_enemies)
                {
                    if (c.IsAlive())
                    {
                        enemyType = c.CurrentType.ToString();
                        enemiesAlive++;

                        m_core.DrawText("HP : " + c.m_hitPoints.ToString(), m_core.m_fonts.m_baseFont, new Vector2(300, 100 + num * 20), Color.White);
                        num++;
                    }
                }

                foreach (Model.Wave c in a_game.m_waves)
                {
                    if (c.m_isActive == false)
                    {
                        currentWave++;
                    }
                }
                
                m_core.DrawText("Hitpoints: " + a_game.hitpoints.ToString(), m_core.m_fonts.m_baseFont, new Vector2(100, 15), Color.Red);
                m_core.DrawText("Enemies Alive: " + enemiesAlive.ToString(), m_core.m_fonts.m_baseFont, new Vector2(100, 40), Color.Red);
                m_core.DrawText("Enemy Type: " + enemyType, m_core.m_fonts.m_baseFont, new Vector2(100, 65), Color.Red);
                m_core.DrawText("Current Wave: " + currentWave, m_core.m_fonts.m_baseFont, new Vector2(300, 15), Color.Red);
            }

            if (m_state == PlayState.BUY_TOWER)
            {
                foreach (Model.Tower c in a_game.m_towers)
                {
                    Vector2 pos = new Vector2();
                    pos.X = (int)(m_core.m_input.m_mousePos.X / 16);
                    pos.Y = (int)(m_core.m_input.m_mousePos.Y / 16);
                    pos *= 16;
                    m_characterView.DrawTower(m_core, pos/*, c.CurrentType*/);
                }
            }

        }

    }
}
