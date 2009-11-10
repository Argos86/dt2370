using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace ZombieHoards.Controllers
{
    class PlayGame : Controllers.ControllerBase
    {
        
        private Vector2 m_mousePathFrom;
        private Vector2 m_mousePathTo;
        private Model.AStar m_mouseSearcher;
        

        public PlayGame(Views.Core a_core) : base(a_core)
        {
            
            
        }


        

        public override bool DoControl(ZombieHoards.Model.Game a_game, float a_elapsedTime, IModel a_model)
        {
            
            int scale = 16;
            Vector2 logicalMousePos = m_view.GetLogicalMousePos(scale);
            //Handle input from soldier button
            Views.IMGui.ButtonState state = m_gui.DoButton(m_core, "Soldier", new Vector2(32, 16 * 32), true, m_view.m_state == Views.GameView.PlayState.BUY_SOLDIER);
            if (state == Views.IMGui.ButtonState.MouseOverLBClicked)
            {
                m_view.m_state = Views.GameView.PlayState.BUY_SOLDIER;
            }

            //Mouse move
            switch (m_view.m_state)
            {
                case Views.GameView.PlayState.MOVE:
                {
                    if (m_mouseSearcher == null || m_mousePathFrom != a_game.m_soldiers[m_selectedCharacter].m_pos || m_mousePathTo != logicalMousePos)
                    {
                        m_mousePathFrom = a_game.m_soldiers[m_selectedCharacter].m_pos;
                        m_mousePathTo = logicalMousePos;

                        m_mouseSearcher = new Model.AStar(a_game.m_map);
                        a_game.m_map.InitGetPath(a_game.m_soldiers[m_selectedCharacter].m_pos, logicalMousePos, ref m_mouseSearcher);

                    }
                    else
                    {
                        m_mouseSearcher.Update(100);
                    }
                    m_view.DrawPath(m_mouseSearcher.m_path, scale);
                    break;
                }
            }
            

            //Mouse clicked
            if (m_core.m_input.IsClickedLMB())
            {
                

                switch (m_view.m_state)
                {
                    case Views.GameView.PlayState.BUY_SOLDIER:
                    {
                        a_model.BuySoldier(logicalMousePos);
                        m_view.m_state = Views.GameView.PlayState.NONE;

                        break;
                    }
                    case Views.GameView.PlayState.NONE:
                    {
                        int i = a_game.GetSoldier(logicalMousePos);
                        if (i != -1)
                        {
                            m_view.m_state = Views.GameView.PlayState.MOVE;
                            m_selectedCharacter = i;
                        }
                        break;
                    }
                    case Views.GameView.PlayState.MOVE:
                    {
                        a_model.MoveSoldier(m_selectedCharacter, logicalMousePos);
                        m_view.m_state = Views.GameView.PlayState.NONE;
                        break;
                    }
                }
            }
            if (m_core.m_input.IsClickedRMB())
            {
                m_view.m_state = Views.GameView.PlayState.NONE;
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
