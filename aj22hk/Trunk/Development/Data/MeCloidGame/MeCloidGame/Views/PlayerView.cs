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
        public void DrawPlayer(Core a_coreView, Model.Player a_player, int a_scale)
        {

            Vector2 pos = a_player.m_pos;
            pos = pos * a_scale - new Vector2(a_scale / 2, a_scale / 2);

            Rectangle dest = new Rectangle((int)pos.X, (int)pos.Y, a_scale, a_scale);
            a_coreView.Draw(a_coreView.Textures.Player, dest, new Rectangle(0, 0, 70, 130), Color.White);

            if (Helpers.Settings.Debug)
            {
                DrawBounds(a_coreView, a_player);
            }
        }

        private void DrawBounds(Core a_coreView, Model.Player a_player)
        {
            Rectangle bounds = a_player.BoundingRectangle;
            Helpers.PointBatch point = new Helpers.PointBatch(a_coreView.m_device, 1.0f, 5);
            point.Begin();

            point.Batch(a_player.m_pos, Color.Yellow);
            point.Batch(new Vector2(a_player.m_pos.X, a_player.m_pos.Y - a_player.HEIGHT), Color.Yellow);
            point.Batch(new Vector2(a_player.m_pos.X + a_player.WIDTH / 2, a_player.m_pos.Y - a_player.HEIGHT / 2), Color.Yellow);
            point.Batch(new Vector2(a_player.m_pos.X - a_player.WIDTH / 2, a_player.m_pos.Y - a_player.HEIGHT / 2), Color.Yellow);

            point.Batch(new Vector2(bounds.X, bounds.Y), Color.Violet);
            point.Batch(new Vector2(bounds.X + bounds.Width, bounds.Y), Color.Violet);
            point.Batch(new Vector2(bounds.X, bounds.Y + bounds.Height), Color.Violet);
            point.Batch(new Vector2(bounds.X + bounds.Width, bounds.Y + bounds.Height), Color.Violet);

            point.Batch(new Vector2(bounds.Center.X, bounds.Center.Y), Color.RoyalBlue);

            point.End();

            Helpers.RectangleRenderer rec = new Helpers.RectangleRenderer(a_coreView.m_device, 1.0f);
            rec.Begin();

            rec.Batch(
                new Vector2(bounds.X, bounds.Y),
                new Vector2(bounds.X + bounds.Width, bounds.Y),
                new Vector2(bounds.X + bounds.Width, bounds.Y + bounds.Height),
                new Vector2(bounds.X, bounds.Y + bounds.Height),
                Color.RoyalBlue,
                1.0f);

            rec.End();

            Helpers.LineBatch dirVec = new Helpers.LineBatch(a_coreView.m_device, 1.0f);
            dirVec.Begin();
            Vector2 newPos = a_player.m_pos + a_player.m_velocity;// *a_elapsedTime;
            newPos = new Vector2(newPos.X, newPos.Y - a_player.HEIGHT / 2);
            dirVec.Batch(new Vector2(a_player.m_pos.X, a_player.m_pos.Y - a_player.HEIGHT / 2), Color.Red, newPos, Color.Blue, 1.0f);
            dirVec.End();
        }

        public void Test(Core a_coreView)
        {
            Model.Player p = new Model.Player();
            p.m_pos.X = 200;
            p.m_pos.Y = 550;

            DrawPlayer(a_coreView, p, 1);
        }
    }
}
