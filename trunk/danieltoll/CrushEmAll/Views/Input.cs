using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ZombieHoards.Views
{
    /* Denna klassen �r t�nkt som en f�renkling av input
     * P� PC styrs spelare 1 av musen
     * 
     * P� XBOX styrs spelare 1 av gamepad
     * 
     * Ut�t exponeras ett enklare interface med en update (m�ste nog ta time)
     * 
     */
    class Input
    {
        private MouseState m_mouse;
        public KeyboardState m_keyState;
        private GamePadState[] m_gamePads;


        //TODO: the following could be one controller...
        public Vector2 m_mousePos;
        private bool m_lmbClicked = false;
        private bool m_lmbDown = false;
        private bool m_rmbClicked = false;
        private bool m_rmbDown = false;




        public Input()
        {
            m_gamePads = new GamePadState[4];
        }


        private void HandleClick(ref Boolean a_down, ref Boolean a_clicked, ButtonState a_state, ButtonState a_otherState)
        {
            a_clicked = false;
            if (a_state == ButtonState.Pressed || a_otherState == ButtonState.Pressed)
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

        public void Update(float a_elapsedTime, Vector2 a_upperLeft, Vector2 a_lowerRight)
        {
            m_mouse = Mouse.GetState();
            m_keyState = Keyboard.GetState();
            
            

            for (int i = 0; i< 4; i++) {
                m_gamePads[i] = GamePad.GetState( (PlayerIndex)i);
            }
#if !XBOX
            m_mousePos.X = m_mouse.X;// m_gamePads[0].ThumbSticks.Left.X; //dessa �r mellan -1 och 1
            m_mousePos.Y = m_mouse.Y;// m_gamePads[0].ThumbSticks.Left.Y;
#endif
            if (m_gamePads[0].IsConnected)
            {
                float x = m_gamePads[0].ThumbSticks.Left.X;
                float y = m_gamePads[0].ThumbSticks.Left.Y;

                if (x < 0)
                {
                    x = -(x * x);
                }
                else
                {
                    x = x * x;
                }
                if (y < 0)
                {
                    y = -(y * y);
                }
                else
                {
                    y = y * y;
                }

                m_mousePos.X += x * (float)a_elapsedTime * 1000.0f;
                m_mousePos.Y -= y * (float)a_elapsedTime * 1000.0f;
            }

            HandleClick(ref m_lmbDown, ref m_lmbClicked, m_mouse.LeftButton, m_gamePads[0].Buttons.A);
            HandleClick(ref m_rmbDown, ref m_rmbClicked, m_mouse.RightButton, m_gamePads[0].Buttons.B);

            m_mousePos = ZombieHoards.Model.MathHelper.Clamp(a_upperLeft, a_lowerRight, m_mousePos);
            
    
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

        public bool IsClickedLMB()
        {
            if (m_lmbClicked)
            {
                m_lmbClicked = false;
                return true;
            }
            return false;
        }
        public bool IsClickedRMB()
        {
            return m_rmbClicked;
        }
        public bool IsDownLMB()
        {
            return m_lmbDown;
        }
        public bool IsDownRMB()
        {
            return m_rmbDown;
        }

        public bool Test(float a_elapsedTime, Vector2 a_upperLeft, Vector2 a_lowerRight)
        {
            Update(a_elapsedTime, a_upperLeft, a_lowerRight);

            return true;
        }
    }


}