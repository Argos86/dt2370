using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeCloidGame.Views
{
    class GameView
    {
        private LevelView m_levelView;
        private PlayerView m_playerView;

        private Core m_coreView;

        public GameView(Core a_coreView)
        {
            m_coreView = a_coreView;
            m_levelView = new LevelView();
            m_playerView = new PlayerView();
        }

        public void Draw(Model.Game a_game, float a_elapsedTime)
        {
            m_playerView.DrawPlayer(m_coreView, a_game.m_player, a_game.m_camera);
            m_levelView.DrawLevel(a_game.m_level, m_coreView, a_game.m_camera);
        }
    }
}
