using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CombatLand.View
{
    class MapView
    {
        private const int TILE_WIDTH = 64;
        private const int TILE_HEIGHT = 48;

        public void DrawMap(Model.Map a_map, Core a_core, Camera a_camera, int a_scale)
        {
            Rectangle src = new Rectangle(0, 0, TILE_WIDTH, TILE_HEIGHT);
            for (int x = 0; x < Model.Map.WIDTH; x++)
            {
                for (int y = 0; y < Model.Map.HEIGHT; y++)
                {
                    if (a_map.m_tiles[x, y].m_tileType == Model.Tile.TileType.Blocked)
                    {
                        a_core.Draw(a_core.m_assets.m_tileTexture, new Rectangle((int)((x * a_scale)- a_camera.m_pos.X), (int)(y * a_scale), a_scale, a_scale), src, Color.White);
                    }
                }
            }
        }
    }
}
