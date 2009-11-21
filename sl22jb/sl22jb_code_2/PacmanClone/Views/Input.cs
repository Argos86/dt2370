using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PacmanClone.Views
{
    class Input
    {
        private PlayerIndex m_playerIndex;

        KeyboardState m_keyboardState;
        KeyboardState m_prevKeyboardState;

        public Input()
        {
            m_playerIndex = PlayerIndex.One;
        }

        public void Update(float a_elapsedTime)
        {
                m_prevKeyboardState = m_keyboardState;
                m_keyboardState = Keyboard.GetState(m_playerIndex);
        }

        public float GetMovement()
        {
            float x = 0.0f;
            //float y = 0.0f;

            if (m_keyboardState.IsKeyDown(Keys.Left))
            {
                x = -1.0f;
            }

            if (m_keyboardState.IsKeyDown(Keys.Right))
            {
                x = 1.0f;
            }

            /*
            if (m_keyboardState.IsKeyDown(Keys.Down))
            {
                y = -1.0f;
            }

            if (m_keyboardState.IsKeyDown(Keys.Up))
            {
                y = 1.0f;
            }
             */

            //TODO: Fixa så att y returneras också
            return x;
        }
    }
}
