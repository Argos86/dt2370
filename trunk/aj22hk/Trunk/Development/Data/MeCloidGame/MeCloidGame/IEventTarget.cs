using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeCloidGame
{
    interface IEventTarget
    {
    }

    interface IModel
    {
        void MovePlayer(float a_movement, bool a_isJumping);
    }
}
