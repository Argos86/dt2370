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

namespace Tower_Defense.View
{
    class Core
    {
        GraphicsDeviceManager m_graphics;
        SpriteBatch m_spriteBatch;
        public View.Input m_input;
        public View.TextureAssets m_assets;
        public View.FontAssets m_fonts;
        public View.SoundAssets m_sounds;
        GraphicsDevice m_device;
        public Particle[] m_particles;
        int MAXPARTICLE = 5000;

        public Core(GraphicsDeviceManager a_graphic)
        {
            m_particles = new Particle[MAXPARTICLE];
            Random r = new Random();
            for (int i = 0; i < MAXPARTICLE; i++)
            {
                m_particles[i] = new Particle(ref r);
            }
            m_graphics = a_graphic;
            m_input = new View.Input();
            m_assets = new View.TextureAssets();
            m_fonts = new View.FontAssets();
            m_sounds = new View.SoundAssets();
            m_graphics.PreferredBackBufferHeight = 800;
            m_graphics.PreferredBackBufferWidth = 1200;
            //m_graphics.IsFullScreen = true;
        }

        public void Initiate(GraphicsDevice a_device, ContentManager a_content)
        {
            m_spriteBatch = new SpriteBatch(a_device);
            m_assets.LoadContent(a_content);
            m_fonts.LoadContent(a_content);
            m_sounds.LoadContent(a_content);

            m_device = a_device;
        }

        public void Update(float a_elapsedTime, int a_width, int a_height)
        {

            m_input.Update(a_elapsedTime, new Vector2(0, 0), new Vector2(a_width, a_height));
            for (int i = 0; i < MAXPARTICLE; i++)
            {
                m_particles[i].Update(a_elapsedTime);
            }
        }

        public void Draw(Texture2D a_texture, Rectangle a_dest, Rectangle a_source, Color a_color)
        {
            m_spriteBatch.Draw(a_texture, a_dest, a_source, a_color);
        }

        public void DrawMagicAttack(Texture2D a_texture, Vector2 a_centerpos, Rectangle a_source, Color a_color, float a_rotation)
        {
            m_spriteBatch.Draw(a_texture, a_centerpos, a_source, a_color, a_rotation, new Vector2(32,32), new Vector2(1,1), SpriteEffects.None, 0);
        }

        public void DrawText(string a_text, SpriteFont a_font, Vector2 a_pos, Color a_color)
        {
            m_spriteBatch.DrawString(a_font, a_text, a_pos, a_color);
        }

        public void DrawParticle()
        {
            for (int i = 0; i < MAXPARTICLE; i++)
            {

                m_spriteBatch.Draw(m_assets.m_particle, m_particles[i].m_pos, new Rectangle(0, 0, m_assets.m_particle.Width, m_assets.m_particle.Height),
                    Color.White, m_particles[i].m_rot, new Vector2(m_assets.m_particle.Width/2, m_assets.m_particle.Height/2), 0.1f, SpriteEffects.None, 0);
            }
        }

        public void DrawMouse()
        {
            Rectangle source = new Rectangle(0, 0, 180, 180);
            Rectangle retval = new Rectangle(
                m_device.Viewport.X,
                m_device.Viewport.Y,
                m_device.Viewport.Width,
                m_device.Viewport.Height);

            Rectangle dest = new Rectangle(0, 0, 16, 16);
            dest.X = (int)m_input.m_mousePos.X - dest.Center.X;
            dest.Y = (int)m_input.m_mousePos.Y - dest.Center.Y;

            Draw(m_assets.m_cursor, dest, source, Color.White);

        }

        public void Begin(SpriteBlendMode a_mode)
        {
            m_spriteBatch.Begin(a_mode);
        }

        public void End()
        {
            m_spriteBatch.End();
        }

    }
}
