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
        
        public bool m_hasWon;
        public bool m_isRunning;
        public Game()
        {
            m_player = new Character(MAX_PLAYER_SPEED);
            m_map = new Map();
            m_map.BuildMap1();
            m_hasWon = false;
            m_isRunning = false;
        }
        public void StartNewGame()
        {
            m_player = new Character(MAX_PLAYER_SPEED);
            m_map = new Map();
            m_map.BuildMap1();
            m_hasWon = false;
            m_isRunning = false;
        }
        public float GetTimeSeconds(GameTime a_gameTime)
        {
            return 1.0f * (float)a_gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;
        }

        public void Update(float a_elapsedTime)
        {
            m_player.UpdateMovement(a_elapsedTime);
            Vector2 newPos = m_player.CalculateNewPosition(a_elapsedTime);

            m_player.m_isOnGround = false;
            m_player.m_pos = Collide(m_player.m_pos, newPos, m_player.m_size, ref m_player.m_velocity, ref m_player.m_isOnGround);
            this.isDying();
            this.isWinning();
        }

        public void isDying()
        {
            if (m_player.m_pos.Y > Model.Map.HEIGHT)
            {
                m_player.m_hitPoints = 0;
            }
        }

        public void isWinning()
        {
            if (m_map.IsCollidingAt(m_player.m_pos, m_player.m_size, Tile.TileType.Win))
            {
                m_hasWon = true;
            }
        }

        public Vector2 Collide(Vector2 a_oldPos, Vector2 a_newPos, Vector2 a_size, ref Vector2 a_outVelocity, ref bool a_outIsOnGround)
        {
            
            if (m_map.IsCollidingAt(a_newPos, a_size, Tile.TileType.Blocked))
            {
                // Try x movement
                Vector2 xMove = new Vector2(a_newPos.X, a_oldPos.Y);
                if (m_map.IsCollidingAt(xMove, a_size, Tile.TileType.Blocked) == false)
                {
                    a_outVelocity.Y = 0.0f;

                    if (a_newPos.Y > a_oldPos.Y)
                    {
                        xMove.Y = (int)(a_newPos.Y + a_size.Y) - a_size.Y;
                    }
                    
                    a_outIsOnGround = true;
                    return xMove;
                }

                // Try y movement
                Vector2 yMove = new Vector2(a_oldPos.X, a_newPos.Y);
                if (m_map.IsCollidingAt(yMove, a_size, Tile.TileType.Blocked) == false)
                {

                   
                    a_outVelocity.X = 0.0f;

                    return yMove;
                }

                if (a_newPos.Y > a_oldPos.Y)
                {
                    a_oldPos.Y = (int)(a_newPos.Y + a_size.Y) - a_size.Y;
                    a_outIsOnGround = true;
                }

                
                a_outVelocity = Vector2.Zero;
               
                return a_oldPos;
            }

            return a_newPos;
        }
    }
}
