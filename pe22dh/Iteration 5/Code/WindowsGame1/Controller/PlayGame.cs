using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CombatLand.Controller
{
    class PlayGame:ControllerBase
    {
        public PlayGame(View.Core a_core)
            : base(a_core)
        {


        }

        public bool DoControlPlay(Model.Game a_game, float a_elapsedTime)
        {   
           
            m_core.m_input.Update(a_elapsedTime);
            m_core.m_camera.UpdateCamera(a_game, a_elapsedTime);


            a_game.m_player.SetMovement(m_core.m_input.GetMovement(), m_core.m_input.IsJumping());
            

            
            m_core.Begin();
            m_gameView.Draw(a_game);

            m_core.End();




            return true;
        }
        public bool DoControlMenu(Model.Game a_game, float a_elapsedTime)
        {

            m_core.m_input.Update(a_elapsedTime);

            if (m_core.m_input.IsJumping())
            {
                a_game.m_isRunning = true;
            }
            m_core.Begin();
            m_menuView.DrawMenu(a_game);
            m_core.End();

            return true;
        }

        public bool DoControlOverlay(Model.Game a_game, float a_elapsedTime)
        {

            m_core.m_input.Update(a_elapsedTime);

            if (m_core.m_input.IsJumping())
            {
                a_game.StartNewGame();
                m_core.m_camera.ResetCamera();
            }
            m_core.Begin();
            m_gameView.Draw(a_game);
            m_menuView.DrawOverlay(a_game);
            m_core.End();

            return true;
        }
    }
}
