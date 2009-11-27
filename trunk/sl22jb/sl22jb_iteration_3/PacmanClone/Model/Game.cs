﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PacmanClone.Model
{
    class Game
    {
        public const float MAX_PLAYER_SPEED = 15.0f;
        public const float MAX_ENEMY_SPEED = 10.0f;
        public const int MAX_ENEMIES = 4;
        public const int MAX_DOTS = 500;

        public Character m_player;
        public Map[] m_map;
        public Enemy m_enemies;
        public Dot[] m_dots;

        public Game()
        {
            m_player = new Character(MAX_PLAYER_SPEED);
        }

        public float GetTime(GameTime a_gameTime)
        {
            return 1.0f * (float)a_gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;
        }

        public void Update(float a_gameTime)
        {
            //Uppdarar spelarposition
            DoMovement(a_gameTime);
        }

        
        public void DoMovement(float a_elapsedTime)
        {
            if(m_player.m_movement.X ==  -1.0f)
            {
                m_player.m_xPos -= 1;
            }

            if (m_player.m_movement.X == 1.0f)
            {
                m_player.m_xPos += 1;
            }

            if (m_player.m_movement.Y == -1.0f)
            {
                m_player.m_yPos += 1;
            }

            if (m_player.m_movement.Y == 1.0f)
            {
                m_player.m_yPos -= 1;
            }
        }
        
    }
}
