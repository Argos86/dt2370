using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Tower_Defense.View
{
    class TextureAssets
    {
        public Texture2D m_texture;
        public Texture2D m_cursor;
        public Texture2D m_bana1;
        public Texture2D m_menu;
        public Texture2D m_HP;
        public Texture2D m_blank;
        public Texture2D m_tower;
        public Texture2D m_circle;
        public Texture2D m_particle;
        public Texture2D m_knight;
        public Texture2D m_dragon;
        public Texture2D m_mushroom;
        public Texture2D m_devil;
        public Texture2D m_skeleton;
        public Texture2D m_golem;
        public Texture2D m_magicattack;

        public void LoadContent(ContentManager a_content)
        {
            m_texture = a_content.Load<Texture2D>("images/sprites");
            m_cursor = a_content.Load<Texture2D>("images/cursor");
            m_bana1 = a_content.Load<Texture2D>("images/Map1");
            m_menu = a_content.Load<Texture2D>("images/Menu");
            m_HP = a_content.Load<Texture2D>("images/healthbar");
            m_blank = a_content.Load<Texture2D>("images/blank");
            m_tower = a_content.Load<Texture2D>("images/Torn");
            m_circle = a_content.Load<Texture2D>("images/circle");
            m_particle = a_content.Load<Texture2D>("images/flame");
            m_knight = a_content.Load<Texture2D>("images/normalknight128");
            m_dragon = a_content.Load<Texture2D>("images/airdragon128");
            m_mushroom = a_content.Load<Texture2D>("images/earthmushroom");
            m_devil = a_content.Load<Texture2D>("images/firedevil96");
            m_skeleton = a_content.Load<Texture2D>("images/undeadskeleton");
            m_golem = a_content.Load<Texture2D>("images/icegolem96");
            m_magicattack = a_content.Load<Texture2D>("images/magicattackyellow");
        }
    }
}
