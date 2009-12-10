using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace JumpMania.View
{
    class Input
    {
        public KeyboardState PreviousKeyboardState;
        public KeyboardState CurrentState = Keyboard.GetState();

        public void Update()
        {
            PreviousKeyboardState = CurrentState; 
            CurrentState = Keyboard.GetState(); 
        }


        public bool IsJumping(GameTime theGameTime, bool m_collideGround)
        {
            if (CurrentState.IsKeyDown(Keys.Space) && PreviousKeyboardState.IsKeyDown(Keys.Space) == true && m_collideGround == true)
            {
                return true;
            }
            return false;
        }

        public bool IsWalkingRight(GameTime theGameTime)
        {
            if(CurrentState.IsKeyDown(Keys.Right))
                {
                    return true;
                }
            return false;
        }

        public bool IsWalkingLeft(GameTime theGameTime)
        {
            if (CurrentState.IsKeyDown(Keys.Left))
            {
                return true;
            }
            return false;
        }

        public bool IsRestarting()
        {
            if (CurrentState.IsKeyDown(Keys.Enter))
            {
                return true;
            }
            return false;
        }
    }
}
