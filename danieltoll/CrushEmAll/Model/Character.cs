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

        public float m_timer = 0.0f;
        public List<Vector2> m_path;

        private AStar m_searcher;

        public bool IsAlive() {
            return m_hitPoints > 0;
        }

        public bool IsSearching()
        {
            if (m_searcher == null)
            {
                return false;
            }
            if (m_searcher.m_state == AStar.SearchResult.SearchNotDone)
            {
                return true;
            }
            return false;
        }

        public Character()
        {
            m_hitPoints = 0;
            m_pos = new Vector2(0, 0);
            m_oldPos = new Vector2(0, 0);
            m_timer = 0.0f;
            m_searcher = null;            
        }

        public Character(Vector2 a_pos)
        {
            m_hitPoints = 10;
            m_pos = a_pos;
            m_oldPos = m_pos;
            m_timer = 0.0f;
            m_path = new List<Vector2>();
            m_searcher = null;            
        }


        public bool Update(float a_elapsedTime)
        {
            m_timer -= a_elapsedTime;
            if (IsAlive() == false)
            {
                return false;
            }
            if (m_timer > 0)
            {
                return false;
            }

            if (IsSearching())
            {
                AStar.SearchResult res = m_searcher.Update(100);
                if (res == AStar.SearchResult.SearchSucceded)
                {
                    m_path = m_searcher.m_path;
                    
                }
                else if (res == AStar.SearchResult.SearchFailedNoPath)
                {
                    m_path.Clear();
                    m_searcher = null;
                }
            }

            return true;
        }

        public bool InitMove(Vector2 a_to, Map a_map)
        {

            m_searcher = new AStar(a_map);
            m_path.Clear();
            if (a_map.InitGetPath(m_pos, a_to, ref m_searcher) == AStar.SearchResult.SearchFailedNoPath)
            {
                
                return false;
            }
            return true;
        }

        public void FollowPath()
        {
            if (m_path.Count > 0)
            {
                m_oldPos = m_pos;
                m_pos = m_path.First();
                m_path.RemoveAt(0);
                m_timer = 0.5f;
            }
            else
            {
                m_oldPos = m_pos;
            }
        }
    }
}
