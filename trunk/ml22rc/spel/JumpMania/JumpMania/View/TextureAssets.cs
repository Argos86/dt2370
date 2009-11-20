using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace JumpMania.View
{
    class TextureAssets
    {
        public Texture2D m_texture;
        public Texture2D m_leveltexture;


        public void LoadContent(ContentManager a_content)
        {
            m_texture = a_content.Load<Texture2D>("images/karaktar_ninjaremake2");
            m_leveltexture = a_content.Load<Texture2D>("images/tile");
        }
    }
}
