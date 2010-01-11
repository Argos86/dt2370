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
            BUY_TOWER,
            UPGRADE_TOWER
        };

        public PlayState m_state = PlayState.NONE;



        public  View.CharacterView m_characterView;
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

        public void Attack(Vector2 a_from, Vector2 a_to, Model.Tower.Type a_type)
        {
                //m_core.m_sounds.m_fireGun.Play(0.1f, 0.0f, 0.0f);
                m_effects.AddShot(a_from, a_to, a_type);
        }


        public Vector2 GetLogicalMousePos(int a_scale)
        {

            return new Vector2((int)(m_core.m_input.m_mousePos / a_scale).X, (int)(m_core.m_input.m_mousePos / a_scale).Y);
        }

        public void DrawUpgrade(Model.Tower a_tower)
        {

        }


        public void KilledEnemy(int a_index)
        {
            m_characterView.m_animationTimer[a_index] = 0.0f;

        }

        public void DrawMax(string a_text, Vector2 a_pos)
        {
            m_core.DrawText(a_text + " Maxed".ToString(), m_core.m_fonts.m_baseFont, a_pos, Color.Red);
        }

        public void DrawLost()
        {
            m_core.DrawText("You utterly utterly failed, you suck!", m_core.m_fonts.m_baseFont, new Vector2(300, 300), Color.Red);
        }

        public void DrawWon()
        {
            m_core.DrawText("You have completed the game at this difficulty, congratulations!", m_core.m_fonts.m_baseFont, new Vector2(300, 300), Color.Red);
        }

        public void DrawWelcome()
        {
            m_core.DrawText("Welcome to the Tower Defend of DOOOOM!", m_core.m_fonts.m_baseFont, new Vector2(300, 250), Color.Red);
            m_core.DrawText("Choose your difficulty:", m_core.m_fonts.m_baseFont, new Vector2(350, 300), Color.Red);
        }
        public void Draw(Tower_Defense.Model.Game a_game, float a_elapsedTime, int a_scale, Model.Tower a_selectedTower, Model.Tower.Type a_type)
        {

            int scale = a_scale;
            m_level.DrawLevel(a_game.m_map, m_core, scale);

            if (m_state == PlayState.UPGRADE_TOWER)
            {

                DrawRange(a_scale, a_selectedTower);
            }

            else if (m_state == PlayState.BUY_TOWER)
            {

                 Vector2 pos = new Vector2();
                pos.X = (int)(m_core.m_input.m_mousePos.X / scale);
                pos.Y = (int)(m_core.m_input.m_mousePos.Y / scale);

                Model.Tower t = new Tower_Defense.Model.Tower(pos, a_type);

                DrawRange(a_scale, t);
            }

            

           

            for (int i = 0; i < Model.Game.MAX_ENEMIES; i++)
            {
                m_characterView.DrawHP(m_core, a_game.m_enemies[i].m_pos, a_scale, a_game.m_enemies[i].m_hitPoints, a_game.m_enemies[i].GetMaxHP()); 
            }

            DrawAllEnemies(a_game, a_scale, a_elapsedTime);

            DrawAllTowers(a_game, scale, a_type);
            m_level.DrawMenu(m_core);
            m_core.DrawText("Money " + a_game.m_cash.ToString(), m_core.m_fonts.m_baseFont, new Vector2(970, 10), Color.Red);
            if (Settings.Debugging == true)
            {
                //int civiliansAlive = 0;
                int enemiesAlive = 0;
                int currentWave = 0;
                string enemyType = "Waiting";



                int num = 0;
                foreach (Model.Enemy c in a_game.m_enemies)
                {
                    if (c.IsAlive())
                    {
                        enemyType = c.CurrentType.ToString();
                        enemiesAlive++;

                        //m_core.DrawText("HP : " + c.m_hitPoints.ToString(), m_core.m_fonts.m_baseFont, new Vector2(300, 100 + num * 20), Color.White);
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
                m_effects.Update(a_elapsedTime, m_core, scale);
                m_core.DrawText("Hitpoints: " + a_game.hitpoints.ToString(), m_core.m_fonts.m_baseFont, new Vector2(30, 760), Color.Red);
                m_core.DrawText("Enemies Alive: " + enemiesAlive.ToString(), m_core.m_fonts.m_baseFont, new Vector2(180, 760), Color.Red);
                m_core.DrawText("Enemy Type: " + enemyType, m_core.m_fonts.m_baseFont, new Vector2(600, 760), Color.Red);
                m_core.DrawText("Current Wave: " + currentWave, m_core.m_fonts.m_baseFont, new Vector2(390, 760), Color.Red);
                //m_core.DrawParticle();
            }



        }

        private void DrawRange(int a_scale, Model.Tower a_selectedTower)
        {
            m_core.End();

            m_core.Begin(SpriteBlendMode.Additive);

            float range = a_selectedTower.GetRange(a_selectedTower.CurrentRangeLevel);
            Rectangle dest = new Rectangle((int)((a_selectedTower.m_pos.X - range) * a_scale),
                (int)((a_selectedTower.m_pos.Y - range) * a_scale), (int)((float)a_scale * 2.0f * range), (int)((float)a_scale * 2.0f * range));
            Rectangle src = new Rectangle(0, 0, 255, 255);
            m_core.Draw(m_core.m_assets.m_circle, dest, src, new Color(32, 32, 32));

            m_core.End();
            m_core.Begin(SpriteBlendMode.AlphaBlend);
        }



        private void DrawAllTowers(Tower_Defense.Model.Game a_game, int scale, Model.Tower.Type a_type)
        {
            List<Model.Tower> towerList = new List<Tower_Defense.Model.Tower>();
            for (int i = 0; i < Model.Game.MAX_TOWERS; i++)
            {
                if (a_game.m_towers[i] != null)
                {
                    towerList.Add(a_game.m_towers[i]);
                }
            }

            if (m_state == PlayState.BUY_TOWER)
            {

                Vector2 pos = new Vector2();
                pos.X = (int)(m_core.m_input.m_mousePos.X / scale);
                pos.Y = (int)(m_core.m_input.m_mousePos.Y / scale);

                Model.Tower t = new Tower_Defense.Model.Tower(pos, a_type);
                towerList.Add(t);
                //m_characterView.DrawTower(m_core, pos, a_type, scale);

            }

            towerList.Sort();

            foreach (Model.Tower t in towerList)
            {
                m_characterView.DrawTower(m_core, t.m_pos, t.CurrentType, scale);
            }
        }

        private void DrawAllEnemies(Tower_Defense.Model.Game a_game, int scale, float a_elapsedTime)
        {
            List<Model.Enemy> enemyList = new List<Tower_Defense.Model.Enemy>();
            for (int i = 0; i < Model.Game.MAX_ENEMIES; i++)
            {
                if (a_game.m_enemies[i] != null)
                {
                    a_game.m_enemies[i].m_index = i;
                    enemyList.Add(a_game.m_enemies[i]);
                }
            }



            enemyList.Sort();

            foreach (Model.Enemy t in enemyList)
            {
                m_characterView.DrawEnemy(m_core, t, a_elapsedTime, t.m_index, scale);
            }
        }
    }
}
