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
        public SoundEffect m_playerJump1;
        public SoundEffect m_playerJump2;
        public SoundEffect m_playerJump3;
        
        public void LoadContent(ContentManager a_content)
        {
            m_playerJump1 = a_content.Load<SoundEffect>("sounds/jump1");
            m_playerJump2 = a_content.Load<SoundEffect>("sounds/jump2");
            m_playerJump3 = a_content.Load<SoundEffect>("sounds/jump3");

        }
        
    }
}
