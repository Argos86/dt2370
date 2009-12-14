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

        private Texture2D m_player;

        public Texture2D Player
        {
            get { return m_player; }
            set { m_player = value; }
        }

        private Texture2D m_explosion;

        public Texture2D Explosion
        {
            get { return m_explosion; }
            set { m_explosion = value; }
        }

        #endregion

        #region Methods

        public void LoadContent(ContentManager a_content)
        {
            m_sprites = a_content.Load<Texture2D>(Helpers.Paths.SPRITES + "sprites");
            m_tiles = a_content.Load<Texture2D>(Helpers.Paths.TILES + "tiles");
            m_player = a_content.Load<Texture2D>(Helpers.Paths.SPRITES + "player");
            m_explosion = a_content.Load<Texture2D>(Helpers.Paths.SPRITES + "explosion");
        }

        #endregion
    }
}
