using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Featherick.Controller
{
    class PlayGame : Controller.ControllerBase
    {

        public PlayGame(View.Core a_core)
            : base(a_core)
        {


        }

        public override bool DoControl(Featherick.Model.Game a_game, float a_elapsedTime, IModel a_model)
        {
            
            //Handle quit by esc or back button (XBOX)
            if (m_core.m_input.DoQuit())
            {
                return false;
            }

            m_view.Draw(a_game, a_elapsedTime);
            m_core.m_input.GetPlayerMovement();
            a_game.m_player.m_walkLeft = false;
            a_game.m_player.m_walkRight = false;
            a_game.m_player.m_isJumping = false;

            if (m_core.m_input.m_movement > 0)
            {
                a_game.m_player.m_velocity.X += +10.0f * a_elapsedTime;
                a_game.m_player.m_walkRight = true;
            }
            if (m_core.m_input.m_movement < 0)
            {                
                a_game.m_player.m_velocity.X += -10.0f * a_elapsedTime;
                a_game.m_player.m_walkLeft = true;
            }
            if (m_core.m_input.TriesToJump()) 
            {
                a_game.m_player.DoJump(); 
            }
         


            return true;
        }
    }
}
