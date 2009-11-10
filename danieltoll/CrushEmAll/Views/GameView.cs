using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZombieHoards.Views
{
    class GameView
    {

        public enum PlayState
        {
            NONE,
            BUY_SOLDIER,
            BUY_CIVILIAN,
            MOVE,
            DRAW
        };
        public PlayState m_state = PlayState.NONE;

        private Views.MapView m_mapView;
        private Views.CharacterView m_characterView;
        private Views.Effects m_effects;

        private Views.Core m_core;



        public GameView(Views.Core a_core)
        {
            m_core = a_core;
            m_mapView = new Views.MapView();
            m_characterView = new Views.CharacterView();
            m_effects = new Views.Effects();
        }

        public void Attack(Vector2 a_from, Vector2 a_to, bool a_isZombie)
        {
            
            if (a_isZombie)
            {
                m_core.m_sounds.m_zombieAttack.Play(0.1f, 0.0f, 0.0f);
                m_effects.AddSplat(a_to);
            }
            else
            {
                m_core.m_sounds.m_fireGun.Play(0.1f, 0.0f, 0.0f);
                m_effects.AddShot(a_from, a_to);
            }
        }


        public Vector2 GetLogicalMousePos(int a_scale)
        {
            
            return new Vector2((int)(m_core.m_input.m_mousePos / a_scale).X, (int)(m_core.m_input.m_mousePos / a_scale).Y);
        }

        public void DrawPath(List<Vector2> a_path, int a_scale)
        {
            foreach (Vector2 v in a_path)
            {
                m_characterView.DrawSoldier(m_core, v * a_scale);
            }
        }

        public void Draw(ZombieHoards.Model.Game a_game, float a_elapsedTime, int a_scale, int a_selectedIndex)
        {

            


            int scale = a_scale;

            m_effects.Update(a_elapsedTime, m_core, scale);

            m_mapView.DrawMap(a_game.m_map, m_core, scale);

            for (int i = 0; i < Model.Game.MAX_CIVILIANS; i++)
            {
                m_characterView.DrawCivilian(m_core, a_game.m_civilians[i], a_elapsedTime, i, scale);
            }
            for (int i = 0; i < Model.Game.MAX_ENEMIES; i++)
            {
                m_characterView.DrawEnemy(m_core, a_game.m_enemies[i], a_elapsedTime, i, scale);
            }
            for (int i = 0; i < Model.Game.MAX_SOLDIERS; i++)
            {
                m_characterView.DrawSoldier(m_core, a_game.m_soldiers[i], a_elapsedTime, i, scale);
                if (m_state == PlayState.MOVE && i == a_selectedIndex)
                {
                    m_characterView.DrawSoldier(m_core, a_game.m_soldiers[i].m_pos * scale);
                }
            }
            m_core.DrawText(a_game.m_cash.ToString(), m_core.m_fonts.m_baseFont, new Vector2(16, 16), Color.Red);
            
            m_core.DrawMouse();
            if (m_state == PlayState.BUY_SOLDIER || m_state == PlayState.MOVE)
            {
                Vector2 pos = new Vector2();
                pos.X = (int)(m_core.m_input.m_mousePos.X/16);
                pos.Y = (int)(m_core.m_input.m_mousePos.Y/16);
                pos *= 16;
                m_characterView.DrawSoldier(m_core, pos);
            }
           
        }
    }
}
