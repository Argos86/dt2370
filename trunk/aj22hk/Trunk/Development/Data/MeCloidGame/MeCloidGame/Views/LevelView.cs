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
        public void DrawLevel(Model.Level a_level, Core a_coreView, int a_scale)
        {
            for (int x = 0; x < Model.Level.WIDTH; ++x)
            {
                for (int y = 0; y < Model.Level.HEIGHT; ++y)
                {
                    Rectangle src;
                    switch (a_level.Tiles[x, y].Type)
                    {
                        case Model.Tile.TileType.Solid:
                            if (x % 2 == 0)
                            {
                                src = new Rectangle((int)Model.Tile.Size.X * 0, (int)Model.Tile.Size.Y * 0, (int)Model.Tile.Size.X, (int)Model.Tile.Size.Y);
                            }
                            else
                            {
                                src = new Rectangle((int)Model.Tile.Size.X * 2, (int)Model.Tile.Size.Y * 0, (int)Model.Tile.Size.X, (int)Model.Tile.Size.Y);
                            }
                            a_coreView.Draw(a_coreView.Textures.Tiles, new Rectangle(x * a_scale, y * a_scale, a_scale, a_scale), src, Color.White);
                            break;
                        case Model.Tile.TileType.Destroyable:
                            src = new Rectangle((int)Model.Tile.Size.X * 1, (int)Model.Tile.Size.Y * 0, (int)Model.Tile.Size.X, (int)Model.Tile.Size.Y);
                            a_coreView.Draw(a_coreView.Textures.Tiles, new Rectangle(x * a_scale, y * a_scale, a_scale, a_scale), src, Color.White);
                            break;
                    }
                }
            }

            DrawGrid(a_coreView);
        }
        /*public void DrawLevel(Model.Level a_level, Core a_coreView, float a_scale)
        {
            for (int y = 0; y < a_level.Height; ++y)
            {
                for (int x = 0; x < a_level.Width; ++x)
                {
                    Vector2 size = Model.Tile.Size * a_scale;
                    Vector2 position = new Vector2(x, y) * size;
                    Rectangle destRect = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
                    Vector2 srcLocation;
                    Rectangle srcRect;

                    switch (a_level.Tiles[x, y].Type)
                    {
                        case Model.Tile.TileType.Solid:
                            if (x % 2 == 0)
                            {
                                srcLocation = new Vector2((int)Model.Tile.Size.X * 0, (int)Model.Tile.Size.Y * 0);
                            }
                            else
                            {
                                srcLocation = new Vector2((int)Model.Tile.Size.X * 2, (int)Model.Tile.Size.Y * 0);
                            }

                            srcRect = new Rectangle((int)srcLocation.X, (int)srcLocation.Y, (int)Model.Tile.Size.X, (int)Model.Tile.Size.Y);

                            a_coreView.Draw(a_coreView.Textures.Tiles, destRect, srcRect, Color.White);
                            break;
                        case Model.Tile.TileType.Destroyable:
                            srcLocation = new Vector2((int)Model.Tile.Size.X * 1, (int)Model.Tile.Size.Y * 0);

                            srcRect = new Rectangle((int)srcLocation.X, (int)srcLocation.Y, (int)Model.Tile.Size.X, (int)Model.Tile.Size.Y);

                            a_coreView.Draw(a_coreView.Textures.Tiles, destRect, srcRect, Color.White);
                            break;
                    }
                }
            }

            if (Helpers.Settings.Debug)
            {
                DrawGrid(a_coreView);
            }
        }*/

        private void DrawGrid(Core a_coreView)
        {
            // Grid lines
            Helpers.LineBatch gridLines = new Helpers.LineBatch(a_coreView.m_device, 0.4f);
            gridLines.Begin();

            for (int x = 0; x < 1280; x += 64)
            {
                gridLines.Batch(new Vector2(x, 0), new Vector2(x, 720), Color.Red, 1.0f);
            }

            for (int y = 0; y < 720; y += 48)
            {
                gridLines.Batch(new Vector2(0, y), new Vector2(1280, y), Color.Red, 1.0f);
            }

            gridLines.End();

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

            gridCenters.End();
        }

        #region Test

        public void Test(Core a_coreView)
        {
            Model.Level testLevel = new Model.Level("test.txt");

            DrawLevel(testLevel, a_coreView, 1);
        }

        #endregion
    }
}
