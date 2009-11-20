using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MeCloidGame.Views
{
    class FontAssets
    {
        #region Fields

        private SpriteFont m_arialFont;
        private SpriteFont m_georgiaFont;

        #endregion

        #region Properties

        public SpriteFont Arial
        {
            get { return m_arialFont; }
            set { m_arialFont = value; }
        }

        public SpriteFont Georgia
        {
            get { return m_georgiaFont; }
            set { m_georgiaFont = value; }
        }

        #endregion

        #region Methods

        public void LoadContent(ContentManager a_content)
        {
            m_arialFont = a_content.Load<SpriteFont>(Helpers.Paths.FONTS + "Arial");
            m_georgiaFont = a_content.Load<SpriteFont>(Helpers.Paths.FONTS + "Georgia");
        }

        #endregion
    }
}
