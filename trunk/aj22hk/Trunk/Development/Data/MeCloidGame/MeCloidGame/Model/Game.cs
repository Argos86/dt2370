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
            m_player.UpdateVelocity();
            m_player.UpdatePosition();
            HandleCollisions();

            return true;
        }

        public void HandleCollisions()
        {
            Rectangle playerBounds = m_player.BoundingRectangle;
            int leftTile = (int)Math.Floor((float)playerBounds.Left / Tile.WIDTH);
            int rightTile = (int)Math.Ceiling(((float)playerBounds.Right / Tile.WIDTH)) - 1;
            int topTile = (int)Math.Floor((float)playerBounds.Top / Tile.HEIGHT);
            int bottomTile = (int)Math.Ceiling(((float)playerBounds.Bottom / Tile.HEIGHT)) - 1;

            m_player.m_isOnGround = false;

            for (int y = topTile; y <= bottomTile; ++y)
            {
                for (int x = leftTile; x <= rightTile; ++x)
                {
                    Tile.TileType collision = m_level.GetCollision(x, y);
                    if (collision != Tile.TileType.Clear)
                    {
                        Rectangle tileBounds = m_level.GetBounds(x, y);
                        Vector2 depth = GetIntersectionDepth(playerBounds, tileBounds);
                        if (depth != Vector2.Zero)
                        {
                            float absDepthX = Math.Abs(depth.X);
                            float absDepthY = Math.Abs(depth.Y);

                            if (absDepthY < absDepthX)
                            {
                                if (m_player.m_prevBottom <= tileBounds.Top)
                                {
                                    m_player.m_isOnGround = true;
                                }
                                if (collision == Tile.TileType.Solid || m_player.m_isOnGround)
                                {
                                    m_player.m_pos = new Vector2(m_player.m_pos.X, m_player.m_pos.Y + depth.Y);

                                    playerBounds = m_player.BoundingRectangle;
                                }
                            }
                            else
                            {
                                m_player.m_pos = new Vector2(m_player.m_pos.X + depth.X, m_player.m_pos.Y);

                                playerBounds = m_player.BoundingRectangle;
                            }
                        }
                    }
                }
            }

            m_player.m_prevBottom = playerBounds.Bottom;
        }

        private Vector2 GetIntersectionDepth(Rectangle a_rectA, Rectangle a_rectB)
        {
            // Calculate half sizes.
            float halfWidthA = a_rectA.Width / 2.0f;
            float halfHeightA = a_rectA.Height / 2.0f;
            float halfWidthB = a_rectB.Width / 2.0f;
            float halfHeightB = a_rectB.Height / 2.0f;

            // Calculate centers.
            Vector2 centerA = new Vector2(a_rectA.Left + halfWidthA, a_rectA.Top + halfHeightA);
            Vector2 centerB = new Vector2(a_rectB.Left + halfWidthB, a_rectB.Top + halfHeightB);

            // Calculate current and minimum-non-intersecting distances between centers.
            float distanceX = centerA.X - centerB.X;
            float distanceY = centerA.Y - centerB.Y;
            float minDistanceX = halfWidthA + halfWidthB;
            float minDistanceY = halfHeightA + halfHeightB;

            // If we are not intersecting at all, return (0, 0).
            if (Math.Abs(distanceX) >= minDistanceX || Math.Abs(distanceY) >= minDistanceY)
            {
                return Vector2.Zero;
            }

            // Calculate and return intersection depths.
            Vector2 depth;
            if (distanceX > 0)
            {
                depth.X = minDistanceX - distanceX;
            }
            else
            {
                depth.X = -minDistanceX - distanceX;
            }

            if (distanceY > 0)
            {
                depth.Y = minDistanceY - distanceY;
            }
            else
            {
                depth.Y = -minDistanceY - distanceY;
            }

            return depth;
        }
    }
}
