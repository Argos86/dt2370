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
        #region Fields



        #endregion

        #region Constructors

        public PlayGame(Views.Core a_coreView)
            : base(a_coreView)
        {
        }

        #endregion

        #region Methods

        public override bool DoControll(Model.Game a_game, float a_elapsedTime)
        {
            if (m_coreView.Input.IsKeyJustPressed(Buttons.B))
            {
                m_coreView.Sounds.TestSound.Play();
            }

            Vector2 velocity = Vector2.Zero;
            velocity.X += m_coreView.Input.GetLeftThumbStick().X;
            velocity.Y -= m_coreView.Input.GetLeftThumbStick().Y;

            if (m_coreView.Input.IsKeyPressed(Buttons.DPadUp))
            {
                velocity.Y -= 1;
            }
            else if (m_coreView.Input.IsKeyPressed(Buttons.DPadDown))
            {
                velocity.Y += 1;
            }

            if (m_coreView.Input.IsKeyPressed(Buttons.DPadRight))
            {
                velocity.X += 1;
            }
            else if (m_coreView.Input.IsKeyPressed(Buttons.DPadLeft))
            {
                velocity.X = -1;
            }

            a_game.m_player.UpdatePlayerVelocity(velocity);

            m_gameView.Draw(a_game, 1.0f);

            return true;
        }

        #endregion
    }
}
