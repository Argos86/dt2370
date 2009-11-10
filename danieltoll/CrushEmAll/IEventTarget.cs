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
        void BuySoldier(Vector2 a_at);
        int AddCivilian(Vector2 a_at);
        void MoveSoldier(int a_index, Vector2 a_at);
        void MoveCivilian(int a_index, Vector2 a_at);
        void Draw(bool a_index, Vector2 a_at);
    }
}
