using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Featherick.Model
{
    class Enemy
    {
        public enum EnemyType
        {
            Ground,
            Boss,
            Water,
            Air
        }

        public int m_health;
        public Vector2 m_pos;
        public Vector2 m_velocity;

        public EnemyType m_enemyType;

        public Enemy(EnemyType a_enemyType, int a_health, Vector2 a_startPos)
        {
            m_pos = a_startPos;
            m_health = a_health;
            m_enemyType = a_enemyType;
        }

        public bool IsAlive()
        {
            return m_health > 0;
        }

        public void Update()
        {
            float length = m_velocity.Length();
            float maxRunSpeed = 5;

            if (length >= maxRunSpeed)
            {
                m_velocity /= length;
                m_velocity *= maxRunSpeed;
            }
        }

        public void ResetPlayer(Vector2 a_startPos, int a_health)
        {
            m_health = a_health;
            m_pos = a_startPos;
            m_velocity.X = 0.0f;
            m_velocity.Y = 0.0f;
        }

    }
}
