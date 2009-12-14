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

        public void DrawMap(Model.Map a_map, Core a_core, Camera a_camera)
        {
           
            float a_layerDepth = 0.0f;
            Rectangle src = new Rectangle(0, 0, TILE_WIDTH, TILE_HEIGHT);
            Color a_color;
            for (int x = 0; x < Model.Map.WIDTH; x++)
            {
                for (int y = 0; y < Model.Map.HEIGHT; y++)
                {
                    Vector2 a_pos = a_camera.Scale(new Vector2(x, y));
                    if (a_map.m_tiles[x, y].m_tileType != Model.Tile.TileType.Empty)
                    {
                        a_color = Color.White;
                        if (a_map.m_tiles[x, y].m_tileType == CombatLand.Model.Tile.TileType.Blocked)
                        {
                            a_color = Color.DarkGreen;
                        }
                        else if (a_map.m_tiles[x, y].m_tileType == CombatLand.Model.Tile.TileType.Win)
                        {
                            a_color = Color.Gold;
                        }

                        a_core.Draw(a_core.m_assets.m_tileTexture, new Rectangle((int)(a_pos.X),
                        (int)(a_pos.Y), View.Camera.m_scale, View.Camera.m_scale), src, a_color, SpriteEffects.None, a_layerDepth);
                    }
                }
            }
        }
    }
}
