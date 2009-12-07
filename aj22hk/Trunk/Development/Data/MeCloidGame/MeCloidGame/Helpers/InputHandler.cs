using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Microsoft.Xna.Framework.Graphics;

namespace MeCloidGame.Helpers
{
    class InputHandler
    {
        #region Fields

        PlayerIndex m_playerIndex;

        GamePadState m_gamePadState;
        GamePadState m_prevGamePadState;

        KeyboardState m_keyboardState;
        KeyboardState m_prevKeyboardState;
        Dictionary<Buttons, Keys> m_keyboardMap;

        #endregion

        #region Constructors

        public InputHandler(PlayerIndex a_playerIndex)
            : this(a_playerIndex, null)
        {
        }

        public InputHandler(PlayerIndex a_playerIndex, Dictionary<Buttons, Keys> a_keyboardMap)
        {
            m_playerIndex = a_playerIndex;
            m_keyboardMap = a_keyboardMap;
        }

        #endregion

        #region Methods

        public void Update()
        {
            m_prevGamePadState = m_gamePadState;
            m_gamePadState = GamePad.GetState(m_playerIndex);

            //if (!m_gamePadState.IsConnected)
            //{
                m_prevKeyboardState = m_keyboardState;
                m_keyboardState = Keyboard.GetState(m_playerIndex);
            //}
        }

        public float GetLeftTrigger()
        {
            float value = 0.0f;

            if (m_gamePadState.IsConnected)
            {
                value = m_gamePadState.Triggers.Left;
            }
            else if (m_keyboardMap != null)
            {
                if (m_keyboardState.IsKeyDown(m_keyboardMap[Buttons.LeftTrigger]))
                {
                    value = 1;
                }
            }

            return value;
        }

        public float GetRightTrigger()
        {
            float value = 0.0f;

            if (m_gamePadState.IsConnected)
            {
                value = m_gamePadState.Triggers.Right;
            }
            else if (m_keyboardMap != null)
            {
                if (m_keyboardState.IsKeyDown(m_keyboardMap[Buttons.RightTrigger]))
                {
                    value = 1;
                }
            }

            return value;
        }

        public Vector2 GetLeftThumbStick()
        {
            Vector2 thumbStickPosition = Vector2.Zero;

            if (m_gamePadState.IsConnected)
            {
                thumbStickPosition = m_gamePadState.ThumbSticks.Left;
            }
            else if (m_keyboardMap != null)
            {
                if (m_keyboardState.IsKeyDown(m_keyboardMap[Buttons.LeftThumbstickUp]))
                {
                    thumbStickPosition.Y = 1;
                }
                else if (m_keyboardState.IsKeyDown(m_keyboardMap[Buttons.LeftThumbstickDown]))
                {
                    thumbStickPosition.Y = -1;
                }

                if (m_keyboardState.IsKeyDown(m_keyboardMap[Buttons.LeftThumbstickRight]))
                {
                    thumbStickPosition.X = 1;
                }
                else if (m_keyboardState.IsKeyDown(m_keyboardMap[Buttons.LeftThumbstickLeft]))
                {
                    thumbStickPosition.X = -1;
                }
            }
            
            return thumbStickPosition;
        }

        public Vector2 GetRightThumbStick()
        {
            Vector2 thumbStickPosition = Vector2.Zero;

            if (m_gamePadState.IsConnected)
            {
                thumbStickPosition = m_gamePadState.ThumbSticks.Right;
            }
            else if (m_keyboardMap != null)
            {
                if (m_keyboardState.IsKeyDown(m_keyboardMap[Buttons.RightThumbstickUp]))
                {
                    thumbStickPosition.Y = 1;
                }
                else if (m_keyboardState.IsKeyDown(m_keyboardMap[Buttons.RightThumbstickDown]))
                {
                    thumbStickPosition.Y = -1;
                }

                if (m_keyboardState.IsKeyDown(m_keyboardMap[Buttons.RightThumbstickRight]))
                {
                    thumbStickPosition.X = 1;
                }
                else if (m_keyboardState.IsKeyDown(m_keyboardMap[Buttons.RightThumbstickLeft]))
                {
                    thumbStickPosition.X = -1;
                }
            }

            return thumbStickPosition;
        }

        public bool IsKeyPressed(Buttons a_button)
        {
            bool pressed = false;

            if (m_gamePadState.IsConnected)
            {
                pressed = m_gamePadState.IsButtonDown(a_button);
            }
            else if (m_keyboardMap != null)
            {
                Keys key = m_keyboardMap[a_button];
                pressed = m_keyboardState.IsKeyDown(key);
            }

            return pressed;
        }

        public bool IsKeyJustPressed(Buttons a_button)
        {
            bool pressed = false;

            if (m_gamePadState.IsConnected)
            {
                pressed = (m_gamePadState.IsButtonDown(a_button) && m_prevGamePadState.IsButtonUp(a_button));
            }
            else if (m_keyboardMap != null)
            {
                Keys key = m_keyboardMap[a_button];
                pressed = (m_keyboardState.IsKeyDown(key) && m_prevKeyboardState.IsKeyUp(key));
            }

            return pressed;
        }

        public bool ToggleEdit()
        {
            if (m_keyboardState.IsKeyDown(Keys.F1) && !m_prevKeyboardState.IsKeyDown(Keys.F1))
            {
                return true;
            }

            return false;
        }

        public bool Save()
        {
            if (m_keyboardState.IsKeyDown(Keys.F2) && !m_prevKeyboardState.IsKeyDown(Keys.F2))
            {
                return true;
            }

            return false;
        }

        #endregion

        #region Test

        public bool Test(Views.Core a_coreView, PlayerIndex a_playerIndex)
        {
            string buttonJustPressed = String.Empty;
            string buttonBeingPressed = "none";
            float rightTrigger = 0;
            float leftTrigger = 0;
            Vector2 rightStick = Vector2.Zero;
            Vector2 leftStick = Vector2.Zero;

            Update();

            if (IsKeyJustPressed(Buttons.A))
            {
                buttonJustPressed = "A";
            }
            else if (IsKeyJustPressed(Buttons.B))
            {
                buttonJustPressed = "B";
            }
            else if (IsKeyJustPressed(Buttons.X))
            {
                buttonJustPressed = "X";
            }
            else if (IsKeyJustPressed(Buttons.Y))
            {
                buttonJustPressed = "Y";
            }
            else if (IsKeyJustPressed(Buttons.RightShoulder))
            {
                buttonJustPressed = "RightShoulder";
            }
            else if (IsKeyJustPressed(Buttons.LeftShoulder))
            {
                buttonJustPressed = "LeftShoulder";
            }
            else if (IsKeyJustPressed(Buttons.DPadDown))
            {
                buttonJustPressed = "DPadDown";
            }
            else if (IsKeyJustPressed(Buttons.DPadLeft))
            {
                buttonJustPressed = "DPadLeft";
            }
            else if (IsKeyJustPressed(Buttons.DPadRight))
            {
                buttonJustPressed = "DPadRight";
            }
            else if (IsKeyJustPressed(Buttons.DPadUp))
            {
                buttonJustPressed = "DPadUp";
            }
            else if (IsKeyJustPressed(Buttons.Start))
            {
                buttonJustPressed = "Start";
            }
            else if (IsKeyJustPressed(Buttons.Back))
            {
                buttonJustPressed = "Back";
            }
            else if (IsKeyJustPressed(Buttons.RightStick))
            {
                buttonJustPressed = "RightStick";
            }
            else if (IsKeyJustPressed(Buttons.LeftStick))
            {
                buttonJustPressed = "LeftStick";
            }

            if (IsKeyPressed(Buttons.A))
            {
                buttonBeingPressed = "A";
            }
            else if (IsKeyPressed(Buttons.B))
            {
                buttonBeingPressed = "B";
            }
            else if (IsKeyPressed(Buttons.X))
            {
                buttonBeingPressed = "X";
            }
            else if (IsKeyPressed(Buttons.Y))
            {
                buttonBeingPressed = "Y";
            }
            else if (IsKeyPressed(Buttons.RightShoulder))
            {
                buttonBeingPressed = "RightShoulder";
            }
            else if (IsKeyPressed(Buttons.LeftShoulder))
            {
                buttonBeingPressed = "LeftShoulder";
            }
            else if (IsKeyPressed(Buttons.DPadDown))
            {
                buttonBeingPressed = "DPadDown";
            }
            else if (IsKeyPressed(Buttons.DPadLeft))
            {
                buttonBeingPressed = "DPadLeft";
            }
            else if (IsKeyPressed(Buttons.DPadRight))
            {
                buttonBeingPressed = "DPadRight";
            }
            else if (IsKeyPressed(Buttons.DPadUp))
            {
                buttonBeingPressed = "DPadUp";
            }
            else if (IsKeyPressed(Buttons.Start))
            {
                buttonBeingPressed = "Start";
            }
            else if (IsKeyPressed(Buttons.Back))
            {
                buttonBeingPressed = "Back";
            }
            else if (IsKeyPressed(Buttons.RightStick))
            {
                buttonBeingPressed = "RightStick";
            }
            else if (IsKeyPressed(Buttons.LeftStick))
            {
                buttonBeingPressed = "LeftStick";
            }

            if (GetRightTrigger() != 0)
            {
                rightTrigger = GetRightTrigger();
            }

            if (GetLeftTrigger() != 0)
            {
                leftTrigger = GetLeftTrigger();
            }
            
            if (GetRightThumbStick() != Vector2.Zero)
            {
                rightStick = GetRightThumbStick();
            }

            if (GetLeftThumbStick() != Vector2.Zero)
            {
                leftStick = GetLeftThumbStick();
            }

            a_coreView.DrawText(String.Format("Button just pressed: {0}", buttonJustPressed), a_coreView.Fonts.Georgia, new Vector2(60.0f, 420.0f), Color.White);
            a_coreView.DrawText(String.Format("Button being pressed: {0}", buttonBeingPressed), a_coreView.Fonts.Georgia, new Vector2(60.0f, 440.0f), Color.White);
            a_coreView.DrawText(String.Format("Right trigger: {0}", rightTrigger), a_coreView.Fonts.Georgia, new Vector2(60.0f, 460.0f), Color.White);
            a_coreView.DrawText(String.Format("Left trigger: {0}", leftTrigger), a_coreView.Fonts.Georgia, new Vector2(60.0f, 480.0f), Color.White);
            a_coreView.DrawText(String.Format("Right stick: {0}", rightStick), a_coreView.Fonts.Georgia, new Vector2(60.0f, 500.0f), Color.White);
            a_coreView.DrawText(String.Format("Left stick: {0}", leftStick), a_coreView.Fonts.Georgia, new Vector2(60.0f, 520.0f), Color.White);

            return true;
        }

        #endregion
    }
}
