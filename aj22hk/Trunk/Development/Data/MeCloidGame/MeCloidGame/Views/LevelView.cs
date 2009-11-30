using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MeCloidGame.Views
{
    class LevelView
    {
        public void DrawLevel(Model.Level a_level, Core a_coreView, Model.Camera a_camera)
        {
            for (int x = 0; x < Model.Level.WIDTH; ++x)
            {
                for (int y = 0; y < Model.Level.HEIGHT; ++y)
                {
                    Vector2 pos = a_camera.Translate(new Vector2(x, y)) * a_camera.m_zoom;
                    Rectangle dest = new Rectangle((int)pos.X, (int)pos.Y, a_camera.m_zoom, a_camera.m_zoom);
                    Rectangle src;
                    switch (a_level.Tiles[x, y].Type)
                    {
                        case Model.Tile.TileType.Solid:
                            if (x % 2 == 0)
                            {
                                src = new Rectangle(64 * 0, 48 * 0, 64, 48);
                            }
                            else
                            {
                                src = new Rectangle(64 * 2, 48 * 0, 64, 48);
                            }
                            a_coreView.Draw(a_coreView.Textures.Tiles, dest, src, Color.White);
                            break;
                        case Model.Tile.TileType.Destroyable:
                            src = new Rectangle(64 * 1, 48 * 0, 64, 48);
                            a_coreView.Draw(a_coreView.Textures.Tiles, dest, src, Color.White);
                            break;
                    }
                }
            }

            if (Helpers.Settings.Debug)
            {
                DrawGrid(a_level, a_coreView, a_camera);
            }
        }

        private void DrawGrid(Model.Level a_level, Core a_coreView, Model.Camera a_camera)
        {
            // Grid lines
            Helpers.LineBatch gridLines = new Helpers.LineBatch(a_coreView.m_device, 0.4f);
            gridLines.Begin();

            // Vertical lines
            for (int x = 0; x <= a_level.Width; ++x)
            {
                gridLines.Batch(new Vector2(x * a_camera.m_zoom - a_camera.m_pos.X * a_camera.m_zoom, 0 - a_camera.m_pos.Y * a_camera.m_zoom), new Vector2(x * a_camera.m_zoom - a_camera.m_pos.X * a_camera.m_zoom, a_level.Height * a_camera.m_zoom - a_camera.m_pos.Y * a_camera.m_zoom), Color.Red, 1.0f);
            }

            // Horizontal lines
            for (int y = 0; y <= a_level.Height; ++y)
            {
                gridLines.Batch(new Vector2(0 - a_camera.m_pos.X * a_camera.m_zoom, y * a_camera.m_zoom - a_camera.m_pos.Y * a_camera.m_zoom), new Vector2(a_level.Width * a_camera.m_zoom - a_camera.m_pos.X * a_camera.m_zoom, y * a_camera.m_zoom - a_camera.m_pos.Y * a_camera.m_zoom), Color.Red, 1.0f);
            }

            gridLines.End();
            /*
            // Grid centers
            Helpers.PointBatch gridCenters = new Helpers.PointBatch(a_coreView.m_device, 0.8f, 3);
            gridCenters.Begin();

            for (int x = 0; x < 20; ++x)
            {
                for (int y = 0; y < 15; ++y)
                {
                    gridCenters.Batch(new Vector2(x * Model.Tile.WIDTH + Model.Tile.WIDTH / 2, y * Model.Tile.HEIGHT + Model.Tile.HEIGHT / 2), Color.Green);
                }
            }

            gridCenters.End();*/
        }

        #region Test

        public void Test(Core a_coreView)
        {
            Model.Level testLevel = new Model.Level("test.txt");

            DrawLevel(testLevel, a_coreView, null);
        }

        #endregion
    }
}
