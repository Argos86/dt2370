using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacmanClone.Views
{
    class GameView
    {
        private Views.CharacterView m_characterView;
        private Views.Core m_core;

        public GameView(Views.Core a_core)
        {
            m_core = a_core;
            m_characterView = new Views.CharacterView();
        }

        public void Draw(Model.Game a_game, int scale)
        {
            m_characterView.DrawPlayer(m_core, a_game.m_player, scale);
        }
    }
}
