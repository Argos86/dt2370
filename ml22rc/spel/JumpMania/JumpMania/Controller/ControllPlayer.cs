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

namespace JumpMania.Controller
{
    class ControllPlayer
    {
        public View.Input a_input;

        public ControllPlayer()
        {
            a_input = new JumpMania.View.Input();
        }

        public void Update(GameTime theGameTime, JumpMania.Model.Game a_game)
        {
            if (a_game.m_won == true)
            {
                a_game.LoadLevel(a_game.m_levelLoaded + 1);
            }
            else if (a_game.m_over == true)
            {
                a_game.LoadLevel(1);
            }
            else
            {
                UpdateGameRunning(theGameTime, a_game);
            }
        }

        private void UpdateGameRunning(GameTime theGameTime, JumpMania.Model.Game a_game)
        {
            a_input.Update();

            if (a_input.IsJumping(theGameTime, a_game.m_player.m_collideGround) == true)
            {
                a_game.m_player.m_velocity.Y = -16.0f; 
            }

            if (a_input.IsWalkingRight(theGameTime) == true)
            {
                a_game.m_player.m_movement = 1.0f;
                a_game.m_player.UpdateWalkRight(theGameTime);

            }
            else
            {
                a_game.m_player.m_velocity.X *= 0.95f;
            }

            if (a_input.IsWalkingLeft(theGameTime) == true)
            {
                a_game.m_player.m_movement = -1.0f;
                a_game.m_player.UpdateWalkLeft(theGameTime);
            }
            else
            {
                a_game.m_player.m_velocity.X *= 0.95f;
            }
            if (a_input.IsRestarting() == true) 
            {
                a_game.LoadLevel(1);
                a_game.m_over = false;
                a_game.m_won = false;

            }
        }
    }
}
