using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MeCloidGame.Helpers
{
    class MouseInput
    {
        public MouseState m_mouse;

        public Vector2 Pos;
        private bool m_lmbClicked = false;
        private bool m_lmbDown = false;
        private bool m_rmbClicked = false;
        private bool m_rmbDown = false;

        public MouseInput()
        {
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

        public void Update(Vector2 a_upperLeft, Vector2 a_lowerRight, float a_elapsedTime)
        {
            m_mouse = Mouse.GetState();

            Pos.X = m_mouse.X;
            Pos.Y = m_mouse.Y;

            HandleClick(ref m_lmbDown, ref m_lmbClicked, m_mouse.LeftButton);
            HandleClick(ref m_rmbDown, ref m_rmbClicked, m_mouse.RightButton);

            Pos.X = MathHelper.Clamp(Pos.X, a_upperLeft.X, a_lowerRight.X);
            Pos.Y = MathHelper.Clamp(Pos.Y, a_upperLeft.Y, a_lowerRight.Y);
        }
    }
}
