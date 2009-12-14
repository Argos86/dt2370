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
        public float m_slowTimer = 0;
        public Vector2 m_pos;
        public Vector2 m_oldPos;
        public int m_value = 0;
        public float m_timer = 0.0f;
        public List<Vector2> m_path;
        public int m_targetCoord = 0;
        public int MaxHP = 0;


        public Vector2[] GetWayPoints(Game.Difficulty a_diff)
        {
            return m_easyWaypoints;
        }
        public Vector2[] m_easyWaypoints =
        {
            new Vector2(0,9),
            new Vector2(3,9),
            new Vector2(3,1),
            new Vector2(1,1),
            new Vector2(1,3),
            new Vector2(21,3),
            new Vector2(21,1),
            new Vector2(19,1),
            new Vector2(19,18),
            new Vector2(21,18),
            new Vector2(21,16),
            new Vector2(1,16),
            new Vector2(1,18),
            new Vector2(3,18),
            new Vector2(3,11),
            new Vector2(Model.Map.WIDTH,11)
        };
        public Vector2[] m_mediumWaypoints =
        {
            new Vector2(0,9),
            new Vector2(3,9),
            new Vector2(3,1),
            new Vector2(1,1),
            new Vector2(1,3),
            new Vector2(21,3),
            new Vector2(21,1),
            new Vector2(19,1),
            new Vector2(19,18),
            new Vector2(21,18),
            new Vector2(21,16),
            new Vector2(1,16),
            new Vector2(1,18),
            new Vector2(3,18),
            new Vector2(3,11),
            new Vector2(Model.Map.WIDTH,11)
        };
        public Vector2[] m_hardWaypoints =
        {
            new Vector2(0,9),
            new Vector2(3,9),
            new Vector2(3,1),
            new Vector2(1,1),
            new Vector2(1,3),
            new Vector2(21,3),
            new Vector2(21,1),
            new Vector2(19,1),
            new Vector2(19,18),
            new Vector2(21,18),
            new Vector2(21,16),
            new Vector2(1,16),
            new Vector2(1,18),
            new Vector2(3,18),
            new Vector2(3,11),
            new Vector2(Model.Map.WIDTH,11)
        };

        public enum Type
        {
            Normal,
            Fire,
            Water,
            Earth,
            Wind,
            Undead,
            Max,
        }
        
        Type m_type;

        public Type CurrentType 
        {
            get { return m_type; }
        }
        //private AStar m_searcher;

        public int GetMaxHP()
        {
            MaxHP = GetValue() * (int)(100.0f * GetHitpointMod());
            return MaxHP;
        }
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

        public Enemy(Vector2 a_pos, int a_value, Type a_type)
        {
            m_pos = a_pos;
            m_oldPos = m_pos;
            m_timer = 0.0f;
            m_path = new List<Vector2>();
            m_type = a_type;
            m_value = a_value;
            m_hitPoints = GetMaxHP();
       //     m_searcher = null;
        }

        public bool Update(float a_elapsedTime)
        {
            
            //m_pos.X += GetSpeed() * a_elapsedTime;
            m_slowTimer -= a_elapsedTime;
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

        public float GetSpeed()
        {

            float speed = 1.0f;
            switch (m_type)
            {
                case Type.Earth: speed = 1.3f; break;
                case Type.Undead: speed = 1.8f; break;
                case Type.Fire: speed = 2.5f; break;
                case Type.Water: speed = 3.5f; break;
                case Type.Wind: speed = 5.0f; break;
                case Type.Normal: speed = 2.3f; break;
            }

            if (m_slowTimer > 0)
            {
                speed *= 0.5f;
            }

            return speed;
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

        public int GetValue()
        {
            return m_value + 1;
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
