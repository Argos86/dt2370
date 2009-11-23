using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ZombieHoards.Model
{
    class Character
    {

        public int m_hitPoints = 0;
        public Vector2 m_pos;
        public Vector2 m_oldPos;
        public Vector2 m_velocity;
        public bool m_collideGround;
        
        public bool IsAlive() {
            return m_hitPoints > 0;
        }

        

        public Character()
        {
            m_hitPoints = 0;
            m_pos = new Vector2(0, 0);
            m_oldPos = new Vector2(0, 0);
            m_velocity = new Vector2(0, 0);
                
        }

        public Character(Vector2 a_pos)
        {
            m_hitPoints = 10;
            m_pos = a_pos;
            m_oldPos = m_pos;
                  
        }


        public bool Update(float a_elapsedTime)
        {
            if (IsAlive() == false)
            {
                return false;
            }
            return true;
        }

        

        
    }
}
