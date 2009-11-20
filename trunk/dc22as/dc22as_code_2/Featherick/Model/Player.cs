using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Featherick.Model
{
    class Player
    {
         // Constants for controling horizontal movement
        private const float MoveAcceleration = 14000.0f;
        private const float MaxMoveSpeed = 2000.0f;
        private const float GroundDragFactor = 0.58f;
        private const float AirDragFactor = 0.65f;

        // Constants for controlling vertical movement
        private const float MaxJumpTime = 0.35f;
        private const float JumpLaunchVelocity = -4000.0f;
        private const float GravityAcceleration = 3500.0f;
        private const float MaxFallSpeed = 600.0f;
        private const float JumpControlPower = 0.14f;

        // Input configuration
        private const float MoveStickScale = 1.0f;
        private const Buttons JumpButton = Buttons.A;        

        public int m_health;
        public Vector2 m_pos;
        public float m_timer = 0.0f;
        private View.Input m_input;


        public Point m_frameSize = new Point(100, 100);
        public Point m_currentFrame = new Point(0, 0);
        public Point m_sheetSize = new Point(8, 1);

        public Rectangle m_playerRect;


        private float movement;

        // Jumping state
        public bool m_isJumping;
        public bool m_wasJumping;
        private float m_jumpTime;

        //movement state
        public bool m_walkRight;
        public bool m_walkLeft;
        
        public bool WalksToRight()
        {
            return m_walkRight;
        }

        public bool WalksToLeft()
        {
            return m_walkLeft;
        }

        public bool IsJumping()
        {
            return m_isJumping;
        }

        public bool WasJumping()
        {
            return m_wasJumping;
        }

        public bool IsAlive()
        {
            return m_health > 0;
        }

        public bool IsOnGround
        {
            get { return isOnGround; }
        }
        bool isOnGround;

        // Physics state
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        Vector2 position;

        private float previousBottom;

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        Vector2 velocity;

       // private Rectangle localBounds;


        
        public Player()
        {
            m_health = 3;
            m_pos = new Vector2(50, 3);
            m_timer = 0.0f;           
        }       



        public bool Update(float a_elapsedTime)
        {
            m_timer -= a_elapsedTime;
            if (IsAlive() == false)
            {
                return false;
            }

            return true;
        }
    }
}
