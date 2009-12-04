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


        public enum UpgradeLevel
        {
            None,
            One, 
            Two,
            Three
        };

        public UpgradeLevel m_rangeUpgrade = UpgradeLevel.None;
        public UpgradeLevel m_speedUpgrade = UpgradeLevel.None;
        public UpgradeLevel m_upgradePrice = UpgradeLevel.None;

        

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

        public static float GetAttackSpeed(Type a_type, UpgradeLevel a_level)
        {
            float upgradeModifier = 1.0f;

            switch (a_level)
            {
                case UpgradeLevel.One: upgradeModifier = 0.80f; break;
                case UpgradeLevel.Two: upgradeModifier = 0.60f; break;
                case UpgradeLevel.Three: upgradeModifier = 0.40f; break;
            }

            switch (a_type)
            {
                case Type.Earth: return 2.0f * upgradeModifier;
                case Type.Undead: return 0.3f * upgradeModifier;
                case Type.Fire: return 0.5f * upgradeModifier;
                case Type.Water: return 1.0f * upgradeModifier;
                case Type.Wind: return 0.3f * upgradeModifier;
                case Type.Normal: return 0.3f * upgradeModifier;
            }
            return 1.0f;
        }

        public float GetRange(UpgradeLevel a_level)
        {
            float upgradeModifier = 0.0f;

            switch (a_level)
            {
                case UpgradeLevel.One: upgradeModifier = 1.0f; break;
                case UpgradeLevel.Two: upgradeModifier = 2.0f; break;
                case UpgradeLevel.Three: upgradeModifier = 3.0f; break;
            }
            switch (m_type)
            {
                case Type.Earth: return 5f + upgradeModifier;
                case Type.Undead: return 7f + upgradeModifier;
                case Type.Fire: return 10f + upgradeModifier;
                case Type.Water: return 3f + upgradeModifier;
                case Type.Wind: return 16f + upgradeModifier;
                case Type.Normal: return 9f + upgradeModifier;
            }
            return upgradeModifier;
        }

        public static float UpgradePrice(Type a_type, UpgradeLevel a_level)
        {
            float upgradeModifier = 1.0f;

            switch (a_level)
            {
                case UpgradeLevel.One: upgradeModifier = 3.0f; break;
                case UpgradeLevel.Two: upgradeModifier = 6.0f; break;
                case UpgradeLevel.Three: upgradeModifier = 9.0f; break;
            }

            switch (a_type)
            {
                case Type.Earth: return 10.0f * upgradeModifier;
                case Type.Undead: return 10.0f * upgradeModifier;
                case Type.Fire: return 10.0f * upgradeModifier;
                case Type.Water: return 10.0f * upgradeModifier;
                case Type.Wind: return 10.0f * upgradeModifier;
                case Type.Normal: return 10.0f * upgradeModifier;
            }
            return 1.0f;

        }
        public static int GetPrice(Type a_type)
        {
            switch (a_type)
            {
                case Type.Earth: return 23;
                case Type.Undead: return 28;
                case Type.Fire: return 28;
                case Type.Water: return 35;
                case Type.Wind: return 40;
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
