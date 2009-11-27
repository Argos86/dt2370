using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense.Model
{
    class Tower
    {
        public Vector2 m_pos = new Vector2();
        public float m_timer = 0.0f;
        public Enemy m_lockedEnemy = null;

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

        public static float GetAttackSpeed(Model.Tower.Type a_type)
        {
            switch (a_type)
            {
                case Type.Earth: return 0.3f;
                case Type.Undead: return 0.8f;
                case Type.Fire: return 1.5f;
                case Type.Water: return 2.5f;
                case Type.Wind: return 4.0f;
                case Type.Normal: return 1.3f;
            }
            return 1.0f;
        }

        public float GetRange()
        {
            switch (m_type)
            {
                case Type.Earth: return 13f;
                case Type.Undead: return 18f;
                case Type.Fire: return 28f;
                case Type.Water: return 35f;
                case Type.Wind: return 50f;
                case Type.Normal: return 25f;
            }
            return 1.0f;
        }

        public static int GetPrice(Model.Tower.Type a_type)
        {
            switch (a_type)
            {
                case Type.Earth: return 13;
                case Type.Undead: return 18;
                case Type.Fire: return 28;
                case Type.Water: return 35;
                case Type.Wind: return 50;
                case Type.Normal: return 25;
            }
            return 10;
        }

        public float GetDamage(Model.Enemy.Type a_enemytype)
        {
            float VersusSelf = 2.0f;
            float VersusLesser = 20.0f;
            float VersusHigher = 5.0f;
            float VersusNormal = 10.0f;


            if (m_type == Type.Earth)
            {
                if (a_enemytype == Model.Enemy.Type.Earth)
                    return VersusSelf;
                if (a_enemytype == Model.Enemy.Type.Water)
                    return VersusLesser;
                if (a_enemytype == Model.Enemy.Type.Wind)
                    return VersusHigher;
            }
            else if (m_type == Type.Water)
            {
                if (a_enemytype == Model.Enemy.Type.Water)
                    return VersusSelf;
                if (a_enemytype == Model.Enemy.Type.Fire)
                    return VersusLesser;
                if (a_enemytype == Model.Enemy.Type.Earth)
                    return VersusHigher;
            }
            else if (m_type == Type.Fire)
            {
                if (a_enemytype == Model.Enemy.Type.Fire)
                    return VersusSelf;
                if (a_enemytype == Model.Enemy.Type.Undead)
                    return VersusLesser;
                if (a_enemytype == Model.Enemy.Type.Water)
                    return VersusHigher;
            }
            else if (m_type == Type.Undead)
            {
                if (a_enemytype == Model.Enemy.Type.Undead)
                    return VersusSelf;
                if (a_enemytype == Model.Enemy.Type.Wind)
                    return VersusLesser;
                if (a_enemytype == Model.Enemy.Type.Fire)
                    return VersusHigher;
            }
            else if (m_type == Type.Wind)
            {
                if (a_enemytype == Model.Enemy.Type.Wind)
                    return VersusSelf;
                if (a_enemytype == Model.Enemy.Type.Earth)
                    return VersusLesser;
                if (a_enemytype == Model.Enemy.Type.Undead)
                    return VersusHigher;
            }
            return VersusNormal;
        }
        public Tower(Vector2 a_pos, Type a_type)
        {
            m_pos = a_pos;
            m_type = a_type;
        }

        public bool Update(float a_elapsedTime)
        {
            m_timer -= a_elapsedTime;
             
            if (m_timer > 0)
            {
                return false;
            }

            return true;
        }
    }
}
