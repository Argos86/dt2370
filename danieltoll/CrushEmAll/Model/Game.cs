using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ZombieHoards.Model
{
    class Game : IModel
    {
        public ZombieHoards.Model.Map m_map;
        public Character[] m_civilians;
        public Character[] m_enemies;
        public Character[] m_soldiers;
        public Wave[] m_waves;
        public const int MAX_CIVILIANS = 20;
        public const int MAX_ENEMIES = 200;
        public const int MAX_SOLDIERS = 200;
        public const int MAX_WAVES = 100;

        public int m_cash = 0;


        public float GetTimeSeconds(GameTime a_gameTime)
        {
            return 1.0f * (float)a_gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;
        }

        public ZombieHoards.IEventTarget m_view;

        public Game(ZombieHoards.IEventTarget a_view)
        {
            m_view = a_view;
            Init();
        }

        public void BuySoldier(Vector2 a_at)
        {
            if (m_cash >= 25)
            {
                AddSoldier(a_at);
                m_cash -= 25;
            }
        }

        private void Init()
        {
            m_map = new Map();
            m_civilians = new Character[MAX_CIVILIANS];
            for (int i = 0; i < MAX_CIVILIANS; i++)
            {
                m_civilians[i] = new Character();
            }
            m_enemies = new Character[MAX_ENEMIES];
            for (int i = 0; i < MAX_ENEMIES; i++)
            {
                m_enemies[i] = new Character();
            }

            m_soldiers = new Character[MAX_SOLDIERS];
            for (int i = 0; i < MAX_SOLDIERS; i++)
            {
                m_soldiers[i] = new Character();
            }
            m_waves = new Wave[MAX_WAVES];
            for (int i = 0; i < MAX_WAVES; i++)
            {
                m_waves[i] = new Wave();
            }
            m_cash = 100;
        }

        public void ActivateWave(ref Wave a_wave)
        {
            a_wave.m_isActive = false;
            Random r = new Random();

            for (int i = 0; i < a_wave.m_amount; i++)
            {
                AddEnemy(a_wave.m_direction, r);
            }
        }

        private int AddEnemy(Map.Direction a_dir, Random r) {
            List<Vector2> positions = m_map.GetFreeMapPos(a_dir);

            Vector2 pos = positions.ElementAt(r.Next() % (int)positions.Count);
            int enemy = GetDeadest(MAX_ENEMIES, m_enemies);
            if (enemy != -1)
            {
                m_enemies[enemy] = new Character(pos);
            }
            return enemy;
        }
        public int AddCivilian(Vector2 a_pos)
        {
            
            int civ = GetDeadest(MAX_CIVILIANS, m_civilians);
            if (civ != -1)
            {
                m_civilians[civ] = new Character(a_pos);
            }
            return civ;
        }
        private int AddSoldier(Vector2 a_pos)
        {
            int civ = GetDeadest(MAX_SOLDIERS, m_soldiers);
            if (civ != -1)
            {
                m_soldiers[civ] = new Character(a_pos);
            }
             return civ;
        }
        private int GetDeadest(int a_max, Character[] a_arr)
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
        

        public int GetSoldier(Vector2 a_pos) 
        {
            Character target = null;
            float closest = 1000.0f;
            return GetClosestVisible(a_pos, 3.0f, ref target, MAX_SOLDIERS, ref closest, m_soldiers);
        }

        public int GetCivilian(Vector2 a_pos) 
        {
            Character target = null;
            float closest = 1000.0f;
            return GetClosestVisible(a_pos, 3.0f, ref target, MAX_CIVILIANS, ref closest, m_civilians);
        }

        

        public void MoveSoldier(int a_index, Vector2 a_at)
        {
            a_at.X = (int)a_at.X;
            a_at.Y = (int)a_at.Y;

            m_soldiers[a_index].InitMove(a_at, m_map);
        }

        public void MoveCivilian(int a_index, Vector2 a_at)
        {
            a_at.X = (int)a_at.X;
            a_at.Y = (int)a_at.Y;

            m_civilians[a_index].m_pos = a_at;
        }

        public void Draw(bool a_blocked, Vector2 a_at)
        {
            a_at = Model.MathHelper.Clamp(new Vector2(0, 0), new Vector2(Map.WIDTH-1, Map.HEIGHT-1), a_at);
            m_map.m_tiles[(int)a_at.X, (int)a_at.Y].m_tileType = a_blocked ? Tile.TileType.Blocked : Tile.TileType.Clear;
        }

        public bool Update(float a_gameTime) {

            //no civilians alive return false
            if (IsGameOver() == true || HasWon() == true)
            {
                return false;
            }

            //Check waves
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

            //Update enemies
            for (int i = 0; i < MAX_ENEMIES; i++)
            {
                UpdateEnemy(i, a_gameTime);
            }

            for (int i = 0; i < MAX_SOLDIERS; i++)
            {
                UpdateSoldier(i, a_gameTime);
                
            }

            return true;
        }

        public bool IsGameOver()
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
        }

        /* Returns true if in range
         */
        private bool GetClosestVisibleHuman(Character a_character, float a_range, out Character a_target)
        {
            a_target = null;
            float closest = 1000.0f;

           // GetClosestVisible(a_character.m_pos, a_range, ref a_target, MAX_SOLDIERS, ref closest, m_soldiers);
            GetClosestVisible(a_character.m_pos, a_range, ref a_target, MAX_CIVILIANS, ref closest, m_civilians);

            return a_target != null;
        }

        private bool GetClosestVisibleZombie(Character a_character, float a_range, out Character a_target)
        {
            a_target = null;
            float closest = 1000.0f;

            GetClosestVisible(a_character.m_pos, a_range, ref a_target, MAX_ENEMIES, ref closest, m_enemies);

            return a_target != null;
        }

        private int GetClosestVisible(Vector2 a_pos, float a_range, ref Character a_target, int a_max, ref float a_closest, Character[] a_arr)
        {
            int closestIndex = -1;
            for (int i = 0; i < a_max; i++)
            {
                if (a_arr[i].IsAlive() == true)
                {
                    float range = (a_pos - a_arr[i].m_pos).Length();
                    if (range < a_range && range < a_closest)
                    {
                        if (m_map.LineOfSight(a_pos, a_arr[i].m_pos) == true)
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



        private bool GetClosestTarget(Character a_character, out Character a_target)
        {
            a_target = null;
            float closest = 1000.0f;

            for (int i = 0; i < MAX_CIVILIANS; i++)
            {
                if (m_civilians[i].IsAlive() == true)
                {
                    float range = (a_character.m_pos - m_civilians[i].m_pos).Length();
                    if (range < closest)
                    {
                        a_target = m_civilians[i];
                        closest = range;
                    }
                }
            }

            for (int i = 0; i < MAX_SOLDIERS; i++)
            {
                if (m_soldiers[i].IsAlive() == true)
                {
                    float range = (a_character.m_pos - m_soldiers[i].m_pos).Length();
                    if (range < closest)
                    {
                        a_target = m_soldiers[i];
                        closest = range;
                    }
                }
            }

            return a_target != null;
        }

        private void Attack(Character a_character, Character a_target, bool a_isZombie)
        {
            if (a_isZombie)
            {
                a_target.m_hitPoints -= 1;
            }
            else
            {
                
                a_target.m_hitPoints -= 1;
            }
            m_view.Attack(a_character.m_pos, a_target.m_pos, a_isZombie);
        }

        public void UpdateEnemy(int a_enemy, float a_gameTime)
        {
            if (m_enemies[a_enemy].Update(a_gameTime) == false)
            {
                return;
            }
            

            Character target = null;
            float range = (float)Math.Sqrt(2.0f);

            //Close to civilian or soldier
            if (GetClosestVisibleHuman(m_enemies[a_enemy], range, out target))
            {
                Attack(m_enemies[a_enemy], target, true);
                m_enemies[a_enemy].m_timer = 1.0f;
                
            }
            else
            {
                if (m_enemies[a_enemy].m_path.Count == 0)
                {
                    if (target == null)
                    {
                        //If we can see someone we run to him
                        if (GetClosestVisibleHuman(m_enemies[a_enemy], 100.0f, out target) == false) {
                            //else we go for smell
                            GetClosestTarget(m_enemies[a_enemy], out target);
                        }
                    }
                    if (target != null) {

                        //if we are not searching right now... create new search
                        if (m_enemies[a_enemy].IsSearching() == false)
                        {
                            m_enemies[a_enemy].InitMove(target.m_pos, m_map);
                        }
                    } else {
                        //No path
                        m_enemies[a_enemy].m_timer = 10.0f;
                    }
                }
                
            }
            //follow path;
            m_enemies[a_enemy].FollowPath();
        }

        public void UpdateSoldier(int a_soldier, float a_gameTime)
        {
            if (m_soldiers[a_soldier].Update(a_gameTime) == false)
            {
                return;
            }

            Character target = null;
            float range = (float)20.0f;

            //Close to civilian or soldier
            if (GetClosestVisibleZombie(m_soldiers[a_soldier], range, out target))
            {
                Attack(m_soldiers[a_soldier], target, false);
                m_soldiers[a_soldier].m_timer = 0.3f;
                if (target.IsAlive() == false)
                {
                    m_cash += 3;
                }
            }
            
            m_soldiers[a_soldier].FollowPath();
            
        }

        public void AddWave(int a_amount, int a_direction, float a_delay, int a_index)
        {
            Wave w = new Wave();
            w.m_amount = a_amount;
            w.m_direction = (ZombieHoards.Model.Map.Direction)a_direction;
            w.m_isActive = true;
            w.m_timer = a_delay;
            m_waves[a_index] = w;
        }

        

        public bool Test()
        {
            

            //no waves left return true (win)
            for (int i = 0; i < MAX_CIVILIANS; i++)
            {
                m_civilians[i].m_hitPoints = 1;
            }
            for (int i = 0; i < MAX_WAVES; i++)
            {
                m_waves[i].m_isActive = false;
            }
            for (int i = 0; i < MAX_ENEMIES; i++)
            {
                m_enemies[i].m_hitPoints = 0;
            }
            if (HasWon() == false)
            {
                return false;
            }

            Random r = new Random();

            int enemy = AddEnemy(Map.Direction.North, r);
            if (enemy != -1)
            {
                if (m_enemies[enemy].m_pos.Y != 0)
                {
                    return false;
                }
            }

            //no civilians alive return false
            for (int i = 0; i < MAX_CIVILIANS; i++)
            {
                m_civilians[i].m_hitPoints = 0;
            }
            if (IsGameOver() == false)
            {
                return false;
            }


            //Character target = null;
            //GetClosestVisibleHuman(m_enemies[0], 1, target);

            return true;
        }

        public void SetupTestGameNoMap()
        {

            m_enemies = new Character[MAX_ENEMIES];
            for (int i = 0; i < MAX_ENEMIES; i++)
            {
                m_enemies[i] = new Character();
            }

            m_soldiers = new Character[MAX_SOLDIERS];
            for (int i = 0; i < MAX_SOLDIERS; i++)
            {
                m_soldiers[i] = new Character();
            }
            m_waves = new Wave[MAX_WAVES];
            for (int i = 0; i < MAX_WAVES; i++)
            {
                m_waves[i] = new Wave();
            }
            m_cash = 100;

            for (int i = 0; i < MAX_WAVES; i++)
            {
                
                AddWave(i, i % 4, 5.0f, i);
            }
        }

        public void SetupTestGame()
        {
            Random p = new Random();

            Init();
            //Load a map
            m_map.CreateTestMap(p);

            //Add a few civs and soldiers
            //Add e few waves
            for (int i = 0; i < 10; i++)
            {
                

                Vector2 pos = new Vector2(p.Next() % Map.WIDTH, p.Next() % Map.HEIGHT);
                if (m_map.IsClear(pos)) 
                {
                    AddCivilian(pos);
                }

                pos = new Vector2(p.Next() % Map.WIDTH, p.Next() % Map.HEIGHT);
                if (m_map.IsClear(pos)) 
                {
                    AddSoldier(pos);
                }

                
            }


            SetupTestGameNoMap();
        }
    }
}
