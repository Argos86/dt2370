using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Featherick.View
{
    class TextureAssets
    {
        public Texture2D m_playerTexture;
        public Texture2D m_blockTexture;
        public Texture2D m_rectTexture;

        public void LoadContent(ContentManager a_content)
        {
            m_playerTexture = a_content.Load<Texture2D>("images/playerIdle3");
            m_blockTexture = a_content.Load<Texture2D>("images/sprite");
            m_rectTexture = a_content.Load<Texture2D>("images/rectangle");
        }
    }
}
