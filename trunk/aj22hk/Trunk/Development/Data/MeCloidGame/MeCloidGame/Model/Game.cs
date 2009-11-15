using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            return true;
        }
    }
}
