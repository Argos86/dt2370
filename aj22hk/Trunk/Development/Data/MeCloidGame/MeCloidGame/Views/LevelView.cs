﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MeCloidGame.Views
{
    class LevelView
    {
        public void DrawLevel(Model.Level a_level, Core a_coreView, float a_scale)
        {
            //Rectangle src = new Rectangle(0, 0, 30, 20);

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
        }

        #region Test

        public void Test(Core a_coreView)
        {
            Model.Level testLevel = new Model.Level("test.txt");

            DrawLevel(testLevel, a_coreView, 1.0f);
        }

        #endregion
    }
}