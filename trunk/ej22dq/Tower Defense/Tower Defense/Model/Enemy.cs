using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tower_Defense.Model
{
    class Enemy
    {
        public float m_hitPoints = 0;
        public Vector2 m_pos;
        public Vector2 m_oldPos;

        public float m_timer = 0.0f;
        public List<Vector2> m_path;

        public enum Type
        {
            Normal,
            Fire,
            Water,
            Earth,
            Wind,
            Undead,
            Maxenemy,
        }
        
        Type m_type;

        public Type CurrentType 
        {
            get { return m_type; }
        }
        //private AStar m_searcher;

        public bool IsAlive()
        {
            return m_hitPoints > 0;
        }

        public Enemy()
        {
            m_hitPoints = 0;
            m_pos = new Vector2(0, 0);
            m_oldPos = new Vector2(0, 0);
            m_timer = 0.0f;
           // m_searcher = null;
        }

        public Enemy(Vector2 a_pos, int a_wave, Type a_type)
        {
            m_pos = a_pos;
            m_oldPos = m_pos;
            m_timer = 0.0f;
            m_path = new List<Vector2>();
            m_type = a_type;
            m_hitPoints = 10 * a_wave * GetHitpointMod();
       //     m_searcher = null;
        }

        public bool Update(float a_elapsedTime)
        {

            m_pos.X += GetSpeed() * a_elapsedTime;

            m_timer -= a_elapsedTime;
            if (IsAlive() == false)
            {
                return false;
            }
            if (m_timer > 0)
            {
                return false;
            }

            return true;
        }

        float GetSpeed()
        {
            switch (m_type)
            {
                case Type.Earth: return 0.3f;
                case Type.Undead: return 0.5f;
                case Type.Fire: return 1.2f;
                case Type.Water: return 1.5f;
                case Type.Wind: return 2.0f;
                case Type.Normal: return 1.0f;
            }
            return 1.0f;
        }

        float GetHitpointMod()
        {
            switch (m_type)
            {
                case Type.Earth: return 3.0f;
                case Type.Undead: return 2.0f;
                case Type.Fire: return 0.9f;
                case Type.Water: return 0.75f;
                case Type.Wind: return 0.5f;
                case Type.Normal: return 1.0f;
            }
            return 1.0f;
        }

       /* public void FollowPath()
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
        }*/
    }
}
