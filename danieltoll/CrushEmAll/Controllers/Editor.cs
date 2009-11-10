using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZombieHoards.Controllers
{
    class Editor : ControllerBase
    {
        

        public Editor(Views.Core a_core) : base(a_core)
        {

        }


        public override bool DoControl(ZombieHoards.Model.Game a_game, float a_elapsedTime, IModel a_model)
        {
            int scale = 16;
            Vector2 logicalMousePos = m_view.GetLogicalMousePos(scale);

            Views.IMGui.ButtonState state = m_gui.DoButton(m_core, "civilian", new Vector2(32, 16 * 32), true, m_view.m_state == Views.GameView.PlayState.BUY_CIVILIAN);
            if (state == Views.IMGui.ButtonState.MouseOverLBClicked)
            {
                m_view.m_state = Views.GameView.PlayState.BUY_CIVILIAN;
            }
            
            state = m_gui.DoButton(m_core, "draw", new Vector2(32+128, 16 * 32), true, m_view.m_state == Views.GameView.PlayState.DRAW);
            if (state == Views.IMGui.ButtonState.MouseOverLBClicked)
            {
                if (m_view.m_state == Views.GameView.PlayState.DRAW)
                {
                    m_view.m_state = Views.GameView.PlayState.NONE;
                }
                else
                {
                    m_view.m_state = Views.GameView.PlayState.DRAW;
                }
            }


            if (m_core.m_input.IsClickedLMB())
            {


                switch (m_view.m_state)
                {
                    case Views.GameView.PlayState.BUY_CIVILIAN:
                        {
                            a_model.AddCivilian(logicalMousePos);
                            m_view.m_state = Views.GameView.PlayState.NONE;

                            break;
                        }
                    case Views.GameView.PlayState.NONE:
                        {
                            int i = a_game.GetCivilian(logicalMousePos);
                            if (i != -1)
                            {
                                m_view.m_state = Views.GameView.PlayState.MOVE;
                                m_selectedCharacter = i;
                            }
                            break;
                        }
                    case Views.GameView.PlayState.MOVE:
                        {
                            a_model.MoveCivilian(m_selectedCharacter, logicalMousePos);
                            m_view.m_state = Views.GameView.PlayState.NONE;
                            break;
                        }
                    
                }
            }

            if (m_core.m_input.IsDownLMB())
            {


                switch (m_view.m_state)
                {
                    case Views.GameView.PlayState.DRAW:
                        {
                            a_model.Draw(true, logicalMousePos);
                            break;
                        }
                }
            }

            if (m_core.m_input.IsDownRMB())
            {

                switch (m_view.m_state)
                {
                    case Views.GameView.PlayState.DRAW:
                        {
                            a_model.Draw(false, logicalMousePos);
                            break;
                        }
                    
                }
            }

            if (m_core.m_input.IsClickedRMB())
            {

                switch (m_view.m_state)
                {
                    case Views.GameView.PlayState.DRAW:
                        {
                            
                            break;
                        }
                    default:
                        {
                            m_view.m_state = Views.GameView.PlayState.NONE;
                            break;
                        }
                }
            }

            //Handle output
            m_view.Draw(a_game, a_elapsedTime, scale, m_selectedCharacter);


            return true;
        }
    }
}
