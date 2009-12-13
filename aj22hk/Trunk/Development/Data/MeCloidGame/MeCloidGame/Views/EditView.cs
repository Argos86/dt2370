using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MeCloidGame.Views
{
    class EditView
    {
        // TODO: Move mouse input and keyboard status to InputHandler
        private Core m_coreView;
        private LevelView m_levelView;

        public EditView(Core a_coreView)
        {
            m_coreView = a_coreView;
            m_levelView = new LevelView();
        }

        public void Draw(Model.Game a_game, float a_elapsedTime, Camera a_camera, Helpers.MouseInput a_mouse)
        {
            m_levelView.DrawLevel(a_game.m_level, m_coreView, a_camera);

            DrawMouse(a_mouse);

            Vector2 logMPos = (a_mouse.Pos + a_camera.TopLeft) / a_camera.m_zoom;
            logMPos.X = (int)logMPos.X;

            m_coreView.DrawText(logMPos.ToString(), m_coreView.Fonts.Georgia, Vector2.Zero, Color.Chartreuse);
            m_coreView.DrawText(a_camera.TopLeft.ToString(), m_coreView.Fonts.Georgia, new Vector2(0, 100), Color.Chartreuse);
            m_coreView.DrawText(a_mouse.Pos.ToString(), m_coreView.Fonts.Georgia, new Vector2(0, 200), Color.Chartreuse);

            Model.Tile.TileType tt = a_game.m_level.GetCollision((int)logMPos.X, (int)logMPos.Y);

            if ((int)logMPos.X >= 0 && (int)logMPos.X < Model.Level.WIDTH && (int)logMPos.Y > 0 && (int)logMPos.Y < Model.Level.HEIGHT)
            {
                m_coreView.DrawText(tt.ToString(), m_coreView.Fonts.Georgia, new Vector2(0.0f, 20.0f), Color.Chartreuse);
            }
            else
            {
                m_coreView.DrawText("Out of bounds", m_coreView.Fonts.Georgia, new Vector2(0.0f, 20.0f), Color.Chartreuse);
            }
        }

        private void DrawMouse(Helpers.MouseInput a_mouse)
        {
            Rectangle src = new Rectangle(64, 0, 32, 32);
            Rectangle dest = new Rectangle(0, 0, 5, 5);
            dest.X = (int)a_mouse.Pos.X - dest.Center.X;
            dest.Y = (int)a_mouse.Pos.Y - dest.Center.Y;

            m_coreView.Draw(m_coreView.Textures.Tiles, dest, src, Color.White);
        }
    }
}
