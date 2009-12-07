using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TileDemo.View
{
    class LevelView
    {

        public TextureAssets m_textures = new TextureAssets();
        public static int g_tileSize = 64;

        public int m_variation = 320;

        public void DrawLevel(Model.Level a_level, ref SpriteBatch a_batch)
        {
            float scale = g_tileSize;

            for (int x = 0; x < Model.Level.WIDTH; x++)
            {
                for (int y = 0; y < Model.Level.HEIGHT; y++)
                {
                    
                        
                    int type = 0;
                    int rot =0;

                    type = GetVisualTileType(x, y, a_level, ref rot);
                    float rotation = (float)rot * (float)Math.PI / 2.0f;

                    int var = (x + y * 32) % 2 == 1 ? m_variation : m_variation + 64;
                    Rectangle sourceUV = new Rectangle(type * g_tileSize, var, g_tileSize, g_tileSize);



                    a_batch.Draw(m_textures.m_sprites, new Rectangle((int)(x * scale), (int)(y * scale), (int)scale, (int)scale), sourceUV, Color.White, rotation, new Vector2(g_tileSize / 2), SpriteEffects.None, 0.0f);
                    
                }

            }
        }


        int GetVisualTileType(int a_x, int a_y, Model.Level a_game, ref int a_rotation) {

	        bool[] b = {false, false, false, false};


            b[0] = a_game.GetTile(a_x + 1, a_y).m_isSet; //1
            b[1] = a_game.GetTile(a_x + 1, a_y + 1).m_isSet; //2
            b[2] = a_game.GetTile(a_x, a_y + 1).m_isSet;//4
            b[3] = a_game.GetTile(a_x, a_y).m_isSet;//8

	        int mask = 0;
	        for(int i = 0; i < 4; i++)
	        {
		        if(b[i]) {
			        mask |= (1 << i);
		        }
	        }
	        return (int)GetTileNumber(mask, ref a_rotation);
        	
        }

        enum Tile
        {
            None,
            Full,
            Corner,
            Side, 
            Three,
            Opposite
        }

        Tile GetTileNumber(int a_mask, ref int a_outRotation)
        {
			
			switch (a_mask) {
                case 0: a_outRotation = 0; return Tile.None;
                case 1: a_outRotation = 1; return Tile.Corner;
                case 2: a_outRotation = 2; return Tile.Corner;
                case 3: a_outRotation = 1; return Tile.Side;
                case 4: a_outRotation = 3; return Tile.Corner;
                case 5: a_outRotation = 0; return Tile.Opposite; //lower left and upper right
                case 6: a_outRotation = 2; return Tile.Side;
                case 7: a_outRotation = 2; return Tile.Three;
                case 8: a_outRotation = 0; return Tile.Corner; //only lower right
                case 9: a_outRotation = 0; return Tile.Side;
                case 10: a_outRotation = 3; return Tile.Opposite;
                case 11: a_outRotation = 1; return Tile.Three;
                case 12: a_outRotation = 3; return Tile.Side; //only lower right
                case 13: a_outRotation = 0; return Tile.Three;
                case 14: a_outRotation = 3; return Tile.Three;
                case 15: a_outRotation = 0; return Tile.Full; //only lower right
			}

			return 0;
		}
    }
}
