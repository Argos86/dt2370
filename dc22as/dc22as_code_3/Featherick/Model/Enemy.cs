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
        }

        public int m_health;
        public Vector2 m_pos;

        public EnemyType m_enemyType;

        public Enemy()
        {
            //m_enemyType = EnemyType.Ground;
        }


    }
}
