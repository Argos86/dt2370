using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace ZombieHoards
{
    interface IEventTarget
    {
        void Attack(Vector2 a_from, Vector2 a_to, bool a_isZombie);

    }

    interface IModel
    {
        
        void Draw(bool a_index, Vector2 a_at);
    }
}
