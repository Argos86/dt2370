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
        public void DrawPlayer(Core a_coreView, Model.Player a_player, Model.Camera a_camera)
        {
            Vector2 pos = a_player.m_pos;
            pos.X = pos.X - (Model.Player.WIDTH) / 2;
            pos.Y = pos.Y - Model.Player.HEIGHT;
            pos = a_camera.Translate(pos) * a_camera.m_zoom;

            if (Helpers.Settings.Debug)
            {
                a_coreView.DrawText(a_player.m_pos.ToString(), a_coreView.Fonts.Georgia, new Vector2(70.0f, 50.0f), Color.Red);
                a_coreView.DrawText(a_player.m_velocity.ToString(), a_coreView.Fonts.Georgia, new Vector2(70.0f, 70.0f), Color.Red);
                DrawBounds(a_coreView, pos, a_camera.m_zoom);
            }
            else
            {
                float width = Model.Player.WIDTH * a_camera.m_zoom;
                float height = Model.Player.HEIGHT * a_camera.m_zoom;

                Rectangle dest = new Rectangle((int)pos.X, (int)pos.Y, (int)width, (int)height);
                a_coreView.Draw(a_coreView.Textures.Player, dest, new Rectangle(0, 0, 70, 130), Color.White);
            }
        }

        private void DrawBounds(Core a_coreView, Vector2 a_pos, int a_scale)
        {
            Vector2 topLeft = new Vector2(a_pos.X, a_pos.Y);
            Vector2 topRight = new Vector2(a_pos.X + Model.Player.WIDTH * a_scale, a_pos.Y);
            Vector2 bottomRight = new Vector2(a_pos.X + Model.Player.WIDTH * a_scale, a_pos.Y + Model.Player.HEIGHT * a_scale);
            Vector2 bottomLeft = new Vector2(a_pos.X, a_pos.Y + Model.Player.HEIGHT * a_scale);

            Helpers.RectangleRenderer rec = new Helpers.RectangleRenderer(a_coreView.m_device, 1.0f);
            rec.Begin();

            rec.Batch(
                topLeft,
                topRight,
                bottomRight,
                bottomLeft,
                Color.RoyalBlue,
                1.0f);

            rec.End();

            Helpers.PointBatch point = new Helpers.PointBatch(a_coreView.m_device, 1.0f, 5);
            point.Begin();

            point.Batch(topLeft, Color.Yellow);
            point.Batch(topRight, Color.Yellow);
            point.Batch(bottomRight, Color.Yellow);
            point.Batch(bottomLeft, Color.Yellow);

            point.Batch(new Vector2(topRight.X, topRight.Y + Model.Player.HEIGHT * a_scale / 2), Color.Violet);
            point.Batch(new Vector2(topLeft.X, topLeft.Y + Model.Player.HEIGHT * a_scale / 2), Color.Violet);
            point.Batch(new Vector2(topLeft.X + Model.Player.WIDTH * a_scale / 2, topLeft.Y), Color.Violet);
            point.Batch(new Vector2(topLeft.X + Model.Player.WIDTH * a_scale / 2, topLeft.Y + Model.Player.HEIGHT * a_scale), Color.Violet);

            point.Batch(new Vector2(topLeft.X + Model.Player.WIDTH * a_scale / 2, topLeft.Y + Model.Player.HEIGHT * a_scale / 2), Color.RoyalBlue);

            point.End();
        }

        public void Test(Core a_coreView)
        {
            Model.Player p = new Model.Player();
            p.m_pos.X = 200;
            p.m_pos.Y = 550;

            DrawPlayer(a_coreView, p, null);
        }
    }
}
