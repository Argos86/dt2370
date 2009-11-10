using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ZombieHoards.Controllers
{
    abstract class ControllerBase : IEventTarget
    {
        protected Views.GameView m_view;
        protected Views.IMGui m_gui;
        protected Views.Core m_core;
        protected int m_selectedCharacter = 0;

       protected ControllerBase(Views.Core a_core)
        {
            m_view = new Views.GameView(a_core);
            m_gui = new Views.IMGui();
            m_core = a_core;
        }


       public void Attack(Vector2 a_from, Vector2 a_to, bool a_isZombie)
       {
           m_view.Attack(a_from, a_to, a_isZombie);

       }


        /*
         * Returns false to quit current controller else true 
         */
       public abstract bool DoControl(ZombieHoards.Model.Game a_game, float a_elapsedTime, IModel a_model);
    }
}
