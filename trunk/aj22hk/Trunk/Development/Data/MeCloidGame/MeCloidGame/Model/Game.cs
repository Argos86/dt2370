using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace MeCloidGame.Model
{
    class Game : IModel
    {
        // TODO: Rethink collision detection. Move away from Platformer Starter Kit...
        // TODO: Create functionality to switch levels.
        // TODO: Add enemies and bullets.
        public Level m_level;
        public Player m_player;

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

            m_player.m_pos = Collide(m_player.m_pos, newPos, new Vector2(1.0f), ref m_player.m_velocity, ref m_player.m_isOnGround);
        }

        public Vector2 Collide(Vector2 a_oldPos, Vector2 a_newPos, Vector2 a_size, ref Vector2 a_outVelocity, ref bool a_outIsOnGround)
        {
            if (m_level.IsCollidingAt(a_newPos, a_size))
            {
                // Try x movement
                Vector2 xMove = new Vector2(a_newPos.X, a_oldPos.Y);
                if (m_level.IsCollidingAt(xMove, a_size) == false)
                {
                    a_outVelocity.Y = 0.0f;

                    a_outIsOnGround = true;
                    return xMove;
                }

                // Try y movement
                Vector2 yMove = new Vector2(a_oldPos.X, a_newPos.Y);
                if (m_level.IsCollidingAt(yMove, a_size) == false)
                {
                    a_outVelocity.X = 0.0f;

                    return yMove;
                }

                return a_oldPos;
            }

            return a_newPos;
        }

        //public Vector2 Collide(Vector2 a_oldPos, Vector2 a_newPos, Vector2 a_size, ref Vector2 a_outVelocity, ref bool a_outIsOnGround)
        //{
        //    int left = (int)Math.Round(a_newPos.X - m_player.WIDTH / 2);
        //    int top = (int)Math.Round(a_newPos.Y - m_player.HEIGHT);
        //    Rectangle newPosBounds = new Rectangle(left, top, m_player.WIDTH, m_player.HEIGHT);

        //    int leftTile = (int)Math.Floor((float)newPosBounds.Left / Tile.WIDTH);
        //    int rightTile = (int)Math.Ceiling(((float)newPosBounds.Right / Tile.WIDTH)) - 1;
        //    int topTile = (int)Math.Floor((float)newPosBounds.Top / Tile.HEIGHT);
        //    int bottomTile = (int)Math.Ceiling(((float)newPosBounds.Bottom / Tile.HEIGHT)) - 1;

        //    m_player.m_isOnGround = false;

        //    for (int y = topTile; y <= bottomTile; ++y)
        //    {
        //        for (int x = leftTile; x <= rightTile; ++x)
        //        {
        //            Tile.TileType collision = m_level.GetCollision(x, y);
        //            if (collision == Tile.TileType.Solid)
        //            {
        //                // Try x movement
        //                Vector2 xMove = new Vector2(a_newPos.X, a_oldPos.Y);
        //                if (m_level.GetCollision((int)xMove.X, (int)xMove.Y) == Tile.TileType.Solid)
        //                {
        //                    a_outVelocity.Y = 0.0f;
        //                    a_outIsOnGround = true;

        //                    //return xMove;
        //                    return a_oldPos;
        //                }

        //                // Try y movement
        //                Vector2 yMove = new Vector2(a_oldPos.X, a_newPos.Y);
        //                if (m_level.GetCollision((int)yMove.X, (int)yMove.Y) == Tile.TileType.Solid)
        //                {
        //                    a_outVelocity.X = 0.0f;
                            
        //                    //return yMove;
        //                    return a_oldPos;
        //                }

        //                return a_oldPos;
        //            }
        //        }
        //    }
            
        //    return a_newPos;
        //}
    }
}
