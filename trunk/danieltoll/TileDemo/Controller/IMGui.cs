using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TileDemo.Controller
{
    class IMGui
    {

        public SpriteFont m_font;

        public void LoadContent(ContentManager a_content)
        {
            m_font = a_content.Load<SpriteFont>("small");
            
        }

        public enum ButtonState {
            MouseNotOver,
            MouseOver,
            MouseOverLBPressed,
            MouseOverRBPressed,
            MouseOverLBClicked,
            MouseOverRBClicked
        };

        private ButtonState DoButton(Vector2 a_mouse, bool a_isClicked, Vector2 a_pos, Vector2 a_size, bool a_isActive) {

            Rectangle r = new Rectangle((int)a_pos.X, (int)a_pos.Y, (int)a_size.X, (int)a_size.Y);

            bool containsMouse = r.Contains((int)a_mouse.X, (int)a_mouse.Y);
            
            if (a_isActive == false) {
                if (containsMouse) {
                    return ButtonState.MouseOver;
                }
            } else {
                if (containsMouse == true) {

                    if (a_isClicked)
                    {
                        return ButtonState.MouseOverLBClicked;
                    } 
                    return ButtonState.MouseOver;
                }
            }
            return ButtonState.MouseNotOver;
        }

        public ButtonState DoButton(Vector2 a_mouse, bool a_isClicked, string a_text, Vector2 a_pos, bool a_isActive, bool a_isSelected, SpriteBatch a_batch)
        {

            Vector2 sizes = new Vector2(140, 25);
            ButtonState ret = DoButton(a_mouse, a_isClicked, a_pos, sizes, a_isActive);
            
            Color color = Color.White;

            if (a_isActive == false) {
                color = Color.Gray;
            } else {
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


            a_batch.DrawString(m_font, a_text, a_pos, color);
            //a_core.DoLabel((int)a_pos.X, (int)a_pos.Y, 140, 25, a_text, new FontAlign(FontAlign.HA.DT_CENTER, FontAlign.VA.DT_VCENTER));
            
            return ret;
        }

        
    }
}
