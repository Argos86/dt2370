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
        private View.Camera m_camera;

        public GameView(View.Core a_core)
        {
            m_core = a_core;
            m_charView = new CharacterView();
            m_mapView = new MapView();
            m_camera = a_core.m_camera;
        }
        public void Draw(CombatLand.Model.Game a_game)
        {
            
            m_mapView.DrawMap(a_game.m_map, m_core, m_camera);
            m_charView.DrawPlayer(m_core, a_game.m_player, m_camera);
        }
    }
}
