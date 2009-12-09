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


        private View.PlayerView m_playerView;        
        private View.Core m_core;
        private View.LevelView m_levelView;
        private View.Camera m_camera;


        public GameView(View.Core a_core)
        {

            m_core = a_core;
            m_playerView = new View.PlayerView();
            m_levelView = new View.LevelView();
            m_camera = new View.Camera();            
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

        public void Draw(Featherick.Model.Game a_game, float a_elapsedTime, int a_width, int a_height)
        {          
            m_camera.Update(a_game.m_player.m_pos, a_elapsedTime, a_width, a_height);
            
            m_playerView.DrawPlayer(m_core, a_game.m_player.m_pos, a_game.m_player.m_currentFrame, a_game.m_player.m_frameSize, a_game.m_player, m_camera);
            m_levelView.DrawLevel(a_game.m_level, m_core, m_camera);
        }
    }
}
