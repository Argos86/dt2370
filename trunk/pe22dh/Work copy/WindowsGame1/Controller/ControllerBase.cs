using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombatLand.Controller
{
    abstract class ControllerBase
    {
        protected View.GameView m_gameView;
        protected View.MenuView m_menuView;
        protected View.Core m_core;

        protected ControllerBase(View.Core a_core)
        {
            m_gameView = new View.GameView(a_core);
            m_menuView = new View.MenuView(a_core);
            //m_gui = new Views.IMGui();
            m_core = a_core;
        }
    }
}
