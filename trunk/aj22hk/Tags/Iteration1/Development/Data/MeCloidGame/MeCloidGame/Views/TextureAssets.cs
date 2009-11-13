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

        public Texture2D m_sprites;

        #endregion

        #region Methods

        public void LoadContent(ContentManager a_content)
        {
            m_sprites = a_content.Load<Texture2D>(Helpers.Paths.TEXTURES + "sprites");
        }

        #endregion
    }
}
