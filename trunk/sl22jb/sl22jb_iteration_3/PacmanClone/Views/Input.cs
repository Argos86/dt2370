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

        public Vector2 GetMovement()
        {
            Vector2 Direction = new Vector2();

            if (m_keyboardState.IsKeyDown(Keys.Left))
            {
                Direction.X = -1.0f;
            }

            if (m_keyboardState.IsKeyDown(Keys.Right))
            {
                Direction.X = 1.0f;
            }

            
            if (m_keyboardState.IsKeyDown(Keys.Down))
            {
                Direction.Y = -1.0f;
            }

            if (m_keyboardState.IsKeyDown(Keys.Up))
            {
                Direction.Y = 1.0f;
            }
            
            return Direction;
        }
    }
}
