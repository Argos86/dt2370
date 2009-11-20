using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense.View
{
    class LevelView
    {
        public void DrawLevel(Core a_core)
        {
            Rectangle dest = new Rectangle((int)0, (int)0, 1280, 800);
            a_core.Draw(a_core.m_assets.m_bana1, dest, new Rectangle(0, 0, 1280, 550), Color.White);
        }
    }
}
