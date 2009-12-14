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

        public bool m_IsStarted = false;
        public bool m_IsOver = false;

        public const int MAX_ENEMIES = 50;
        public const int MAX_TOWERS = 20000;
        public const int MAX_WAVES = 50;

        public int m_cash = 0;


        public Difficulty m_difficulty = Difficulty.EASY;

        public enum Difficulty
        {
            NONE,
            EASY,
            MEDIUM,
            HARD
        }

        public float GetTimeSeconds(GameTime a_gameTime)
        {
            return 1.0f * (float)a_gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;
        }

        public Tower_Defense.IEventTarget m_view;

        public Game(Tower_Defense.IEventTarget a_view)
        {
            m_view = a_view;
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

        public void Init(Difficulty a_diff)
        {
            m_IsStarted = true;
            m_map = new Map(m_difficulty);
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

                AddWave(MAX_ENEMIES, 1.0f, i);
            }

            m_cash = 100;
        }

        public void ActivateWave(ref Wave a_wave)
        {
            m_cash += 50 * (a_wave.m_index);
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


        public bool Update(float a_gameTime)
        {

            //no civilians alive return false
            if (IsGameOver() == true || HasWon() == true || m_IsStarted == false)
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


                Vector2 dir = c.GetWayPoints(m_difficulty)[c.m_targetCoord] - c.m_pos;

                float len = dir.Length();


                if (len <= c.GetSpeed() * a_gameTime)
                {
                    c.m_pos = c.GetWayPoints(m_difficulty)[c.m_targetCoord];
                    c.m_targetCoord++;

                    if (c.m_targetCoord == c.GetWayPoints(m_difficulty).Count())
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
            if (m_IsOver == true)
            {
                return true;
            }
        }

        public void UpgradeRange(Model.Tower a_tower)
        {
            Model.Tower.Type type = a_tower.CurrentType;
            Model.Tower.UpgradeLevel upgradeRange = a_tower.CurrentRangeLevel;

            if (m_cash >= Model.Tower.UpgradePrice(type, upgradeRange))
            {
                int increaseLevel = (int)a_tower.CurrentRangeLevel;
                increaseLevel++;
                a_tower.m_rangeUpgrade = (Model.Tower.UpgradeLevel)increaseLevel;
                m_cash -= (int)Model.Tower.UpgradePrice(type, upgradeRange);
            }
        }

        public void UpgradeAttackspeed(Model.Tower a_tower)
        {
            Model.Tower.Type type = a_tower.CurrentType;
            Model.Tower.UpgradeLevel speedUpgrade = a_tower.CurrentAttackSpeed ;

            if (m_cash >= Model.Tower.UpgradePrice(type, speedUpgrade))
            {
                int increaseLevel = (int)a_tower.CurrentAttackSpeed;
                increaseLevel++;
                a_tower.m_speedUpgrade = (Model.Tower.UpgradeLevel)increaseLevel;
                m_cash -= (int)Model.Tower.UpgradePrice(type, speedUpgrade);
            }
        }

        public void UpgradeAoE(Model.Tower a_tower)
        {
            Model.Tower.Type type = a_tower.CurrentType;
            Model.Tower.UpgradeLevel aoeUpgrade = a_tower.CurrentAoE;

            if (m_cash >= Model.Tower.UpgradePrice(type, aoeUpgrade))
            {
                int increaseLevel = (int)a_tower.CurrentAoE;
                increaseLevel++;
                a_tower.m_AoEUpgrade = (Model.Tower.UpgradeLevel)increaseLevel;
                m_cash -= (int)Model.Tower.UpgradePrice(type, aoeUpgrade);
            }
        }

        public void UpgradeDamage(Model.Tower a_tower)
        {
            Model.Tower.Type type = a_tower.CurrentType;
            Model.Tower.UpgradeLevel damageUpgrade = a_tower.CurrentDamage;

            if (m_cash >= Model.Tower.UpgradePrice(type, damageUpgrade))
            {
                int increaseLevel = (int)a_tower.CurrentDamage;
                increaseLevel++;
                a_tower.m_damageUpgrade = (Model.Tower.UpgradeLevel)increaseLevel;
                m_cash -= (int)Model.Tower.UpgradePrice(type, damageUpgrade);
            }
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

                if (m_IsOver == true)
                {
                    return true;
                }
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
                    if (a_arr[i].m_pos.X > 0)
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

            for (int i = 0; i< MAX_ENEMIES; i++) 
            {
                Enemy c = m_enemies[i];
                if (c.IsAlive() == true)
                {
                    float distance = (c.m_pos - a_target.m_pos).Length();
                    if (distance <= a_tower.AoE(a_tower.CurrentType, a_tower.CurrentAoE))
                    {

                        if (a_tower.CurrentType == Tower.Type.Fire)
                        {
                            float damage = 1.0f - distance / a_tower.AoE(a_tower.CurrentType, a_tower.CurrentAoE);
                            c.m_hitPoints -= a_tower.GetDamage(a_target.CurrentType, a_tower.CurrentDamage) * damage;
                        }
                        else
                        {
                            c.m_hitPoints -= a_tower.GetDamage(a_target.CurrentType, a_tower.CurrentDamage);
                        }

                        if (c.IsAlive() == false)
                        {

                            m_view.KilledEnemy(i);
                            m_cash += 1;
                        }
                        if (a_tower.CurrentType == Tower.Type.Water)
                        {
                            c.m_slowTimer = 3.0f;
                        }

                    }
                }
            }
            
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
