using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MeCloidGame.Controllers
{
    abstract class ControllerBase : IEventTarget
    {
        #region Fields

        protected Views.Core m_core;

        #endregion

        #region Constructors

        protected ControllerBase(Views.Core a_core)
        {
            m_core = a_core;
        }

        #endregion

        #region Methods

        public abstract bool DoControll();

        #endregion
    }
}
