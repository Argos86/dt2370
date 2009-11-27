using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Featherick.View
{
    class GameView
    {
        public enum PlayState
        {
            NONE,
            ATTACK,
            MOVE,
            DRAW
        };
        public PlayState m_state = PlayState.NONE;

        private View.PlayerView m_playerView;        
        private View.Core m_core;
        private View.LevelView m_levelView;        


        public GameView(View.Core a_core)
        {

            m_core = a_core;
            m_playerView = new View.PlayerView();
            m_levelView = new View.LevelView();
        }

        public void Attack(Vector2 a_from, Vector2 a_to, bool a_isEnemy)
        {

            if (a_isEnemy)
            {
                //m_core.m_sounds.m_zombieAttack.Play(0.1f, 0.0f, 0.0f);
                //m_effects.AddSplat(a_to);
            }
            else
            {
                //m_core.m_sounds.m_fireGun.Play(0.1f, 0.0f, 0.0f);
                //m_effects.AddShot(a_from, a_to);
            }
        }       

        public void Draw(Featherick.Model.Game a_game, float a_elapsedTime)
        {
            float scale = 16.0f;
            m_levelView.DrawLevel(a_game.m_level, m_core, scale);
            m_playerView.DrawPlayer(m_core, a_game.m_player.m_pos, a_game.m_player.m_currentFrame, a_game.m_player.m_frameSize, a_game.m_player, scale);
            m_playerView.PlayPlayerSound(m_core, a_game.m_player);
        }
    }
}
