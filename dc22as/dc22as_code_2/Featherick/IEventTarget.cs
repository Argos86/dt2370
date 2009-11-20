using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Featherick
{
    interface IEventTarget
    {
        //TODO: vilka argument behöver jag?
        void Attack(Vector2 a_from, Vector2 a_to, bool a_isEnemy);
    }
}
