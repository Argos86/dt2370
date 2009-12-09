using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Featherick.View
{
    class MapView
    {
        public void DrawMap(Model.Map a_map, Core a_core, float a_scale)
        {
            Rectangle src = new Rectangle(5, 212, 36, 36);

            for (int x = 0; x < Model.Map.WIDTH; x++)
            {
                for (int y = 0; y < Model.Map.HEIGHT; y++)
                {
                    if (a_map.m_tiles[x, y].m_tileType == Model.Tile.TileType.Blocked)
                    {
                        a_core.Draw(a_core.m_assets.m_blockTexture, new Vector2(a_scale * x, a_scale * y), Color.White);
                        //a_core.Draw(a_core.m_assets.m_texture, new Rectangle(x * a_scale - a_scale / 2, y * a_scale - a_scale / 2, a_scale, a_scale), src, Color.White);
                    }
                    else
                    {
                        // a_core.Draw(a_core.m_assets.m_texture, new Rectangle(x * a_scale, y * a_scale, a_scale, a_scale), src, Color.DarkGray);
                    }
                }
            }

            /*
            // For each tile position
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    // If there is a visible tile in that position
                    Texture2D texture = tiles[x, y].Texture;
                    if (texture != null)
                    {
                        // Draw it in screen space.
                        Vector2 position = new Vector2(x, y) * Tile.Size;
                        spriteBatch.Draw(texture, position, Color.White);
                    }
                }
            }*/
        }        

        public void Test(Core a_core)
        {
            Model.Map testMap = new Model.Map();
            for (int x = 0; x < Model.Map.WIDTH; x++)
            {
                for (int y = 0; y < Model.Map.HEIGHT; y++)
                {
                    if (x == y || x == 0 || y == 0 || x == Model.Map.WIDTH - 1 || y == Model.Map.HEIGHT - 1)
                    {
                        testMap.m_tiles[x, y].m_tileType = Model.Tile.TileType.Blocked;
                    }
                }
            }

            DrawMap(testMap, a_core, 8);
        }
    }
}
