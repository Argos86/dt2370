using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Featherick.Controller
{
    class PlayGame : Controller.ControllerBase
    {
        public PlayGame(View.Core a_core)
            : base(a_core)
        {
        }

        public override bool DoControl(Featherick.Model.Game a_game, float a_elapsedTime, IModel a_model, int a_width, int a_height)
        {
            
            //Handle quit by esc or back button (XBOX)
            if (m_core.m_input.DoQuit())
            {
                return false;
            }


            m_view.Draw(a_game, a_elapsedTime, a_width, a_height);


            if (a_game.HasNotStarted())
            {
                //TODO: visa en meny
            }
             else if (a_game.IsGameOver())
            {                
                if (m_core.m_input.TriesToContinue())
                {
                    a_game.ResetGame();
                }
                else
                {
                    //TODO: fixa så att man kan trycka enter eller space för att starta om
                    Vector2 size = new Vector2(m_core.m_assets.m_LoseScreen.Width, m_core.m_assets.m_LoseScreen.Height);
                    m_core.Draw(m_core.m_assets.m_LoseScreen, new Vector2(a_width / 2 - size.X / 2, a_height / 2 - size.Y / 2), Color.White);
                }
            }
            else if (a_game.HasWon())
            {
                //TODO: ladda nästa bana
                if (m_core.m_input.TriesToContinue())
                {
                    a_game.ResetGame();
                }
                else
                {
                    //TODO: fixa så att man kan trycka enter eller space för att gå vidare
                    Vector2 size = new Vector2(m_core.m_assets.m_WinScreen.Width, m_core.m_assets.m_WinScreen.Height);
                    m_core.Draw(m_core.m_assets.m_WinScreen, new Vector2(a_width / 2 - size.X / 2, a_height / 2 - size.Y / 2), Color.White);
                }
            }
            else
            {
                ControlPlaying(a_game, a_elapsedTime);
            }
         
            return true;
        }

        private void ControlPlaying(Featherick.Model.Game a_game, float a_elapsedTime)
        {
            m_core.m_input.GetPlayerMovement();
            a_game.m_player.m_walkLeft = false;
            a_game.m_player.m_walkRight = false;
            a_game.m_player.m_isJumping = false;

            if (m_core.m_input.m_movement > 0)
            {
                a_game.m_player.WalkRight(a_elapsedTime);
            }
            if (m_core.m_input.m_movement < 0)
            {
                a_game.m_player.WalkLeft(a_elapsedTime);
            }
            if (m_core.m_input.TriesToJump())
            {
                a_game.m_player.DoJump();
            }
        }
    }
}
