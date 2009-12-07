using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace MeCloidGame.Controllers
{
    class Editor : ControllerBase
    {
        private Views.Camera m_camera;

        private Helpers.MouseInput m_mouse;

        public Editor(Views.Core a_coreView)
            : base(a_coreView)
        {
            m_camera = new Views.Camera();
            m_camera.m_pos = new Vector2(Model.Level.WIDTH / 2, Model.Level.HEIGHT / 2);
            m_camera.m_zoom = 15;

            m_mouse = new Helpers.MouseInput();
        }

        public override bool DoControll(Model.Game a_game, float a_elapsedTime, IModel a_model, int a_width, int a_height)
        {
            Vector2 cameraMovement = Vector2.Zero;
            cameraMovement.X = m_coreView.Input.GetRightThumbStick().X;
            cameraMovement.Y = -m_coreView.Input.GetRightThumbStick().Y;
            m_camera.m_movement = cameraMovement;

            m_camera.UpdateCamera(a_elapsedTime, a_width, a_height);

            m_mouse.Update(Vector2.Zero, new Vector2(a_width, a_height), a_elapsedTime);

            //Vector2 logMPos = new Vector2((int)(m_mouse.Pos.X / m_camera.m_zoom - 19), (int)(m_mouse.Pos.Y / m_camera.m_zoom));

            Vector2 logMPos = (m_mouse.Pos + m_camera.TopLeft) / m_camera.m_zoom;

            Model.Tile.TileType thisType = a_game.m_level.GetCollision((int)logMPos.X, (int)logMPos.Y);

            if (thisType != Model.Tile.TileType.Clear && m_mouse.IsDownRMB() && (int)logMPos.X >= 0 && (int)logMPos.X < a_game.m_level.Width && (int)logMPos.Y >= 0 && (int)logMPos.Y < a_game.m_level.Height)
            {
                a_game.m_level.Tiles[(int)logMPos.X, (int)logMPos.Y].Type = Model.Tile.TileType.Clear;
            }

            if (thisType == Model.Tile.TileType.Clear && m_mouse.IsDownLMB() && (int)logMPos.X >= 0 && (int)logMPos.X < a_game.m_level.Width && (int)logMPos.Y >= 0 && (int)logMPos.Y < a_game.m_level.Height)
            {
                a_game.m_level.Tiles[(int)logMPos.X, (int)logMPos.Y].Type = Model.Tile.TileType.Solid;
            }

            m_editView.Draw(a_game, a_elapsedTime, m_camera, m_mouse);

            return true;
        }
    }
}
