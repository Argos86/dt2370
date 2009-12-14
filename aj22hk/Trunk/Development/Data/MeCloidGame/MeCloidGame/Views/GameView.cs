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
        private ParticleExplosion m_particleView;

        private Core m_coreView;

        public GameView(Core a_coreView)
        {
            m_coreView = a_coreView;
            m_levelView = new LevelView();
            m_playerView = new PlayerView();
            m_particleView = new ParticleExplosion();
        }

        public void Draw(Model.Game a_game, float a_elapsedTime, Camera a_camera)
        {
            m_playerView.DrawPlayer(m_coreView, a_game.m_player, a_camera);
            m_levelView.DrawLevel(a_game.m_level, m_coreView, a_camera);
            m_particleView.DrawParticles(m_coreView, a_camera, a_elapsedTime);
        }
    }
}
