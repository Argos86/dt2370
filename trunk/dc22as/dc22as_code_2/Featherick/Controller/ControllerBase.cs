using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Featherick.Controller
{
    abstract class ControllerBase : IEventTarget
    {
        protected View.GameView m_view;
        protected View.Core m_core;
        protected int m_selectedCharacter = 0;

        protected ControllerBase(View.Core a_core)
        {
            m_view = new View.GameView(a_core);
            m_core = a_core;
        }


        public void Attack(Vector2 a_from, Vector2 a_to, bool a_isEnemy)
        {
            m_view.Attack(a_from, a_to, a_isEnemy);

        }


        /*
         * Returns false to quit current controller else true 
         */
        public abstract bool DoControl(Featherick.Model.Game a_game, float a_elapsedTime, IModel a_model);
    }
}
