using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CombatLand.View
{
    class TextureAssets
    {
        public Texture2D m_texture;

        public void LoadContent(ContentManager a_content)
        {
            m_texture = a_content.Load<Texture2D>("assets/sprites/untitled");
        }
    }
}
