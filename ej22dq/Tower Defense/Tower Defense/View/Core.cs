using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Tower_Defense.View
{
    class Core
    {
        GraphicsDeviceManager m_graphics;
        SpriteBatch m_spriteBatch;
        public View.Input m_input;
        public View.TextureAssets m_assets;
        public View.FontAssets m_fonts;
        public View.SoundAssets m_sounds;
        GraphicsDevice m_device;

        public Core(GraphicsDeviceManager a_graphic)
        {
            m_graphics = a_graphic;
            m_input = new View.Input();
            m_assets = new View.TextureAssets();
            m_fonts = new View.FontAssets();
            m_sounds = new View.SoundAssets();
            m_graphics.PreferredBackBufferHeight = 800;
            m_graphics.PreferredBackBufferWidth = 1200;
            //m_graphics.IsFullScreen = true;
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

        public void Draw(Texture2D a_texture, Rectangle a_dest, Rectangle a_source, Color a_color)
        {
            m_spriteBatch.Draw(a_texture, a_dest, a_source, a_color);
        }

        public void DrawText(string a_text, SpriteFont a_font, Vector2 a_pos, Color a_color)
        {
            m_spriteBatch.DrawString(a_font, a_text, a_pos, a_color);
        }

        public void DrawMouse()
        {
            Rectangle source = new Rectangle(0, 0, 180, 180);
            Rectangle retval = new Rectangle(
                m_device.Viewport.X,
                m_device.Viewport.Y,
                m_device.Viewport.Width,
                m_device.Viewport.Height);

            Rectangle dest = new Rectangle(0, 0, 16, 16);
            dest.X = (int)m_input.m_mousePos.X - dest.Center.X;
            dest.Y = (int)m_input.m_mousePos.Y - dest.Center.Y;

            Draw(m_assets.m_cursor, dest, source, Color.White);

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
