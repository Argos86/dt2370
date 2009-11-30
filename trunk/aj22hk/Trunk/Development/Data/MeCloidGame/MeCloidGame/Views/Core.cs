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

        public GraphicsDeviceManager m_graphics;
        public Helpers.DisplaySettings m_displaySettings;
        SpriteBatch m_spriteBatch;
        private Helpers.InputHandler m_input;

        public Helpers.InputHandler Input
        {
            get { return m_input; }
            set { m_input = value; }
        }
        private Helpers.KeyboardSettings m_keyboardSettings;
        private Views.FontAssets m_fonts;
        private Views.TextureAssets m_textures;
        private Views.SoundAssets m_sounds;

        public Views.SoundAssets Sounds
        {
            get { return m_sounds; }
            set { m_sounds = value; }
        }

        public Views.TextureAssets Textures
        {
            get { return m_textures; }
            set { m_textures = value; }
        }
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
            
            m_displaySettings = Helpers.SettingsHandler.ReadDisplaySettings(@"Content\" + Helpers.Paths.SETTINGS + "DisplaySettings.xml");
            m_graphics.IsFullScreen = m_displaySettings.FullScreen;
            m_graphics.PreferredBackBufferWidth = m_displaySettings.WindowWidth;
            m_graphics.PreferredBackBufferHeight = m_displaySettings.WindowHeight;
            m_graphics.PreferMultiSampling = true;

            m_keyboardSettings = Helpers.SettingsHandler.ReadKeyboardSettings(@"Content\" + Helpers.Paths.SETTINGS + "KeyboardSettings.xml");
            m_input = new Helpers.InputHandler(PlayerIndex.One, Helpers.SettingsHandler.GetKeyboardDictionary(m_keyboardSettings));

            m_fonts = new Views.FontAssets();
            m_textures = new Views.TextureAssets();
            m_sounds = new Views.SoundAssets();
        }

        #endregion

        #region Methods

        public void Initialize(GraphicsDevice a_device, ContentManager a_content)
        {
            m_spriteBatch = new SpriteBatch(a_device);

            m_fonts.LoadContent(a_content);
            m_textures.LoadContent(a_content);
            m_sounds.LoadContent(a_content);
            
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

        public void Update()
        {
            m_input.Update();
        }

        #endregion

        #region Test

        public bool Test(GraphicsDevice a_device)
        {
            Vector2 screenCenter = new Vector2(a_device.Viewport.Width / 2, a_device.Viewport.Height / 2);

            Draw(m_textures.Sprites, new Rectangle(0, 30, 288, 96), new Rectangle(0, 0, 288, 96), Color.White);
            Draw(m_textures.Sprites, new Rectangle(290, 30, 96, 96), new Rectangle(96, 0, 96, 96), Color.White);
            Draw(m_textures.Sprites, new Rectangle(388, 30, 96, 96), new Rectangle(0, 0, 96, 96), Color.White);
            Draw(m_textures.Sprites, new Rectangle(486, 30, 96, 96), new Rectangle(192, 0, 96, 96), Color.White);

            DrawText("Core::DrawText - Font: Arial, Position: 0.0, Color: White", m_fonts.Arial, Vector2.Zero, Color.White);
            DrawCenteredText("Core::DrawCenteredText\nFont: Georgia\nPosition: Screen center\nColor: Red", m_fonts.Georgia, screenCenter, Color.Red);

            return true;
        }

        #endregion
    }
}
