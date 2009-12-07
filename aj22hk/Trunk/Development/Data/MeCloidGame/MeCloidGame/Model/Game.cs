using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace MeCloidGame.Model
{
    class Game : IModel
    {
        // TODO: Create functionality to switch levels.
        // TODO: Add enemies and bullets.
        public Level m_level;
        public Player m_player;

        public Level[,] m_worldGrid;
        public Point m_currentLevel;

        public IEventTarget m_view;

        public Game(IEventTarget a_view)
        {
            m_view = a_view;
            Initialize();
        }

        private void Initialize()
        {
            m_level = new Level("0.txt");
            m_player = new Player();
        }

        #region Interface methods

        public void MovePlayer(float a_movement, bool a_isJumping)
        {
            m_player.m_movement = a_movement;
            m_player.m_isJumping = a_isJumping;
        }

        #endregion

        public bool Update(float a_elapsedTime)
        {
            UpdatePlayer(a_elapsedTime);

            return true;
        }

        public void UpdatePlayer(float a_elapsedTime)
        {
            m_player.UpdateVelocity(a_elapsedTime);

            Vector2 newPos = m_player.CalculateNewPosition(a_elapsedTime);
            
            m_player.m_isOnGround = false;
            m_player.m_pos = Collide(m_player.m_pos, newPos, ref m_player.m_velocity, ref m_player.m_isOnGround);
        }

        public Vector2 Collide(Vector2 a_oldPos, Vector2 a_newPos, ref Vector2 a_velocity, ref bool a_isOnGround)
        {
            if (m_level.IsCollidingAt(a_newPos))
            {
                // Try x movement
                Vector2 xMove = new Vector2(a_newPos.X, a_oldPos.Y);
                if (m_level.IsCollidingAt(xMove) == false)
                {
                    a_velocity.Y = 0.0f;

                    if (xMove.Y < a_newPos.Y)
                    {
                        xMove.Y = (int)(a_newPos.Y);
                        a_isOnGround = true;
                    }

                    return xMove;
                }

                // Try y movement
                Vector2 yMove = new Vector2(a_oldPos.X, a_newPos.Y);
                if (m_level.IsCollidingAt(yMove) == false)
                {
                    a_velocity.X = 0.0f;

                    return yMove;
                }

                a_velocity = Vector2.Zero;
                if (a_newPos.Y > a_oldPos.Y)
                {
                    a_oldPos.Y = (int)(a_newPos.Y);
                    a_isOnGround = true;
                }
                return a_oldPos;
            }

            return a_newPos;
        }
    }
}
