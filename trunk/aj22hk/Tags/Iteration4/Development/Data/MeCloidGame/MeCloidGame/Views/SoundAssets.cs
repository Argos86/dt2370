using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace MeCloidGame.Views
{
    class SoundAssets
    {
        private SoundEffect m_testSound;

        public SoundEffect TestSound
        {
            get { return m_testSound; }
            set { m_testSound = value; }
        }

        public void LoadContent(ContentManager a_content)
        {
            m_testSound = a_content.Load<SoundEffect>(Helpers.Paths.SOUNDS + "test");
        }
    }
}
