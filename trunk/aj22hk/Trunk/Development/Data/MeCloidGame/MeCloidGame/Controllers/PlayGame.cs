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

        public override bool DoControll(Model.Game a_game)
        {
            if (m_coreView.Input.IsKeyJustPressed(Buttons.B))
            {
                m_coreView.Sounds.TestSound.Play();
            }

            a_game.m_player.m_pos += m_coreView.Input.GetLeftThumbStick();

            if (m_coreView.Input.IsKeyPressed(Buttons.DPadUp))
            {
                a_game.m_player.m_pos.Y -= 1;
            }
            else if (m_coreView.Input.IsKeyPressed(Buttons.DPadDown))
            {
                a_game.m_player.m_pos.Y += 1;
            }

            if (m_coreView.Input.IsKeyPressed(Buttons.DPadRight))
            {
                a_game.m_player.m_pos.X += 1;
            }
            else if (m_coreView.Input.IsKeyPressed(Buttons.DPadLeft))
            {
                a_game.m_player.m_pos.X -= 1;
            }

            m_gameView.Draw(a_game, 1.0f);

            return true;
        }

        #endregion
    }
}
