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
            /*Vector2 gravity = new Vector2(0, 9.82f);

            m_player.m_velocity += gravity; //* a_elapsedTime;
            Vector2 newPos = m_player.m_pos + m_player.m_velocity * a_elapsedTime;
            Console.WriteLine(m_level.GetCollision((int)m_player.m_pos.X, (int)m_player.m_pos.Y));
            m_player.m_isOnGround = false;
            m_player.m_pos = Collide(m_player.m_pos, newPos, new Vector2(1, 1), ref m_player.m_velocity, ref m_player.m_isOnGround);*/

            m_player.UpdateVelocity(a_elapsedTime);
            Vector2 newPos = m_player.CalculateNewPosition(a_elapsedTime);

            m_player.m_pos = Collide(m_player.m_pos, newPos, new Vector2(1.0f), ref m_player.m_velocity, ref m_player.m_isOnGround);
            //HandleCollisions();
        }

        public Vector2 Collide(Vector2 a_oldPos, Vector2 a_newPos, Vector2 a_size, ref Vector2 a_outVelocity, ref bool a_outIsOnGround)
        {
            int left = (int)Math.Round(a_newPos.X - m_player.WIDTH / 2);
            int top = (int)Math.Round(a_newPos.Y - m_player.HEIGHT);
            Rectangle newPosBounds = new Rectangle(left, top, m_player.WIDTH, m_player.HEIGHT);

            int leftTile = (int)Math.Floor((float)newPosBounds.Left / Tile.WIDTH);
            int rightTile = (int)Math.Ceiling(((float)newPosBounds.Right / Tile.WIDTH)) - 1;
            int topTile = (int)Math.Floor((float)newPosBounds.Top / Tile.HEIGHT);
            int bottomTile = (int)Math.Ceiling(((float)newPosBounds.Bottom / Tile.HEIGHT)) - 1;

            m_player.m_isOnGround = false;

            for (int y = topTile; y <= bottomTile; ++y)
            {
                for (int x = leftTile; x <= rightTile; ++x)
                {
                    Tile.TileType collision = m_level.GetCollision(x, y);
                    if (collision == Tile.TileType.Solid)
                    {
                        // Try x movement
                        Vector2 xMove = new Vector2(a_newPos.X, a_oldPos.Y);
                        if (m_level.GetCollision((int)xMove.X, (int)xMove.Y) == Tile.TileType.Solid)
                        {
                            a_outVelocity.Y = 0.0f;
                            a_outIsOnGround = true;

                            //return xMove;
                            return a_oldPos;
                        }

                        // Try y movement
                        Vector2 yMove = new Vector2(a_oldPos.X, a_newPos.Y);
                        if (m_level.GetCollision((int)yMove.X, (int)yMove.Y) == Tile.TileType.Solid)
                        {
                            a_outVelocity.X = 0.0f;

                            //return yMove;
                            return a_oldPos;
                        }

                        return a_oldPos;
                    }
                }
            }
            
            return a_newPos;
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
