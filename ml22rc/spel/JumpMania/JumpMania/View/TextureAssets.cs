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
        public Texture2D m_platformtexture;
        public Texture2D m_airtexture;
        public Texture2D m_floortexture;
        public Texture2D m_giantstartexture;
        public Texture2D m_flametexture;
        public Texture2D m_backgroundtexture1;
        public Texture2D m_backgroundtexture2;
        public Texture2D m_backgroundtexture3;
        public Texture2D m_backgroundtexture4;
        public Texture2D m_backgroundtexture5;
        public Texture2D m_lostscreentexture;
        public Texture2D m_wonscreentexture;

        public void LoadContent(ContentManager a_content)
        {
            m_texture = a_content.Load<Texture2D>("images/mainch1");
            m_platformtexture = a_content.Load<Texture2D>("images/tilez2");
            m_airtexture = a_content.Load<Texture2D>("images/otile");
            m_floortexture = a_content.Load<Texture2D>("images/floor");
            m_giantstartexture = a_content.Load<Texture2D>("images/ztar1");
            m_flametexture = a_content.Load<Texture2D>("images/flame");
            m_backgroundtexture1 = a_content.Load<Texture2D>("images/background1");
            m_backgroundtexture2 = a_content.Load<Texture2D>("images/background2");
            m_backgroundtexture3 = a_content.Load<Texture2D>("images/background3");
            m_backgroundtexture4 = a_content.Load<Texture2D>("images/background4");
            m_backgroundtexture5 = a_content.Load<Texture2D>("images/background5");
            m_lostscreentexture = a_content.Load<Texture2D>("images/lostscreen");
            m_wonscreentexture = a_content.Load<Texture2D>("images/wonscreen");
        }
    }
}
