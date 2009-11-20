using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Tower_Defense.View
{
    class TextureAssets
    {
        public Texture2D m_texture;
        public Texture2D m_cursor;
        public Texture2D m_bana1;

        public void LoadContent(ContentManager a_content)
        {
            m_texture = a_content.Load<Texture2D>("images/sprites");
            m_cursor = a_content.Load<Texture2D>("images/cursor");
            m_bana1 = a_content.Load<Texture2D>("images/bana1");
        }
    }
}
