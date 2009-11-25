using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace CombatLand.View
{
    class Core
    {
        GraphicsDeviceManager m_graphics;
        SpriteBatch m_spriteBatch;
        public View.Input m_input;
        public View.TextureAssets m_assets;
        //public View.FontAssets m_fonts;
        //public View.SoundAssets m_sounds;
        GraphicsDevice m_device;

        public Core(GraphicsDeviceManager a_graphics)
        {
            m_graphics = a_graphics;
            m_input = new Input();
            m_assets = new View.TextureAssets();
            //m_fonts = new Views.FontAssets();
            //m_sounds = new Views.SoundAssets();
        }
        public void Initiate(GraphicsDevice a_device, ContentManager a_content)
        {
            m_spriteBatch = new SpriteBatch(a_device);
            m_assets.LoadContent(a_content);
            //m_fonts.LoadContent(a_content);
            //m_sounds.LoadContent(a_content);

            m_device = a_device;
        }
        public void Draw(Texture2D a_texture, Rectangle a_dest, Rectangle a_source, Color a_color)
        {
            m_spriteBatch.Draw(a_texture, a_dest, a_source, a_color);
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
