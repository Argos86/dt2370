using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense.View
{
    class Effects
    {
        class Effect
        {
            public Vector2 m_from;
            public Vector2 m_to;
            public float m_timer;

            public enum EffectType
            {
                TypeSplat,
                TypeGunFire
            };
            public EffectType m_type;
            public Model.Tower.Type m_towerType;
        }
        float m_magicattacktimer = 0.0f;

        Effect[] m_effects;

        private const int MAX_EFFECTS = 50000;
        private const float GUN_TIME = 2.5f;

        public Effects()
        {
            m_effects = new Effect[MAX_EFFECTS];
            for (int i = 0; i < MAX_EFFECTS; i++)
            {
                m_effects[i] = new Effect();
                m_effects[i].m_timer = -1.0f;
            }
        }

        public void AddSplat(Vector2 a_pos)
        {
            int index = GetOldestEffect();

            m_effects[index].m_to = a_pos;
            m_effects[index].m_timer = 3.0f;
            m_effects[index].m_type = Effect.EffectType.TypeSplat;
        }

        public void AddShot(Vector2 a_from, Vector2 a_to, Model.Tower.Type a_type)
        {
            int index = GetOldestEffect();
            m_effects[index].m_towerType = a_type;
            m_effects[index].m_from = a_from + new Vector2(0,-0.5f);
            m_effects[index].m_to = a_to;
            m_effects[index].m_timer = GUN_TIME;
            m_effects[index].m_type = Effect.EffectType.TypeGunFire;
        }

        int GetOldestEffect()
        {
            float oldest = 1000.0f;
            int index = 0;
            for (int i = 0; i < MAX_EFFECTS; i++)
            {
                if (oldest > m_effects[i].m_timer)
                {
                    oldest = m_effects[i].m_timer;
                    index = i;
                }
            }
            return index;
        }


        public void Update(float a_elapsedTime, Core a_core, int a_scale)
        {
            m_magicattacktimer += a_elapsedTime;
            for (int i = 0; i < MAX_EFFECTS; i++)
            {
                m_effects[i].m_timer -= a_elapsedTime;
                if (m_effects[i].m_timer > 0)
                {
                    Render(m_effects[i], a_core, (float)a_scale);
                }
            }
        }

        private void Render(Effect a_effect, Core a_core, float a_scale)
        {
            Vector2 pos = new Vector2();
            if (a_effect.m_type == Effect.EffectType.TypeSplat)
            {
                pos = a_effect.m_to;
            }
            else if (a_effect.m_type == Effect.EffectType.TypeGunFire)
            {
                Vector2 dir = a_effect.m_to - a_effect.m_from;
                float len = dir.Length();

                float timeTotal = len / 20.0f; //100 / sec
                float timeElapsed = GUN_TIME - a_effect.m_timer;
                float at = timeElapsed / timeTotal;

                dir.Normalize();

                pos = a_effect.m_from + dir * len * at;
                if (at > 1.0f)
                {
                    pos = a_effect.m_to;
                }
                else
                {
                    Vector2 attackDir = a_effect.m_to - a_effect.m_from;
                    float rot = (float)Math.Atan2(attackDir.Y, attackDir.X);

                    Color color = Color.White;
                    Byte c = 128;
                    switch (a_effect.m_towerType)
                    {
                        case Model.Tower.Type.Normal: color = Color.White; break;
                        case Model.Tower.Type.Earth: color = Color.Brown; break;
                        case Model.Tower.Type.Fire: color = Color.Red; break;
                        case Model.Tower.Type.Water: color = new Color(c, c, 255); break;
                        case Model.Tower.Type.Wind: color = Color.Green; break;
                        case Model.Tower.Type.Undead: color = Color.Gray; break;
                    }

                    float frameanimation = 21.0f;
                    float fps = 24.0f;
                    
                    int xFrame = (int)(m_magicattacktimer * fps)% (int)frameanimation;

                    Rectangle src = new Rectangle(xFrame * 64, 0, 64, 64);
                    
                    Vector2 attackPos = new Vector2(pos.X * a_scale, pos.Y * a_scale - a_scale / 2);
                    a_core.DrawMagicAttack(a_core.m_assets.m_magicattack, attackPos, src, color, rot);
                }
            }
        }
    }
}
