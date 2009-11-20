using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense.View
{
    class IMGui
    {
        public enum ButtonState
        {
            MouseNotOver,
            MouseOver,
            MouseOverLBPressed,
            MouseOverRBPressed,
            MouseOverLBClicked,
            MouseOverRBClicked
        };

        private ButtonState DoButton(Core a_core, Vector2 a_pos, Vector2 a_size, bool a_isActive)
        {

            Rectangle r = new Rectangle((int)a_pos.X, (int)a_pos.Y, (int)a_size.X, (int)a_size.Y);

            bool containsMouse = r.Contains((int)a_core.m_input.m_mousePos.X, (int)a_core.m_input.m_mousePos.Y);

            if (a_isActive == false)
            {
                if (containsMouse)
                {
                    return ButtonState.MouseOver;
                }
            }
            else
            {
                if (containsMouse == true)
                {

                    if (a_core.m_input.IsClickedLMB())
                    {
                        return ButtonState.MouseOverLBClicked;
                    }
                    else if (a_core.m_input.IsClickedRMB())
                    {
                        return ButtonState.MouseOverRBClicked;
                    }
                    else if (a_core.m_input.IsDownLMB())
                    {
                        return ButtonState.MouseOverLBPressed;
                    }
                    else if (a_core.m_input.IsDownRMB())
                    {
                        return ButtonState.MouseOverRBPressed;
                    }
                    return ButtonState.MouseOver;
                }
            }
            return ButtonState.MouseNotOver;
        }

        public ButtonState DoButton(Core a_core, string a_text, Vector2 a_pos, bool a_isActive, bool a_isSelected)
        {

            Vector2 sizes = a_core.m_fonts.m_baseFont.MeasureString(a_text);
            ButtonState ret = DoButton(a_core, a_pos, sizes, a_isActive);

            Color color = Color.White;

            if (a_isActive == false)
            {
                color = Color.Gray;
            }
            else
            {
                switch (ret)
                {
                    case ButtonState.MouseNotOver: color = Color.LightGray; break;
                    case ButtonState.MouseOver: color = Color.White; break;
                    case ButtonState.MouseOverLBPressed: color = Color.Gray; break;
                    case ButtonState.MouseOverRBPressed: color = Color.Gray; break;
                    case ButtonState.MouseOverLBClicked: color = Color.White; break;
                    case ButtonState.MouseOverRBClicked: color = Color.White; break;
                }
            }



            a_core.DrawText(a_text, a_core.m_fonts.m_baseFont, a_pos, color);

            return ret;
        }
    }
}
