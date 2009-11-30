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
        Views.Camera m_camera;

        #region Constructors

        public PlayGame(Views.Core a_coreView, GraphicsDevice a_device)
            : base(a_coreView)
        {
            m_camera = new Views.Camera();
        }

        #endregion

        #region Methods

        public override bool DoControll(Model.Game a_game, float a_elapsedTime, IModel a_model, int a_width, int a_height)
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
            m_camera.m_movement = cameraMovement;

            if (m_coreView.Input.IsKeyPressed(Buttons.RightShoulder))
            {
                m_camera.m_zoom += 1;
            }

            if (m_coreView.Input.IsKeyPressed(Buttons.LeftShoulder))
            {
                m_camera.m_zoom -= 1;
                if (m_camera.m_zoom < 1)
                {
                    m_camera.m_zoom = 1;
                }
            }

            if (m_coreView.Input.IsKeyPressed(Buttons.RightStick))
            {
                m_camera.m_pos = Vector2.Zero;
                m_camera.m_zoom = 48;
            }

            if (m_coreView.Input.IsKeyJustPressed(Buttons.B))
            {
                m_coreView.Sounds.TestSound.Play();
            }

            float movement = 0;
            movement = m_coreView.Input.GetLeftThumbStick().X;

            bool isJumping = m_coreView.Input.IsKeyPressed(Buttons.A);

            a_model.MovePlayer(movement, isJumping);

            m_camera.UpdateCamera(a_elapsedTime, a_width, a_height);
            m_camera.m_pos = a_game.m_player.m_pos;

            m_gameView.Draw(a_game, a_elapsedTime, m_camera);

            return true;
        }

        #endregion
    }
}
