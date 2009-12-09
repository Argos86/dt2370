using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Featherick.View
{
    class LevelView
    {
        public void DrawLevel(Model.Level a_level, Core a_core, Camera a_camera)
        {
            float rescaledImageSize = a_camera.m_scale * 2.0f;

            for (int x = 0; x < Model.Level.WIDTH; x++)
            {
                for (int y = 0; y < Model.Level.HEIGHT; y++)
                {
                    if (a_level.m_tiles[x, y].m_tileType == Model.Tile.TileType.Blocked)
                    {                       
                        a_core.Draw(a_core.m_assets.m_block48Texture, new Vector2(a_camera.m_scale * x - a_camera.m_topLeft.X, a_camera.m_scale * y - a_camera.m_topLeft.Y), Color.White);                        
                    }

                    if (a_level.m_tiles[x, y].m_tileType == Model.Tile.TileType.Exit)
                    {
                        a_core.Draw(a_core.m_assets.m_exitTexture, new Vector2(a_camera.m_scale * x - a_camera.m_topLeft.X, a_camera.m_scale * y - a_camera.m_topLeft.Y), Color.White);  
                    }

                    if (a_level.m_tiles[x, y].m_tileType == Model.Tile.TileType.Water)
                    {
                        a_core.Draw(a_core.m_assets.m_waterTexture, new Vector2(a_camera.m_scale * x - a_camera.m_topLeft.X, a_camera.m_scale * y - a_camera.m_topLeft.Y), Color.White);
                    }

                    if (a_level.m_tiles[x, y].m_tileType == Model.Tile.TileType.Sticky)
                    {
                        a_core.Draw(a_core.m_assets.m_stickyTexture, new Vector2(a_camera.m_scale * x - a_camera.m_topLeft.X, a_camera.m_scale * y - a_camera.m_topLeft.Y), Color.White);
                    }
                }
            }            
        }        

        public void Test(Core a_core)
        {
            Model.Level testLevel = new Model.Level("0.txt");
            Camera testCamera = new Camera();
            for (int x = 0; x < Model.Level.WIDTH; x++)
            {
                for (int y = 0; y < Model.Level.HEIGHT; y++)
                {
                    if (x == y || x == 0 || y == 0 || x == Model.Level.WIDTH - 1 || y == Model.Level.HEIGHT - 1)
                    {
                        testLevel.m_tiles[x, y].m_tileType = Model.Tile.TileType.Blocked;
                    }
                }
            }

            DrawLevel(testLevel, a_core, testCamera);
        }
    }
}
