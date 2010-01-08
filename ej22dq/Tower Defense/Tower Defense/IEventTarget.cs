using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tower_Defense
{
    interface IEventTarget
    {
        void Attack(Vector2 a_from, Vector2 a_to, Model.Tower.Type a_type);
        void KilledEnemy(int a_index);
    }

    interface IModel
    {
        void BuyTower(Vector2 a_at, Model.Tower.Type a_type);
    }
}
