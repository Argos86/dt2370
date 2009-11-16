using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MeCloidGame.Views
{
    class PlayerView
    {
        public void DrawPlayer(Core a_coreView, Model.Player a_player, float a_scale)
        {
            Vector2 size = new Vector2(96, 96) * a_scale;
            Vector2 pos = a_player.m_pos * a_scale - new Vector2(a_scale / 2);
            Rectangle destRect = new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y);
            a_coreView.Draw(a_coreView.Textures.Sprites, destRect, new Rectangle(96, 0, 96, 96), Color.White);
        }

        public void Test(Core a_coreView)
        {
            Model.Player p = new Model.Player();
            p.m_pos.X = 200;
            p.m_pos.Y = 550;

            DrawPlayer(a_coreView, p, 1.0f);
        }
    }
}
