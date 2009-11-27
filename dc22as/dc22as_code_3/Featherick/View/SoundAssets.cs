using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Featherick.View
{
    class SoundAssets
    {
        public SoundEffect m_playerJump;
        
        public void LoadContent(ContentManager a_content)
        {
            m_playerJump = a_content.Load<SoundEffect>("sounds/jump1");

        }
        
    }
}
