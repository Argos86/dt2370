using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombatLand.Controller
{
    abstract class ControllerBase
    {
        protected View.GameView m_view;
        protected View.Core m_core;

        protected ControllerBase(View.Core a_core)
        {
            m_view = new View.GameView(a_core);
            //m_gui = new Views.IMGui();
            m_core = a_core;
        }
    }
}
