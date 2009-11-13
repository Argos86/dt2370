using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MeCloidGame.Views
{
    class Core
    {
        #region Fields

        GraphicsDeviceManager m_graphics;
        SpriteBatch m_spriteBatch;
        private Views.FontAssets m_fonts;
        public Views.TextureAssets m_textures;
        public GraphicsDevice m_device;

        #endregion

        #region Properties

        public Views.FontAssets Fonts
        {
            get { return m_fonts; }
            set { m_fonts = value; }
        }

        #endregion

        #region Constructors

        public Core(GraphicsDeviceManager a_graphics)
        {
            m_graphics = a_graphics;
            m_fonts = new Views.FontAssets();
            m_textures = new Views.TextureAssets();
        }

        #endregion

        #region Methods

        public void Initialize(GraphicsDevice a_device, ContentManager a_content)
        {
            m_spriteBatch = new SpriteBatch(a_device);

            m_fonts.LoadContent(a_content);
            m_textures.LoadContent(a_content);

            m_device = a_device;
        }

        public void Draw(Texture2D a_texture, Rectangle a_dest, Rectangle a_src, Color a_color)
        {
            m_spriteBatch.Draw(a_texture, a_dest, a_src, a_color);
        }

        public void DrawText(string a_text, SpriteFont a_font, Vector2 a_pos, Color a_color)
        {
            m_spriteBatch.DrawString(a_font, a_text, a_pos, a_color);
        }

        public void DrawCenteredText(string a_text, SpriteFont a_font, Vector2 a_pos, Color a_color)
        {
            Vector2 textCenter = new Vector2(a_pos.X - a_font.MeasureString(a_text).X / 2, a_pos.Y - a_font.MeasureString(a_text).Y / 2);
            m_spriteBatch.DrawString(a_font, a_text, textCenter, a_color);
        }

        public void Begin()
        {
            m_spriteBatch.Begin();
        }

        public void End()
        {
            m_spriteBatch.End();
        }

        #endregion

        #region Test

        public bool Test(GraphicsDevice a_device)
        {
            Vector2 screenCenter = new Vector2(a_device.Viewport.Width / 2, a_device.Viewport.Height / 2);

            Draw(m_textures.m_sprites, new Rectangle(5, 30, 256, 256), new Rectangle(0, 0, 256, 256), Color.BlueViolet);
            Draw(m_textures.m_sprites, new Rectangle(260, 30, 128, 256), new Rectangle(32, 2, 32, 64), Color.BurlyWood);
            Draw(m_textures.m_sprites, new Rectangle(520, 30, 128, 256), new Rectangle(2, 2, 32, 64), Color.Chartreuse);

            DrawText("Core::DrawText - Font: Arial, Position: 0.0, Color: White", m_fonts.Arial, Vector2.Zero, Color.White);
            DrawCenteredText("Core::DrawCenteredText\nFont: Georgia\nPosition: Screen center\nColor: Red", m_fonts.Georgia, screenCenter, Color.Red);

            return true;
        }

        #endregion
    }
}
