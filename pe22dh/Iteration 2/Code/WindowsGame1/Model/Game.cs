using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CombatLand.Model
{
    class Game
    {   
        public const int MAX_BULLETS = 300;
        public const int MAX_ENEMIES = 50;

        public const float MAX_PLAYER_SPEED = 10.0f;
        public const float MAX_ENEMY_SPEED = 5.0f;

        public Character m_player;
        public Character[] m_enemies;
        public Map m_map;
        public Bullet[] m_bullets;

        public Game()
        {
            m_player = new Character(MAX_PLAYER_SPEED);
        }
        public float GetTimeSeconds(GameTime a_gameTime)
        {
            return 1.0f * (float)a_gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;
        }

    }
}
