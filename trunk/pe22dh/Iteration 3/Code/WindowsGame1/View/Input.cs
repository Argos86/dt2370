using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CombatLand.View
{
    class Input
    {
        //For gamepad
        private PlayerIndex m_playerIndex;
        private GamePadState m_gamePadState;
        private GamePadState m_prevGamePadState;
        //for keyboard (temp)
        KeyboardState m_keyboardState;
        KeyboardState m_prevKeyboardState;

        public Input()
        {
            m_playerIndex = PlayerIndex.One;
        }
        public void Update(float a_elapsedTime)
        {
            m_prevGamePadState = m_gamePadState;
            m_gamePadState = GamePad.GetState(m_playerIndex);

            if (!m_gamePadState.IsConnected)
            {
                m_prevKeyboardState = m_keyboardState;
                m_keyboardState = Keyboard.GetState();
            }
        }

        public float GetMovement()
        {
            float x = 0.0f;

            
            if (m_keyboardState.IsKeyDown(Keys.Left))
            {
                x = -1.0f;
            }
            else if (m_keyboardState.IsKeyDown(Keys.Right))
            {
                x = 1.0f;
            }
            
            else
            {
                x = m_gamePadState.ThumbSticks.Left.X;
            }

            return x;
        }

        public bool IsJumping()
        {
            if(m_prevGamePadState.IsButtonUp(Buttons.A) && m_gamePadState.IsButtonDown(Buttons.A))
            {
                return true;
            }
            else if (m_prevKeyboardState.IsKeyUp(Keys.Space) && m_keyboardState.IsKeyDown(Keys.Space))
            {
                return true;
            }

            return false;
        }


    }
}
