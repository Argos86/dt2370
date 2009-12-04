﻿using System;
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
        private Model.Tower.Type m_type;

        public PlayGame(View.Core a_core) : base(a_core)
        {

        }

        public override bool DoControl(Tower_Defense.Model.Game a_game, float a_elapsedTime, IModel a_model)
        {

            int scale = 800/Model.Map.HEIGHT;
            
            //Use views to draw game... 
            m_view.Draw(a_game, a_elapsedTime, scale, m_selectedTower, m_type);
            
            Vector2 logicalMousePos = m_view.GetLogicalMousePos(scale);
            //Handle input from tower button
            View.IMGui.ButtonState state = m_gui.DoButton(m_core, "Basic Tower", new Vector2(970, 110), a_game.m_cash >= Model.Tower.GetPrice(Model.Tower.Type.Normal), m_view.m_state == View.GameView.PlayState.BUY_TOWER && m_type == Model.Tower.Type.Normal);
            if (state == View.IMGui.ButtonState.MouseOverLBClicked)
            {
                m_view.m_state = View.GameView.PlayState.BUY_TOWER;

                m_type = Model.Tower.Type.Normal;
            }

            state = m_gui.DoButton(m_core, "Wind Tower", new Vector2(970, 140), a_game.m_cash >= Model.Tower.GetPrice(Model.Tower.Type.Wind), m_view.m_state == View.GameView.PlayState.BUY_TOWER && m_type == Model.Tower.Type.Wind);
            if (state == View.IMGui.ButtonState.MouseOverLBClicked)
            {
                m_view.m_state = View.GameView.PlayState.BUY_TOWER;

                m_type = Model.Tower.Type.Wind;
            }
            state = m_gui.DoButton(m_core, "Fire Tower", new Vector2(970, 170), a_game.m_cash >= Model.Tower.GetPrice(Model.Tower.Type.Fire), m_view.m_state == View.GameView.PlayState.BUY_TOWER && m_type == Model.Tower.Type.Fire);
            if (state == View.IMGui.ButtonState.MouseOverLBClicked)
            {
                m_view.m_state = View.GameView.PlayState.BUY_TOWER;

                m_type = Model.Tower.Type.Fire;
            }

            state = m_gui.DoButton(m_core, "Water Tower", new Vector2(970, 200), a_game.m_cash >= Model.Tower.GetPrice(Model.Tower.Type.Water), m_view.m_state == View.GameView.PlayState.BUY_TOWER && m_type == Model.Tower.Type.Water);
            if (state == View.IMGui.ButtonState.MouseOverLBClicked)
            {
                m_view.m_state = View.GameView.PlayState.BUY_TOWER;

                m_type = Model.Tower.Type.Water;
            }

            state = m_gui.DoButton(m_core, "Earth Tower", new Vector2(970, 230), a_game.m_cash >= Model.Tower.GetPrice(Model.Tower.Type.Earth), m_view.m_state == View.GameView.PlayState.BUY_TOWER && m_type == Model.Tower.Type.Earth);
            if (state == View.IMGui.ButtonState.MouseOverLBClicked)
            {
                m_view.m_state = View.GameView.PlayState.BUY_TOWER;

                m_type = Model.Tower.Type.Earth;
            }

            state = m_gui.DoButton(m_core, "Undead Tower", new Vector2(970, 260), a_game.m_cash >= Model.Tower.GetPrice(Model.Tower.Type.Undead), m_view.m_state == View.GameView.PlayState.BUY_TOWER && m_type == Model.Tower.Type.Undead);
            if (state == View.IMGui.ButtonState.MouseOverLBClicked)
            {
                m_view.m_state = View.GameView.PlayState.BUY_TOWER;

                m_type = Model.Tower.Type.Undead;
            }



            if (m_view.m_state == Tower_Defense.View.GameView.PlayState.UPGRADE_TOWER)
            {
                state = m_gui.DoButton(m_core, "Upgrade Range", new Vector2(970, 260+64), a_game.m_cash >= Model.Tower.GetPrice(Model.Tower.Type.Undead), m_view.m_state == View.GameView.PlayState.BUY_TOWER && m_type == Model.Tower.Type.Undead);
                if (state == View.IMGui.ButtonState.MouseOverLBClicked)
                {
                   
                }
            }


            m_core.DrawMouse();
            //Mouse clicked
            if (m_core.m_input.IsClickedLMB())
            {

                switch (m_view.m_state)
                {
                    case View.GameView.PlayState.BUY_TOWER:
                        {
                            a_model.BuyTower(logicalMousePos, m_type);
                            m_view.m_state = View.GameView.PlayState.NONE;

                            break;
                        }
                    case View.GameView.PlayState.NONE:
                        {
                            Model.Tower selected = a_game.GetTower(logicalMousePos);
                            if (selected != null)
                            {
                                m_view.m_state = View.GameView.PlayState.UPGRADE_TOWER;
                                m_selectedTower = selected;
                            }
                            break;
                        }
                    case View.GameView.PlayState.UPGRADE_TOWER:
                        {
                            m_view.m_state = View.GameView.PlayState.NONE;
                            m_selectedTower = null;

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
