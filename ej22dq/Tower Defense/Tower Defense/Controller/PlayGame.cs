using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense.Controller
{
    class PlayGame : Controller.ControllerBase
    {
        private Vector2 m_mousePathFrom;
        private Vector2 m_mousePathTo;
        private Model.AStar m_mouseSearcher;


        public PlayGame(View.Core a_core) : base(a_core)
        {

        }

        public override bool DoControl(Tower_Defense.Model.Game a_game, float a_elapsedTime, IModel a_model)
        {

            int scale = 10;
            
            //Use views to draw game... 
            m_view.Draw(a_game, a_elapsedTime, scale, m_selectedCharacter);
            
            Vector2 logicalMousePos = m_view.GetLogicalMousePos(scale);
            //Handle input from tower button
            View.IMGui.ButtonState state = m_gui.DoButton(m_core, "Tower", new Vector2(32, 700), true, m_view.m_state == View.GameView.PlayState.BUY_TOWER);
            if (state == View.IMGui.ButtonState.MouseOverLBClicked)
            {
                m_view.m_state = View.GameView.PlayState.BUY_TOWER;
            }
            m_core.DrawMouse();
            //Mouse clicked
            if (m_core.m_input.IsClickedLMB())
            {

                switch (m_view.m_state)
                {
                    case View.GameView.PlayState.BUY_TOWER:
                        {
                            a_model.BuyTower(logicalMousePos);
                            m_view.m_state = View.GameView.PlayState.NONE;

                            break;
                        }
                    case View.GameView.PlayState.NONE:
                        {
                            break;
                        }
                }
            }
            if (m_core.m_input.IsClickedRMB())
            {
                m_view.m_state = View.GameView.PlayState.NONE;
            }

            //Handle quit by esc or back button (XBOX)
            if (m_core.m_input.DoQuit())
            {
                return false;
            }

            return true;
        }

    }
}
