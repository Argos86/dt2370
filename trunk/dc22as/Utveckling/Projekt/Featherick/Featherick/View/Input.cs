using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Featherick.View
{
    class Input
    {
        private GamePadState[] m_gamePads;
        GamePadState m_gamePadState;
        GamePadState m_oldGamePadState;
        private KeyboardState m_keyboardState;
        private KeyboardState m_oldKeyboardState;

        public float m_movement;

        // Input configuration
        private const float MoveStickScale = 1.0f;

        // Jumping state
        private bool m_isJumping;
        private bool m_continue;


        public Input() 
        {
            m_gamePads = new GamePadState[4];
        }

        public bool TriesToContinue()
        {
            if ((m_gamePadState.IsButtonUp(Buttons.A) && m_oldGamePadState.IsButtonDown(Buttons.A)) ||
                (m_gamePadState.IsButtonUp(Buttons.Start) && m_oldGamePadState.IsButtonDown(Buttons.Start)) ||
                (m_keyboardState.IsKeyUp(Keys.Space) && m_oldKeyboardState.IsKeyDown(Keys.Space)) ||
                (m_keyboardState.IsKeyUp(Keys.Enter) && m_oldKeyboardState.IsKeyDown(Keys.Enter)))
            {
                m_continue = true;
                return m_continue;
            }
            return false;
        }

        public void GetPlayerMovement()
        {
            // Get analog horizontal movement.
            m_movement = m_gamePadState.ThumbSticks.Left.X * MoveStickScale;

            // Ignore small movements to prevent running in place.
            if (Math.Abs(m_movement) < 0.5f)
                m_movement = 0.0f;

            // If any digital horizontal movement input is found, override the analog movement.
            if (m_gamePadState.IsButtonDown(Buttons.DPadLeft) ||
                m_keyboardState.IsKeyDown(Keys.Left) ||
                m_keyboardState.IsKeyDown(Keys.A))
            {
                m_movement = -1.0f;
            }
            else if (m_gamePadState.IsButtonDown(Buttons.DPadRight) ||
                     m_keyboardState.IsKeyDown(Keys.Right) ||
                     m_keyboardState.IsKeyDown(Keys.D))
            {
                m_movement = 1.0f;
            }
        }

        public bool TriesToJump()
        {
            // Check if the player wants to jump.
            if (m_gamePadState.IsButtonDown(Buttons.A) ||
                m_keyboardState.IsKeyDown(Keys.Space) ||
                m_keyboardState.IsKeyDown(Keys.Up) ||
                m_keyboardState.IsKeyDown(Keys.W))
            {
                m_isJumping = true;
                return m_isJumping;
            }
            return false;
        }

        public void Update(float a_elapsedTime, Vector2 a_upperLeft, Vector2 a_lowerRight) 
        {
            // Get input state.
            m_oldKeyboardState = m_keyboardState;
            m_oldGamePadState = m_gamePadState;
            m_keyboardState = Keyboard.GetState();
            m_gamePadState = GamePad.GetState(PlayerIndex.One);
        }
        
        public bool DoQuit()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                return true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                return true;
            }
            return false;
        }     
    }
}
