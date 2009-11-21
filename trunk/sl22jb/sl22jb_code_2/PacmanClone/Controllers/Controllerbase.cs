using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacmanClone.Controllers
{
    abstract class Controllerbase
    {
        protected Views.GameView m_view;
        protected Views.Core m_core;

        protected Controllerbase(Views.Core a_core)
        {
            m_view = new Views.GameView(a_core);
            m_core = a_core;
        }
    }
}
