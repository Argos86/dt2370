using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace MeCloidGame
{
    interface IEventTarget
    {
    }

    interface IModel
    {
        void MovePlayer(float a_movement, bool a_isJumping);
        void MoveCamera(Vector2 a_cameraMovement);
    }
}
