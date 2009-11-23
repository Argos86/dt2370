using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZombieHoards.Views
{
    class MapView
    {
        
        public void DrawMap(Model.Map a_map, Core a_core, int a_scale)
        {
            Rectangle src = new Rectangle(5, 212, 36, 36);

            for (int x = 0; x < Model.Map.WIDTH; x++)
            {
                for (int y = 0; y < Model.Map.HEIGHT; y++)
                {
                    if (a_map.m_tiles[x, y].m_tileType == Model.Tile.TileType.Blocked)
                    {
                        a_core.Draw(a_core.m_assets.m_texture, new Rectangle(x * a_scale , y * a_scale , a_scale, a_scale), src, Color.White);
                    }
                    else
                    {
                       // a_core.Draw(a_core.m_assets.m_texture, new Rectangle(x * a_scale, y * a_scale, a_scale, a_scale), src, Color.DarkGray);
                    }
                }
            }
        }


        public void Test(Core a_core)
        {
            Model.Map testMap = new Model.Map();
            for (int x = 0; x < Model.Map.WIDTH; x++)
            {
                for (int y = 0; y < Model.Map.HEIGHT; y++)
                {
                    if (x == y || x == 0 || y == 0 || x == Model.Map.WIDTH-1 || y == Model.Map.HEIGHT-1)
                    {
                        testMap.m_tiles[x, y].m_tileType = Model.Tile.TileType.Blocked;
                    }
                }
            }

            DrawMap(testMap, a_core, 8);
        }
    }
}
