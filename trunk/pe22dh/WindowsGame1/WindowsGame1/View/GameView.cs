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
        private View.MapView m_mapView;

        public GameView(View.Core a_core)
        {
            m_core = a_core;
            m_charView = new CharacterView();
            m_mapView = new MapView();
        }
        public void Draw(CombatLand.Model.Game a_game, int scale)
        {
            
            m_mapView.DrawMap(a_game.m_map, m_core, scale);
            m_charView.DrawPlayer(m_core, a_game.m_player, scale);
        }
    }
}
