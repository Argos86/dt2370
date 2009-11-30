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
        #region Constructors

        public PlayGame(Views.Core a_coreView)
            : base(a_coreView)
        {
        }

        #endregion

        #region Methods

        public override bool DoControll(Model.Game a_game, float a_elapsedTime, IModel a_model)
        {
            if (m_coreView.Input.IsKeyJustPressed(Buttons.Start))
            {
                m_coreView.m_graphics.ToggleFullScreen();
            }

            if (m_coreView.Input.IsKeyJustPressed(Buttons.Back))
            {
                if (Helpers.Settings.Debug)
                {
                    Helpers.Settings.Debug = false;
                }
                else
                {
                    Helpers.Settings.Debug = true;
                }
            }

            Vector2 cameraMovement = Vector2.Zero;
            cameraMovement.X = m_coreView.Input.GetRightThumbStick().X;
            cameraMovement.Y = -m_coreView.Input.GetRightThumbStick().Y;
            a_model.MoveCamera(cameraMovement);

            if (m_coreView.Input.IsKeyPressed(Buttons.RightShoulder))
            {
                a_game.m_camera.m_zoom += 1;
            }

            if (m_coreView.Input.IsKeyPressed(Buttons.LeftShoulder))
            {
                a_game.m_camera.m_zoom -= 1;
                if (a_game.m_camera.m_zoom < 1)
                {
                    a_game.m_camera.m_zoom = 1;
                }
            }

            if (m_coreView.Input.IsKeyPressed(Buttons.RightStick))
            {
                a_game.m_camera.m_pos = Vector2.Zero;
                a_game.m_camera.m_zoom = 48;
            }

            if (m_coreView.Input.IsKeyJustPressed(Buttons.B))
            {
                m_coreView.Sounds.TestSound.Play();
            }

            float movement = 0;
            movement = m_coreView.Input.GetLeftThumbStick().X;

            bool isJumping = m_coreView.Input.IsKeyPressed(Buttons.A);

            a_model.MovePlayer(movement, isJumping);

            m_gameView.Draw(a_game, a_elapsedTime);

            return true;
        }

        #endregion
    }
}
