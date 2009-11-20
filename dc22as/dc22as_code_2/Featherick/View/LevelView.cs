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
        public void DrawLevel(Model.Level a_level, Core a_core, float a_scale)
        {
            Rectangle src = new Rectangle(5, 212, 36, 36);

            for (int x = 0; x < Model.Level.WIDTH; x++)
            {
                for (int y = 0; y < Model.Level.HEIGHT; y++)
                {
                    if (a_level.m_tiles[x, y].m_tileType == Model.Tile.TileType.Blocked)
                    {
                        a_core.Draw(a_core.m_assets.m_blockTexture, new Vector2(a_scale * x, a_scale * y), Color.White);                       
                    }
                    else
                    {   
                    
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
                        testLevel.m_tiles[x, y].m_tileType = Model.Tile.TileType.Blocked;
                    }
                }
            }

            DrawLevel(testLevel, a_core, 8);
        }
    }
}
