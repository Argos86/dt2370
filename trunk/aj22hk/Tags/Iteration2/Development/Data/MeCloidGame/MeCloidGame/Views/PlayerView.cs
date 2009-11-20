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
        // TODO: Make applying sprite more general.
        public void DrawPlayer(Core a_coreView, Model.Player a_player, float a_scale)
        {
            Vector2 size = new Vector2(a_player.WIDTH, a_player.HEIGHT) * a_scale;
            Vector2 pos = Vector2.Zero;
            pos.X = (a_player.m_pos.X - size.X / 2) * a_scale - (a_scale / 2);
            pos.Y = (a_player.m_pos.Y - size.Y) * a_scale - (a_scale / 2);
            Rectangle destRect = new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y);
            a_coreView.Draw(a_coreView.Textures.Player, destRect, new Rectangle(0, 0, (int)size.X, (int)size.Y), Color.White);
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
