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
        
        public MouseState m_mouse;

        public Vector2 m_mousePos;
        private bool m_lmbClicked = false;
        private bool m_lmbDown = false;
        private bool m_rmbClicked = false;
        private bool m_rmbDown = false;

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

            m_mouse = Mouse.GetState();

            m_mousePos.X = m_mouse.X;
            m_mousePos.Y = m_mouse.Y;

            HandleClick(ref m_lmbDown, ref m_lmbClicked, m_mouse.LeftButton);
            HandleClick(ref m_rmbDown, ref m_rmbClicked, m_mouse.RightButton);

            m_mousePos.X = MathHelper.Clamp(m_mousePos.X, 0, View.Core.BACK_BUFFER_WIDTH);
            m_mousePos.Y = MathHelper.Clamp(m_mousePos.Y, 0, View.Core.BACK_BUFFER_HEIGHT);
        }

        public float GetMovement()
        {
            float x = 0.0f;

            
            if (m_keyboardState.IsKeyDown(Keys.A))
            {
                x = -1.0f;
            }
            else if (m_keyboardState.IsKeyDown(Keys.D))
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
        

        private void HandleClick(ref bool a_down, ref bool a_clicked, ButtonState a_state)
        {
            a_clicked = false;
            if (a_state == ButtonState.Pressed)
            {
                a_down = true;
            }
            else
            {
                if (a_down)
                {
                    a_down = false;
                    a_clicked = true;
                }
            }
        }

        public bool IsClickedLMB()
        {
            return m_lmbClicked;
        }

        public bool IsDownLMB()
        {
            return m_lmbDown;
        }

        public bool IsClickedRMB()
        {
            return m_rmbClicked;
        }

        public bool IsDownRMB()
        {
            return m_rmbDown;
        }

    }
}
