using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MeCloidGame.Views
{
    class TextureAssets
    {
        #region Fields

        private Texture2D m_sprites;

        public Texture2D Sprites
        {
            get { return m_sprites; }
            set { m_sprites = value; }
        }
        private Texture2D m_tiles;

        public Texture2D Tiles
        {
            get { return m_tiles; }
            set { m_tiles = value; }
        }

        #endregion

        #region Methods

        public void LoadContent(ContentManager a_content)
        {
            m_sprites = a_content.Load<Texture2D>(Helpers.Paths.SPRITES + "sprites");
            m_tiles = a_content.Load<Texture2D>(Helpers.Paths.TILES + "tiles");
        }

        #endregion
    }
}
