using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CombatLand.View
{
    class GameView
    {
        

        private View.Core m_core;
        private View.CharacterView m_charView;
        private View.MapView m_mapView;
        private View.Camera m_camera;
        private View.BulletView m_bulletView;

        public GameView(View.Core a_core)
        {
            m_core = a_core;
            m_charView = new CharacterView();
            m_mapView = new MapView();
            m_camera = a_core.m_camera;
            m_bulletView = new BulletView();
        }
        public void Draw(CombatLand.Model.Game a_game)
        {
            
            m_mapView.DrawMap(a_game.m_map, m_core, m_camera);
            m_charView.DrawPlayer(m_core, a_game.m_player, m_camera);
            m_bulletView.DrawBullets(a_game.m_player, m_core, m_camera);
            m_core.DrawText("HP: " +a_game.m_player.m_hitPoints.ToString(), m_core.m_fonts.m_font, new Vector2(50.0f, 10.0f), Color.Black);
            DrawMouse();
        }
        private void DrawMouse()
        {
            Vector2 a_mousePos = m_core.m_input.m_mousePos; 
            Rectangle src = new Rectangle(0, 0, 40, 40);
            Rectangle dest = new Rectangle(0, 0, 20, 20);
            dest.X = (int)a_mousePos.X - dest.Center.X;
            dest.Y = (int)a_mousePos.Y - dest.Center.Y;

            m_core.Draw(m_core.m_assets.m_crosshair, dest, src, Color.White, SpriteEffects.None, 0.0f);
        }
    }
}
