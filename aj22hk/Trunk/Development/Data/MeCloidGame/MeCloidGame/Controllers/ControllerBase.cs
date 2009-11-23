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

        protected Views.GameView m_gameView;
        protected Views.Core m_coreView;

        #endregion

        #region Constructors

        protected ControllerBase(Views.Core a_coreView)
        {
            m_gameView = new Views.GameView(a_coreView);
            m_coreView = a_coreView;
        }

        #endregion

        #region Methods

        public abstract bool DoControll(Model.Game a_game, float a_elapsedTime, IModel a_model);

        #endregion
    }
}
