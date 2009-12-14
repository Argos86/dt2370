using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CombatLand.View
{
    class FontAssets
    {
        public SpriteFont m_font;

        public void LoadContent(ContentManager a_content)
        {
            m_font = a_content.Load<SpriteFont>("assets/fonts/Courier New");
        }
    }
}
