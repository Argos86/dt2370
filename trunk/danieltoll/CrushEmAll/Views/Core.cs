using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ZombieHoards.Views
{
    class Core
    {
        GraphicsDeviceManager m_graphics;
        SpriteBatch m_spriteBatch;
        public Views.Input m_input;
        public Views.TextureAssets m_assets;
        public Views.FontAssets m_fonts;
        public Views.SoundAssets m_sounds;
        GraphicsDevice m_device;

        public Core(GraphicsDeviceManager a_graphics)
        {
            m_graphics = a_graphics;
            m_input = new Input();
            m_assets = new Views.TextureAssets();
            m_fonts = new Views.FontAssets();
            m_sounds = new Views.SoundAssets();
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
            Rectangle source = new Rectangle(64, 0, 32, 32);
            Rectangle retval = new Rectangle(
                m_device.Viewport.X,
                m_device.Viewport.Y,
                m_device.Viewport.Width,
                m_device.Viewport.Height);

            Rectangle dest = new Rectangle(0, 0, 32, 32);
            dest.X = (int)m_input.m_mousePos.X - dest.Center.X;
            dest.Y = (int)m_input.m_mousePos.Y - dest.Center.Y;

            Draw(m_assets.m_texture, dest, source, Color.White);

        }

        public void Begin()
        {
            m_spriteBatch.Begin();
        }

        public void End()
        {
            m_spriteBatch.End();
        }

        public bool Test(float a_elapsedTime, GraphicsDevice a_device)
        {


            Draw(m_assets.m_texture, new Rectangle(0, 0, 256, 256), new Rectangle(0, 0, 256, 256), Color.White);

            DrawText("Testing fonts", m_fonts.m_baseFont, new Vector2(a_device.Viewport.Width / 2, a_device.Viewport.Height / 2), Color.White);
            DrawText("Testing fonts BLUE", m_fonts.m_baseFont, new Vector2(a_device.Viewport.Width / 2, a_device.Viewport.Height / 2 + 64), Color.Blue);

           

            return true;
        }


    }
}
