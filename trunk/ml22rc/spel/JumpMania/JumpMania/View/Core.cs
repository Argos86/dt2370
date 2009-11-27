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

namespace JumpMania.View
{
    class Core
    {
        SpriteBatch m_spriteBatch;
        public View.TextureAssets m_assets;
        public GraphicsDeviceManager m_graphics;
        GraphicsDevice m_device;


        public Core(GraphicsDeviceManager a_graphics)
        {
            m_graphics = a_graphics;
            m_assets = new View.TextureAssets();
            m_graphics.PreferredBackBufferHeight = 900;
            m_graphics.PreferredBackBufferWidth = 900;

            //Möjliggör fullskärm
            //m_graphics.ToggleFullScreen();
            //m_graphics.IsFullScreen = true;
        }

        public void Initiate(GraphicsDevice a_device, ContentManager a_content)
        {
            m_assets.LoadContent(a_content);
            m_spriteBatch = new SpriteBatch(a_device);
            m_device = a_device;
        }

        public void Draw(Texture2D a_texture, Vector2 position, Rectangle a_source, int a_scale, Color a_color)
        {
            Rectangle dest = new Rectangle((int)position.X, (int)position.Y, a_scale, a_scale);
            m_spriteBatch.Draw(a_texture, dest, a_source, a_color);
        }
        

        public void DrawPlaya(GameTime gameTime, GraphicsDevice a_device, Vector2 a_pos)
        {
            int scale = m_graphics.GraphicsDevice.Viewport.Width / Model.Level.WIDTH;
            Draw(m_assets.m_texture, a_pos, new Rectangle(0, 0, 121, 180), scale, Color.White);
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
