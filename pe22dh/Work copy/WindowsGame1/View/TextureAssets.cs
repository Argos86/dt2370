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
        public Texture2D m_playerTexture;
        public Texture2D m_tileTexture;
        public Texture2D m_mainMenuTexture;
        public Texture2D m_overlayGameOver;
        public Texture2D m_overlayWin;
        public Texture2D m_crosshair;

        public void LoadContent(ContentManager a_content)
        {
            m_playerTexture = a_content.Load<Texture2D>("assets/sprites/player");
            m_tileTexture = a_content.Load<Texture2D>("assets/sprites/whiteTile");
            m_mainMenuTexture = a_content.Load<Texture2D>("assets/sprites/mainMenu");
            m_overlayGameOver = a_content.Load<Texture2D>("assets/sprites/overlayGameOver");
            m_overlayWin = a_content.Load<Texture2D>("assets/sprites/overlayWin");
            m_crosshair = a_content.Load<Texture2D>("assets/sprites/crosshair");
        }
    }
}
