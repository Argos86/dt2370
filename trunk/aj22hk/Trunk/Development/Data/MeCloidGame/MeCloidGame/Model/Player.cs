using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace MeCloidGame.Model
{
    class Player
    {
        // TODO: Add elapsed time everywhere
        // TODO: Add collision detection for player
        // TODO: Add physics for player
        public Vector2 m_pos;
        public Vector2 m_prevPos;
        public Vector2 m_velocity;
        public float speed = 20.0f;

        public Player()
        {
            m_pos = Vector2.Zero;
            m_prevPos = Vector2.Zero;
        }

        public bool MovePlayer(Vector2 a_velocity)
        {
            int tiles = 1280 / 64;
            speed = tiles / 0.1f;
            m_velocity = a_velocity * speed;
            return true;
        }


    }
}
