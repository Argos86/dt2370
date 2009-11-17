using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Featherick.View
{
    class Core
    {
        GraphicsDeviceManager m_graphics;
        SpriteBatch m_spriteBatch;
        public View.Input m_input;
        public View.PlayerView m_playerView;
        public View.TextureAssets m_assets;
        public View.SoundAssets m_sounds;
        public View.FontAssets m_fonts;
        GraphicsDevice m_device;

        public Core(GraphicsDeviceManager a_graphics)
        {
            m_graphics = a_graphics;
            m_input = new Input();
            m_assets = new View.TextureAssets();
            m_fonts = new View.FontAssets();
            m_sounds = new View.SoundAssets();
        }
 
        public void Initiate(GraphicsDevice a_device, ContentManager a_content)
        {
            m_spriteBatch = new SpriteBatch(a_device);
            m_assets.LoadContent(a_content);
            m_fonts.LoadContent(a_content);
            m_sounds.LoadContent(a_content);

            m_device = a_device;
        }

        public void Update(float a_elapsedTime, int a_width, int a_height)
        {

            m_input.Update(a_elapsedTime, new Vector2(0, 0), new Vector2(a_width, a_height));
        }

        public void Draw(Texture2D a_texture, Vector2 a_pos, Color a_color)
        {
            m_spriteBatch.Draw(a_texture, a_pos, a_color);
        }

        
        //behöver jag denna?
        public void Draw(Texture2D a_texture, Rectangle a_dest, Rectangle a_source, Color a_color)
        {
            m_spriteBatch.Draw(a_texture, a_dest, a_source, a_color);
        }

        public void Draw(Texture2D a_texture, Vector2 a_pos, Rectangle a_rect, Color a_color, float a_rotation, Vector2 a_origin, float a_scale, SpriteEffects a_effects, float a_layerDepth)
        {
            m_spriteBatch.Draw(a_texture, a_pos, a_rect, a_color, a_rotation, a_origin, a_scale, a_effects, a_layerDepth);
        } 

        public void DrawText(string a_text, SpriteFont a_font, Vector2 a_pos, Color a_color)
        {
            m_spriteBatch.DrawString(a_font, a_text, a_pos, a_color);
        }
       
        public void Begin()
        {
            m_spriteBatch.Begin();
        }

        public void End()
        {
            m_spriteBatch.End();
        }        

    }
}
