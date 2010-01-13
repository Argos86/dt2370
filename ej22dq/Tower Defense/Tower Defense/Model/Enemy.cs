using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tower_Defense.Model
{
    class Enemy : IComparable<Enemy>
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
        public int m_index = 0;

        public int m_pathIndex = 0;

        public static Vector2[,] GetWayPoints(Game.Difficulty a_diff)
        {
            Vector2[,] m_diff = m_easyWaypoints;

            switch (a_diff)
            {
                case Game.Difficulty.EASY: m_diff = m_easyWaypoints; break;
                case Game.Difficulty.MEDIUM: m_diff = m_mediumWaypoints; break;
                case Game.Difficulty.HARD: m_diff = m_hardWaypoints; break;
            }
            return m_diff;
        }
        public static Vector2[,] m_easyWaypoints ={
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
        }
                                           };
        public static Vector2[,] m_mediumWaypoints = {
        {
            new Vector2(0,3),
            new Vector2(8,3),
            new Vector2(8,1),
            new Vector2(13,1),
            new Vector2(13,5),
            new Vector2(16,5),
            new Vector2(16,2),
            new Vector2(19,2),
            new Vector2(19,7),
            new Vector2(2,7),
            new Vector2(2,9),
            new Vector2(Model.Map.WIDTH,9),
        },
        {
            new Vector2(0,16),
            new Vector2(8,16),
            new Vector2(8,18),
            new Vector2(13,18),
            new Vector2(13,14),
            new Vector2(16,14),
            new Vector2(16,17),
            new Vector2(19,17),
            new Vector2(19,12),
            new Vector2(2,12),
            new Vector2(2,10),
            new Vector2(Model.Map.WIDTH,10),
        }
                                               };
        public static Vector2[,] m_hardWaypoints = {
        {
            new Vector2(0,1),
            new Vector2(3,1),
            new Vector2(3,3),
            new Vector2(6,3),
            new Vector2(6,2),
            new Vector2(12,2),
            new Vector2(12,5),
            new Vector2(13,5),
            new Vector2(14,5),
            new Vector2(15,5),
            new Vector2(15,9),
            new Vector2(Model.Map.WIDTH,9)
        },
        {
            new Vector2(0,6),
            new Vector2(3,6),
            new Vector2(3,4),
            new Vector2(6,4),
            new Vector2(6,5),
            new Vector2(8,5),
            new Vector2(8,7),
            new Vector2(13,7),
            new Vector2(13,9),
            new Vector2(15,9),
            new Vector2(15,5),
            new Vector2(Model.Map.WIDTH,5)
        },
        {
            new Vector2(0,13),
            new Vector2(3,13),
            new Vector2(3,15),
            new Vector2(6,15),
            new Vector2(6,14),
            new Vector2(8,14),
            new Vector2(8,12),
            new Vector2(13,12),
            new Vector2(13,10),
            new Vector2(15,10),
            new Vector2(15,14),
            new Vector2(Model.Map.WIDTH,14)
        },
        {
            new Vector2(0,18),
            new Vector2(3,18),
            new Vector2(3,16),
            new Vector2(6,16),
            new Vector2(6,17),
            new Vector2(12,17),
            new Vector2(12,14),
            new Vector2(13,14),
            new Vector2(13,14),
            new Vector2(15,14),
            new Vector2(15,10),
            new Vector2(Model.Map.WIDTH,10)
        }
        };

        public int CompareTo(Enemy other)
        {
            if (m_pos.Y < other.m_pos.Y)
            {
                return -1;
            }
            else if (m_pos.Y > other.m_pos.Y)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

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

        public Enemy(Vector2 a_pos, int a_value, Type a_type, int a_pathIndex)
        {
            m_pos = a_pos;
            m_oldPos = m_pos;
            m_timer = 0.0f;
            m_path = new List<Vector2>();
            m_type = a_type;
            m_value = a_value;
            m_hitPoints = GetMaxHP();
            m_pathIndex = a_pathIndex;
        }

        public bool Update(float a_elapsedTime)
        {
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

    }
}
