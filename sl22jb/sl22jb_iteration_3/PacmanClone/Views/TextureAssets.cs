using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PacmanClone.Views
{
    class TextureAssets
    {
        public Texture2D m_sprites;

        public void LoadContent(ContentManager a_content)
        {
            m_sprites = a_content.Load<Texture2D>("images/playerSprite");
        }
    }
}
