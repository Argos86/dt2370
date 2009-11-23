using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace ZombieHoards.Controllers
{
    class PlayGame : Controllers.ControllerBase
    {
        
        public PlayGame(Views.Core a_core) : base(a_core)
        {
            
            
        }


        

        public override bool DoControl(ZombieHoards.Model.Game a_game, float a_elapsedTime, IModel a_model)
        {
            
            int scale = 16;
            Vector2 logicalMousePos = m_view.GetLogicalMousePos(scale);
            
            
            
            

            //TODO: Some of this should be moved into character
            //Mouse clicked
            if (m_core.m_input.m_keyState.IsKeyDown(Keys.Left))
            {
                a_game.m_player.m_velocity.X += -10.0f * a_elapsedTime;
            }
            if (m_core.m_input.m_keyState.IsKeyDown(Keys.Right))
            {
                a_game.m_player.m_velocity.X += +10.0f * a_elapsedTime;
            }
            if (m_core.m_input.m_keyState.IsKeyDown(Keys.Up) && a_game.m_player.m_collideGround)
            {

                a_game.m_player.m_velocity.Y = -12.0f;
            }

            //Handle quit by esc or back button (XBOX)
            if (m_core.m_input.DoQuit())
            {
                return false;
            }


            //Use views to draw game... Note that this is done over the button...
            m_view.Draw(a_game, a_elapsedTime, scale, m_selectedCharacter );

            return true;
        }
    }
}
