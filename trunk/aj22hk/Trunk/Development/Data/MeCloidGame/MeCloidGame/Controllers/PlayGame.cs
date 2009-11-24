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
            int scale = 16;
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

            m_gameView.Draw(a_game, scale);

            return true;
        }

        #endregion
    }
}
