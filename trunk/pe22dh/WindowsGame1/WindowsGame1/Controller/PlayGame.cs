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

        public bool DoControl(Model.Game a_game, float a_elapsedTime)
        {
            m_core.m_input.Update(a_elapsedTime);



            a_game.m_player.SetMovement(m_core.m_input.GetMovement(), m_core.m_input.IsJumping());
            a_game.UpdatePlayer(a_elapsedTime);

            int scale = 48;
            m_core.Begin();
            m_view.Draw(a_game, scale);

            m_core.End();




            return true;
        }
    }
}
