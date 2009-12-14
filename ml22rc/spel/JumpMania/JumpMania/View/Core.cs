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

namespace JumpMania.View
{
    class Core
    {
        SpriteBatch m_spriteBatch;
        public View.TextureAssets m_assets;
        public GraphicsDeviceManager m_graphics;
        GraphicsDevice m_device;
        SpriteEffects m_flip = SpriteEffects.None;

        public Particle[] m_particles;
        int MAXPARTICLES = 300;


        public Core(GraphicsDeviceManager a_graphics)
        {
            m_graphics = a_graphics;
            m_assets = new View.TextureAssets();
            m_graphics.PreferredBackBufferHeight = 900;
            m_graphics.PreferredBackBufferWidth = 900;


            m_particles = new Particle[MAXPARTICLES];
            Random r = new Random();
            for (int i = 0; i < MAXPARTICLES; i++)
            {
                m_particles[i] = new Particle(ref r);
            }

            

            //Möjliggör fullskärm
            //m_graphics.ToggleFullScreen();
            //m_graphics.IsFullScreen = true;
        }

        public void Initiate(GraphicsDevice a_device, ContentManager a_content)
        {
            m_assets.LoadContent(a_content);
            m_spriteBatch = new SpriteBatch(a_device);
            m_device = a_device;
        }

        public void DrawRectangle(Texture2D a_texture, Vector2 position, Rectangle a_source, int a_scale, Color a_color)
        {
            Rectangle dest = new Rectangle((int)position.X, (int)position.Y, a_scale, a_scale);
            m_spriteBatch.Draw(a_texture, dest, a_source, a_color);
        }

        public void Draw(Texture2D a_texture, Vector2 position, Vector2 size, Rectangle a_source, Color a_color)
        {
            Rectangle dest = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            m_spriteBatch.Draw(a_texture, dest, a_source, a_color);
        }

        public void Draw(Texture2D a_texture, Vector2 position, Vector2 size, Rectangle a_source, Color a_color, float a_rotation, Vector2 a_origin, SpriteEffects a_effect, float a_layerDepth)
        {
            Rectangle dest = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            m_spriteBatch.Draw(a_texture, dest, a_source, a_color, a_rotation, a_origin, a_effect, a_layerDepth);
        }    

        public void DrawPlaya(GameTime gameTime, Vector2 a_pos, Camera a_camera, Model.Player a_player)
        {
            if (a_player.m_movement < 0)
            {
                m_flip = SpriteEffects.FlipHorizontally;
            }
            else if (a_player.m_movement > 0)
            {
                m_flip = SpriteEffects.None;
            }

            //int scale = m_graphics.GraphicsDevice.Viewport.Width / Model.Level.WIDTH;
            a_pos *= a_camera.m_scale;
            a_pos.Y -= a_camera.camY;
            Draw(m_assets.m_texture, a_pos, new Vector2(a_camera.m_scale * 1, a_camera.m_scale * 2), new Rectangle(0, 0, 50, 100), Color.White, 0, Vector2.Zero, m_flip, 0);
        }

        public void DrawParticle()
        {
            for (int i = 0; i < MAXPARTICLES; i++)
            {

                m_spriteBatch.Draw(m_assets.m_flametexture, m_particles[i].m_pos, new Rectangle(0, 0, m_assets.m_flametexture.Width, m_assets.m_flametexture.Height),
                    Color.White, m_particles[i].m_rot, new Vector2(m_assets.m_flametexture.Width / 2, m_assets.m_flametexture.Height / 2), 0.1f, SpriteEffects.None, 0);
            }
        }

        public void Update(float gameTime)
        {
            for (int i = 0; i < MAXPARTICLES; i++)
            {
                m_particles[i].Update(gameTime);
            }
        }

        public void Begin()
        {
            m_spriteBatch.Begin();
        }

        public void End()

        {
            m_spriteBatch.End();
        }
    }
}
