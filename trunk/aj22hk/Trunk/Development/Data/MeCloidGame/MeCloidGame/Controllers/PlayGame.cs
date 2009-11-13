using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MeCloidGame.Controllers
{
    class PlayGame : Controllers.ControllerBase
    {
        #region Fields



        #endregion

        #region Constructors

        public PlayGame(Views.Core a_core)
            : base(a_core)
        {
        }

        #endregion

        #region Methods

        public override bool DoControll()
        {
            return true;
        }

        #endregion
    }
}
