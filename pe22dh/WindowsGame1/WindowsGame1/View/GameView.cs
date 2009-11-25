using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CombatLand.View
{
    class GameView
    {
        private View.Core m_core;
        private View.CharacterView m_charView;

        public GameView(View.Core a_core)
        {
            m_core = a_core;
            m_charView = new CharacterView();
        }
        public void Draw(CombatLand.Model.Game a_game, int scale)
        {
            m_charView.DrawPlayer(m_core, a_game.m_player, scale);
        }
    }
}
