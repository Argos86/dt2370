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

        public void BuyTower(Vector2 a_at, Model.Tower.Type a_type)
        {
            if (m_cash >= Tower.GetPrice(a_type))
            {
                if (AddTower(a_at, a_type) == true)
                {
                    m_cash -= Tower.GetPrice(a_type);
                }
            }
        }

        public Model.Tower GetTower(Vector2 a_pos)
        {
            Tower target = null;
            float closest = 1.0f;
            foreach (Tower c in m_towers)
            {
                if (c != null)
                {
                    float len = (a_pos - c.m_pos).Length();
                    if (len < closest)
                    {
                        closest = len;
                        target = c;
                    }
                }
            }
            return target;
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

                AddWave(MAX_ENEMIES, 5.0f, i);
            }

            m_cash = 100;
        }

        public void ActivateWave(ref Wave a_wave)
        {
            a_wave.m_isActive = false;
            Random r = new Random();
            int type = r.Next() % ((int)Enemy.Type.Max);
            for (int i = 0; i < a_wave.m_amount; i++)
            {
                AddEnemy(new Vector2(-2 * i, 9), a_wave.m_index, (Model.Enemy.Type)type);
            }
        }

        private int AddEnemy(Vector2 a_pos, int a_wave, Model.Enemy.Type a_type)
        {
            
            int enemy = GetDeadest(MAX_ENEMIES, m_enemies);
            if (enemy != -1)
            {
                m_enemies[enemy] = new Enemy(a_pos, a_wave, a_type);
            }
            return enemy;
        }

        private bool AddTower(Vector2 a_pos, Model.Tower.Type a_type)
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
                        m_towers[i] = new Tower(a_pos, a_type);
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
            if (IsEnemyAlive() == false)
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
            foreach (Model.Enemy c in m_enemies)
            {
                if (c.IsAlive() == false)
                {
                    continue;
                }
                Vector2 dir = c.m_waypoints[c.m_targetCoord] - c.m_pos;

                float len = dir.Length();





                if (len <= c.GetSpeed() * a_gameTime)
                {
                    c.m_pos = c.m_waypoints[c.m_targetCoord];
                    c.m_targetCoord++;

                    if (c.m_targetCoord == c.m_waypoints.Count())
                    {
                        c.m_hitPoints = 0;
                        hitpoints -= 1;
                    }
                }
                else
                {
                    dir.Normalize();
                    c.m_pos += dir * c.GetSpeed() * a_gameTime;
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
                    UpdateTower(i, a_gameTime);
                }
            }
            return true;
        }

        public bool IsEnemyAlive()
        {     
            foreach (Model.Enemy c in m_enemies)
            {
                if (c.IsAlive())
                {
                    return true;
                }
            }
            return false;
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

            foreach (Enemy c in m_enemies)
            {
                float distance = (c.m_pos - a_target.m_pos).Length();
                if (distance <= AoE(a_tower))
                {

                    if (a_tower.CurrentType == Tower.Type.Fire)
                    {
                        float damage = 1.0f - distance / AoE(a_tower);
                        c.m_hitPoints -= a_tower.GetDamage(a_target.CurrentType) * damage;
                    }
                    else
                    {
                        c.m_hitPoints -= a_tower.GetDamage(a_target.CurrentType);
                    }


                    if (a_tower.CurrentType == Tower.Type.Water)
                    {
                        c.m_slowTimer = 3.0f;
                    }

                }
            }
            
            m_view.Attack(a_tower.m_pos, a_target.m_pos);
        }

        private float AoE(Tower a_tower)
        {
            switch (a_tower.CurrentType)
            {
                case Model.Tower.Type.Earth: return 5;
                case Model.Tower.Type.Fire: return 5;
                case Model.Tower.Type.Water: return 3;
            }
            return 0.1f;
            
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

        public void UpdateTower(int a_tower, float a_gameTime)
        {
            if (m_towers[a_tower].Update(a_gameTime) == false)
            {
                return;
            }

            //har jag inget mål m_lockedEnemy, eller målet för långt bort eller målet dött
                //Skaffa ett mål
            if (m_towers[a_tower].m_lockedEnemy == null || (m_towers[a_tower].m_lockedEnemy.m_pos - m_towers[a_tower].m_pos).Length() > m_towers[a_tower].GetRange(m_towers[a_tower].m_rangeUpgrade) ||
                m_towers[a_tower].m_lockedEnemy.IsAlive() == false)
            {
                GetClosestVisibleZombie(m_towers[a_tower], m_towers[a_tower].GetRange(m_towers[a_tower].m_rangeUpgrade), out m_towers[a_tower].m_lockedEnemy);
            }

            //har jag ett mål
            if (m_towers[a_tower].m_lockedEnemy != null)
            {
                Attack(m_towers[a_tower], m_towers[a_tower].m_lockedEnemy);
                m_towers[a_tower].m_timer = Model.Tower.GetAttackSpeed(m_towers[a_tower].CurrentType, m_towers[a_tower].m_speedUpgrade);
                if (m_towers[a_tower].m_lockedEnemy.IsAlive() == false)
                {
                    m_cash += m_towers[a_tower].m_lockedEnemy.GetValue();
                }
            }

            //Close to civilian or soldier
           

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
