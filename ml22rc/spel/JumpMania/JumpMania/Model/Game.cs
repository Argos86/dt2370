using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace JumpMania.Model
{
    class Game
    {

        public Player m_player;

        public void Init()
        {
            m_player = new JumpMania.Model.Player();
        }

        public float GetTimeSeconds(GameTime a_gameTime)
        {
            return 1.0f * (float)a_gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;
        }

        /*public bool IsGameOver()
        {
            for (int i = 0; i < MAX_CIVILIANS; i++)
            {
                if (m_civilians[i].IsAlive())
                {
                    return false;
                }
            }
            return true;
        }

        public bool HasWon()
        {
            for (int i = 0; i < MAX_WAVES; i++)
            {
                if (m_waves[i].m_isActive == true)
                {
                    return false;
                }
            }
            for (int i = 0; i < MAX_ENEMIES; i++)
            {
                if (m_enemies[i].IsAlive() == true)
                {
                    return false;
                }
            }
            return true;
        }*/
    }
}
