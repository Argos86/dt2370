using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace ZombieHoards.Views
{
    class FontAssets
    {
        public SpriteFont m_baseFont;

        public void LoadContent(ContentManager a_content)
        {
            m_baseFont = a_content.Load<SpriteFont>("Courier New");
            
        }
    }
}
