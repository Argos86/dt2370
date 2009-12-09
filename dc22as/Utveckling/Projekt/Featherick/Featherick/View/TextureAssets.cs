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
        public Texture2D m_enemy1Texture;
        public Texture2D m_block16Texture;
        public Texture2D m_block64Texture;
        public Texture2D m_block48Texture;
        public Texture2D m_exitTexture;
        public Texture2D m_WinScreen;
        public Texture2D m_LoseScreen;
        public Texture2D m_waterTexture;
        public Texture2D m_stickyTexture;

        public void LoadContent(ContentManager a_content)
        {
            m_enemy1Texture = a_content.Load<Texture2D>("images/enemy1");
            m_playerTexture = a_content.Load<Texture2D>("images/playerSprite");
            m_block16Texture = a_content.Load<Texture2D>("images/block16");
            m_block64Texture = a_content.Load<Texture2D>("images/block64");
            m_block48Texture = a_content.Load<Texture2D>("images/block48");
            m_exitTexture = a_content.Load<Texture2D>("images/exit");
            m_waterTexture = a_content.Load<Texture2D>("images/water");
            m_WinScreen = a_content.Load<Texture2D>("images/winScreen");
            m_LoseScreen = a_content.Load<Texture2D>("images/loseScreen");
            m_stickyTexture = a_content.Load<Texture2D>("images/sticky");
        }
    }
}
