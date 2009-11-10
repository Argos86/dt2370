using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace ZombieHoards.Views
{
    class SoundAssets
    {
        public SoundEffect m_fireGun;
        public SoundEffect m_zombieAttack;

        public void LoadContent(ContentManager a_content)
        {
            m_fireGun = a_content.Load<SoundEffect>("m_fireGun");
            m_zombieAttack = a_content.Load<SoundEffect>("m_zombieAttack");
        }
    }
}
