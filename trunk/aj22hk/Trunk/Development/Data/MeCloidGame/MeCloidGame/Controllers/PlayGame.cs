using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Microsoft.Xna.Framework.Input;

namespace MeCloidGame.Controllers
{
    class PlayGame : Controllers.ControllerBase
    {
        // TODO: Break out input to a separate function.
        #region Constructors

        public PlayGame(Views.Core a_coreView)
            : base(a_coreView)
        {
        }

        #endregion

        #region Methods

        public override bool DoControll(Model.Game a_game, float a_elapsedTime, IModel a_model)
        {
            if (m_coreView.Input.IsKeyJustPressed(Buttons.B))
            {
                m_coreView.Sounds.TestSound.Play();
            }

            float movement = 0;
            movement = m_coreView.Input.GetLeftThumbStick().X;

            if (m_coreView.Input.IsKeyPressed(Buttons.DPadRight))
            {
                movement = 1.0f;
            }
            else if (m_coreView.Input.IsKeyPressed(Buttons.DPadLeft))
            {
                movement = -1.0f;
            }

            bool isJumping = m_coreView.Input.IsKeyPressed(Buttons.A);

            a_model.MovePlayer(movement, isJumping);

            if (Helpers.Settings.Debug)
            {
                DrawGrid(a_game, a_elapsedTime);
            }

            m_gameView.Draw(a_game, 1.0f);

            return true;
        }

        private void DrawGrid(Model.Game a_game, float a_elapsedTime)
        {
            Helpers.PointBatch gridCenters = new Helpers.PointBatch(m_coreView.m_device, 0.8f, 3);
            gridCenters.Begin();

            for (int x = 0; x < 20; ++x)
            {
                for (int y = 0; y < 15; ++y)
                {
                    gridCenters.Batch(new Vector2(x * Model.Tile.WIDTH + Model.Tile.WIDTH / 2, y * Model.Tile.HEIGHT + Model.Tile.HEIGHT / 2), Color.Green);
                }
            }

            gridCenters.End();

            Helpers.LineBatch line = new Helpers.LineBatch(m_coreView.m_device, 0.4f);
            line.Begin();

            for (int x = 0; x < 1280; x += 64)
            {
                line.Batch(new Vector2(x, 0), new Vector2(x, 720), Color.Red, 1.0f);
            }

            for (int y = 0; y < 720; y += 48)
            {
                line.Batch(new Vector2(0, y), new Vector2(1280, y), Color.Red, 1.0f);
            }

            line.End();

            Rectangle bounds = a_game.m_player.BoundingRectangle;
            Helpers.PointBatch point = new Helpers.PointBatch(m_coreView.m_device, 1.0f, 5);
            point.Begin();

            point.Batch(a_game.m_player.m_pos, Color.Yellow);
            point.Batch(new Vector2(a_game.m_player.m_pos.X, a_game.m_player.m_pos.Y - a_game.m_player.HEIGHT), Color.Yellow);
            point.Batch(new Vector2(a_game.m_player.m_pos.X + a_game.m_player.WIDTH / 2, a_game.m_player.m_pos.Y - a_game.m_player.HEIGHT / 2), Color.Yellow);
            point.Batch(new Vector2(a_game.m_player.m_pos.X - a_game.m_player.WIDTH / 2, a_game.m_player.m_pos.Y - a_game.m_player.HEIGHT / 2), Color.Yellow);

            point.Batch(new Vector2(bounds.X, bounds.Y), Color.Violet);
            point.Batch(new Vector2(bounds.X + bounds.Width, bounds.Y), Color.Violet);
            point.Batch(new Vector2(bounds.X, bounds.Y + bounds.Height), Color.Violet);
            point.Batch(new Vector2(bounds.X + bounds.Width, bounds.Y + bounds.Height), Color.Violet);

            point.Batch(new Vector2(bounds.Center.X, bounds.Center.Y), Color.RoyalBlue);

            point.End();

            Helpers.LineBatch dirVec = new Helpers.LineBatch(m_coreView.m_device, 1.0f);
            dirVec.Begin();
            Vector2 newPos = a_game.m_player.m_pos + a_game.m_player.m_velocity * a_elapsedTime;
            newPos = new Vector2(newPos.X, newPos.Y - a_game.m_player.HEIGHT / 2);
            dirVec.Batch(new Vector2(a_game.m_player.m_pos.X, a_game.m_player.m_pos.Y - a_game.m_player.HEIGHT / 2), Color.Red, newPos, Color.Blue, 1.0f);
            dirVec.End();
        }

        #endregion
    }
}
