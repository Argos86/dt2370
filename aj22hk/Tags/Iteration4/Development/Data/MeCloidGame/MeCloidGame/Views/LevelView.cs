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
        public void DrawLevel(Model.Level a_level, Core a_coreView, Camera a_camera)
        {
            if (Helpers.Settings.Edit)
            {
                DrawGrid(a_level, a_coreView, a_camera);
                DrawTiles(a_level, a_coreView, a_camera);
            }
            else
            {
                for (int x = 0; x < Model.Level.WIDTH; ++x)
                {
                    for (int y = 0; y < Model.Level.HEIGHT; ++y)
                    {
                        Vector2 pos = a_camera.Translate(new Vector2(x, y));
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
        }

        private void DrawGrid(Model.Level a_level, Core a_coreView, Camera a_camera)
        {
            // Grid lines
            Helpers.LineBatch gridLines = new Helpers.LineBatch(a_coreView.m_device, 0.4f);
            gridLines.Begin();

            // Vertical lines
            for (int x = 0; x <= a_level.Width; ++x)
            {
                gridLines.Batch(new Vector2(x * a_camera.m_zoom - a_camera.TopLeft.X , 0 - a_camera.TopLeft.Y), new Vector2(x * a_camera.m_zoom - a_camera.TopLeft.X, a_level.Height * a_camera.m_zoom - a_camera.TopLeft.Y), Color.Red, 1.0f);
            }

            // Horizontal lines
            for (int y = 0; y <= a_level.Height; ++y)
            {
                gridLines.Batch(new Vector2(0 - a_camera.TopLeft.X, y * a_camera.m_zoom - a_camera.TopLeft.Y), new Vector2(a_level.Width * a_camera.m_zoom - a_camera.TopLeft.X, y * a_camera.m_zoom - a_camera.TopLeft.Y), Color.Red, 1.0f);
            }

            gridLines.End();
        }

        private void DrawTiles(Model.Level a_level, Core a_coreView, Camera a_camera)
        {
            Helpers.PointBatch tiles = new Helpers.PointBatch(a_coreView.m_device, 0.8f, a_camera.m_zoom);
            tiles.Begin();

            for (int x = 0; x < Model.Level.WIDTH; ++x)
            {
                for (int y = 0; y < Model.Level.HEIGHT; ++y)
                {
                    Vector2 pos = a_camera.Translate(new Vector2(x, y));
                    pos = new Vector2(pos.X + a_camera.m_zoom / 2, pos.Y + a_camera.m_zoom / 2);
                    switch (a_level.Tiles[x, y].Type)
                    {
                        case Model.Tile.TileType.Solid:
                            tiles.Batch(pos, Color.BlanchedAlmond);
                            break;
                        case Model.Tile.TileType.Destroyable:
                            tiles.Batch(pos, Color.BlueViolet);
                            break;
                    }
                }
            }

            tiles.End();
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
