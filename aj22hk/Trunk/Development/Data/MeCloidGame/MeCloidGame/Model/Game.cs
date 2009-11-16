using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace MeCloidGame.Model
{
    class Game : IModel
    {
        public Level m_level;
        public Player m_player;

        public IEventTarget m_view;

        public Game(IEventTarget a_view)
        {
            m_view = a_view;
            Initialize();
        }

        private void Initialize()
        {
            m_level = new Level("0.txt");
            m_player = new Player();
        }

        public bool Update(float a_elapsedTime)
        {
            m_player.m_pos += m_player.m_velocity * a_elapsedTime;
            if (m_player.m_pos.X < 0)
            {
                m_player.m_pos.X = 0;
            }
            if (m_player.m_pos.X >= 1280-95)
            {
                m_player.m_pos.X = 1280-95;
            }
            if (m_player.m_pos.Y < 0)
            {
                m_player.m_pos.Y = 0;
            }
            if (m_player.m_pos.Y >= 720 - 95)
            {
                m_player.m_pos.Y = 720 - 95;
            }

            return true;
        }
    }
}
