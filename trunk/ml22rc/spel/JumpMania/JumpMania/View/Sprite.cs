using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace JumpMania.View
{
    class Sprite
    {
        //Spritens storlek
        public Rectangle m_size;

        //Vilken skala bilden ska ha.
        public float m_scale = 1.0f;

        //Spritens position
        public Vector2 s_position = new Vector2(0, 0);

        //Texturobjektet som används när man ritar spriten
        private Texture2D mSpriteTexture;
        public Texture2D MSpriteTexture
        {
            get { return mSpriteTexture; }
            private set { mSpriteTexture = value; }
        }


        //Laddar texturen för spriten genom att använda Content Pipeline
        public void LoadContent(ContentManager theContentManager, string picture)
        {
            mSpriteTexture = theContentManager.Load<Texture2D>(picture);
            m_size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * m_scale), (int)(mSpriteTexture.Height * m_scale));
        }

        //Uppdaterar spriten och dess position beroende på farten, riktningen samt förfluten tid.
        public void Update(GameTime theGameTime, Vector2 m_speed, Vector2 m_direction)
        {
            s_position += m_direction * m_speed * (float)theGameTime.ElapsedGameTime.TotalSeconds;
        }


        //Ritar spriten på skärmen
        public virtual void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(mSpriteTexture, s_position,
            new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height),
            Color.White, 0.0f, Vector2.Zero, m_scale, SpriteEffects.None, 0);
        }

    }
}
