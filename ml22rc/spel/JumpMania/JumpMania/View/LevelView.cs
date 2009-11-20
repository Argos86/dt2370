using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace JumpMania.View
{
    class LevelView
    {
        public void DrawLevel(Model.Level a_map, Core a_core, int a_scale)
        {
            Rectangle src = new Rectangle(5, 212, 36, 36);

            for (int x = 0; x < Model.Level.WIDTH; x++)
            {
                for (int y = 0; y < Model.Level.HEIGHT; y++)
                {
                    if (a_map.m_tiles[x, y].m_tileType == Model.Tile.TileType.Platform)
                    {
                        
                        a_core.Draw(a_core.m_assets.m_leveltexture, new Vector2 (200, 200), new Rectangle(0, 0, 121, 180), Color.White);
                    }
                }
            }
        }

        public void Test(Core a_core)
        {
            Model.Level testLevel = new Model.Level();
            for (int x = 0; x < Model.Level.WIDTH; x++)
            {
                for (int y = 0; y < Model.Level.HEIGHT; y++)
                {
                    if (x == y || x == 0 || y == 0 || x == Model.Level.WIDTH - 1 || y == Model.Level.HEIGHT - 1)
                    {
                        testLevel.m_tiles[x, y].m_tileType = Model.Tile.TileType.Platform;
                    }
                }
            }

            DrawLevel(testLevel, a_core, 8);
        }
    }
}
