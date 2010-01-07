using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense.View
{
    class LevelView
    {
        public void DrawLevel(Model.Map a_map, Core a_core, int a_scale)
        {
            Rectangle src = new Rectangle(5, 212, 36, 36);

            for (int x = 0; x < Model.Map.WIDTH; x++)
            {
                for (int y = 0; y < Model.Map.HEIGHT; y++)
                {
                    if (a_map.m_tiles[y, x].m_tileType == Model.Tile.TileType.Blocked)
                    {
                        a_core.Draw(a_core.m_assets.m_blank, new Rectangle(x * a_scale - a_scale / 2, y * a_scale - a_scale / 2, a_scale, a_scale), src, Color.PaleGoldenrod);
                    }
                    else if (a_map.m_tiles[y, x].m_tileType == Model.Tile.TileType.Towerground)
                    {
                        a_core.Draw(a_core.m_assets.m_blank, new Rectangle(x * a_scale - a_scale / 2, y * a_scale - a_scale / 2, a_scale, a_scale), src, new Color(64,64,64,255));
                    }
                    else
                    {
                        // a_core.Draw(a_core.m_assets.m_texture, new Rectangle(x * a_scale, y * a_scale, a_scale, a_scale), src, Color.DarkGray);
                    }
                }
            }
        }

        public void DrawMenu(Core a_core)
        {
            Rectangle dest = new Rectangle((int)920, (int)0, 280, 810);
            a_core.Draw(a_core.m_assets.m_menu, dest, new Rectangle(0, 0, 165, 525), Color.White);
        }
    }
}
