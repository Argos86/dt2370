using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tower_Defense.Model
{
    class Game : IModel
    {
        public Tower_Defense.Model.Map m_map;
        public Enemy[] m_enemies;
        public Tower[] m_towers;
        public Wave[] m_waves;
        public int hitpoints = 0;
        public const int MAX_ENEMIES = 50;
        public const int MAX_TOWERS = 20000;
        public const int MAX_WAVES = 100;

        public int m_cash = 0;

        public float GetTimeSeconds(GameTime a_gameTime)
        {
            return 1.0f * (float)a_gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;
        }

        public Tower_Defense.IEventTarget m_view;

        public Game(Tower_Defense.IEventTarget a_view)
        {
            m_view = a_view;
            Init();
        }

        public void BuyTower(Vector2 a_at)
        {
            if (m_cash >= 25)
            {
                if (AddTower(a_at) == true)
                {
                    m_cash -= 25;
                }
            }
        }

        private void Init()
        {
            m_map = new Map();
            hitpoints = 20;

            m_towers = new Tower[MAX_TOWERS];
            for (int i = 0; i < MAX_TOWERS; i++)
            {
                m_towers[i] = null;
            }
            
            m_waves = new Wave[MAX_WAVES];
            for (int i = 0; i < MAX_WAVES; i++)
            {
                m_waves[i] = new Wave();
            }
            
            m_enemies = new Enemy[MAX_ENEMIES];
            for (int i = 0; i < MAX_ENEMIES; i++)
            {
                m_enemies[i] = new Enemy();//new Vector2(-1*i, 35), m_waves[i].m_index, Enemy.Type.Normal);
            }

            for (int i = 0; i < MAX_WAVES; i++)
            {

                AddWave(MAX_ENEMIES, 10.0f, i);
            }

            m_cash = 100;
        }

        public void ActivateWave(ref Wave a_wave)
        {
            a_wave.m_isActive = false;
            Random r = new Random();

            for (int i = 0; i < a_wave.m_amount; i++)
            {
                AddEnemy(new Vector2(-1 * i, 35), a_wave.m_index);
            }
        }

        private int AddEnemy(Vector2 a_pos, int a_wave)
        {
            
            int enemy = GetDeadest(MAX_ENEMIES, m_enemies);
            Random rand = new Random();
            if (enemy != -1)
            {
                int value =  rand.Next()%((int)Enemy.Type.Maxenemy-1);
                m_enemies[enemy] = new Enemy(a_pos, a_wave, (Enemy.Type)value);
            }
            return enemy;
        }

        private bool AddTower(Vector2 a_pos)
        {
            if (m_map.CanPlaceTower(a_pos) == false)
            {
                return false;
            }
            else
            {

                for (int i = 0; i < MAX_TOWERS; i++)
                {
                    if (m_towers[i] == null)
                    {
                        m_towers[i] = new Tower(a_pos);
                        m_map.PlacedTower(a_pos);
                        return true;
                    }
                }
                return false;
            }
            
        }

        private int GetDeadest(int a_max, Enemy[] a_arr)
        {

            for (int i = 0; i < a_max; i++)
            {
                if (a_arr[i].IsAlive() == false)
                {
                    return i;
                }
            }

            return -1;
        }

        public void Draw(bool a_blocked, Vector2 a_at)
        {
            a_at = Model.MathHelper.Clamp(new Vector2(0, 0), new Vector2(Map.WIDTH - 1, Map.HEIGHT - 1), a_at);
            m_map.m_tiles[(int)a_at.X, (int)a_at.Y].m_tileType = a_blocked ? Tile.TileType.Blocked : Tile.TileType.Clear;
        }

        public bool Update(float a_gameTime)
        {

            //no civilians alive return false
            if (IsGameOver() == true || HasWon() == true)
            {
                return false;
            }

            //Check waves
            if (m_enemies[49].IsAlive() == false)
            {
                for (int i = 0; i < MAX_WAVES; i++)
                {
                    if (m_waves[i].m_isActive == true)
                    {
                        m_waves[i].m_timer -= a_gameTime;
                        if (m_waves[i].m_timer < 0.0f)
                        {
                            ActivateWave(ref m_waves[i]);
                        }
                        break;
                    }
                }
            }

            //Update enemies
            for (int i = 0; i < MAX_ENEMIES; i++)
            {
                UpdateEnemy(i, a_gameTime);
            }

            for (int i = 0; i < MAX_TOWERS; i++)
            {
                if (m_towers[i] != null)
                {
                    UpdateTower(i, a_gameTime, m_waves[i].m_index);
                }
            }

            return true;
        }

        public bool IsGameOver()
        {
            if (hitpoints > 0)
            {
                return false;
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
        }

        private bool GetClosestVisibleZombie(Tower a_tower, float a_range, out Enemy a_target)
        {
            a_target = null;
            float closest = 1000.0f;

            GetClosestVisible(a_tower.m_pos, a_range, ref a_target, MAX_ENEMIES, ref closest, m_enemies);

            return a_target != null;
        }

        private int GetClosestVisible(Vector2 a_pos, float a_range, ref Enemy a_target, int a_max, ref float a_closest, Enemy[] a_arr)
        {
            int closestIndex = -1;
            for (int i = 0; i < a_max; i++)
            {
                if (a_arr[i].IsAlive() == true)
                {
                    float range = (a_pos - a_arr[i].m_pos).Length();
                    if (range < a_range && range < a_closest)
                    {
                        a_target = a_arr[i];
                        a_closest = range;
                        closestIndex = i;
                    }
                }
            }

            return closestIndex;
        }

        private bool GetClosestTarget(Enemy a_character, out Tower a_target)
        {
            a_target = null;
            float closest = 1000.0f;

            for (int i = 0; i < MAX_TOWERS; i++)
            {
                float range = (a_character.m_pos - m_towers[i].m_pos).Length();
                if (range < closest)
                {
                    a_target = m_towers[i];
                    closest = range;
                }
            }

            return a_target != null;
        }

        private void Attack(Tower a_tower, Enemy a_target)
        {

            a_target.m_hitPoints -= 1;
            m_view.Attack(a_tower.m_pos, a_target.m_pos);
        }

        public void UpdateEnemy(int a_enemy, float a_gameTime)
        {
            if (m_enemies[a_enemy].Update(a_gameTime) == false)
            {
                return;
            }

           // float range = (float)Math.Sqrt(2.0f);

            //follow path;
            //m_enemies[a_enemy].FollowPath();
        }

        public void UpdateTower(int a_tower, float a_gameTime, int a_wave)
        {
            if (m_towers[a_tower].Update(a_gameTime) == false)
            {
                return;
            }

            Enemy target = null;
            float range = (float)20.0f;

            //Close to civilian or soldier
            if (GetClosestVisibleZombie(m_towers[a_tower], range, out target))
            {
                Attack(m_towers[a_tower], target);
                m_towers[a_tower].m_timer = 0.3f;
                if (target.IsAlive() == false)
                {
                    m_cash += 2 * a_wave;
                }
            }

        }

        public void AddWave(int a_amount, float a_delay, int a_index)
        {
            Wave w = new Wave();
            w.m_amount = a_amount;
            w.m_isActive = true;
            w.m_timer = a_delay;
            w.m_index = a_index;
            m_waves[a_index] = w;
        }
        

    }
}
