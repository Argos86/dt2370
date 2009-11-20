using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tower_Defense.Model
{
    class Wave
    {
        public bool m_isActive;
        public float m_timer;
        public int m_amount;
        public Map.Direction m_direction;
        public int m_index;
        //float m_delay;
        //int[] m_enemyTypes
        //Direction m_entranceDirection

        public Wave()
        {
            m_isActive = false;
            m_timer = 0.0f;
            m_amount = 0;
            m_direction = Map.Direction.North;
            m_index = 0;
        }
    }
}
