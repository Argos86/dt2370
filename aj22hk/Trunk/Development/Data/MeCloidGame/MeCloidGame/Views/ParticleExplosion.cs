using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MeCloidGame.Views
{
    class ParticleExplosion
    {
        const int MaxParticles = 10000;
        Particle[] m_particles;

        public ParticleExplosion()
        {
            m_particles = new Particle[MaxParticles];
            for (int i = 0; i < m_particles.Length; ++i)
            {
                m_particles[i] = new Particle();
            }
        }

        public void DrawParticles(Core a_coreView, Camera a_camera, float a_elapsedTime)
        {
            foreach (Particle p in m_particles)
            {
                Vector2 pos = a_camera.Translate(p.m_pos);
                a_coreView.Draw(a_coreView.Textures.Tiles, new Rectangle((int)pos.X, (int)pos.Y, (int)(Particle.m_size * a_camera.m_zoom), (int)(Particle.m_size * a_camera.m_zoom)), new Rectangle(0, 0, 20, 20), Color.White);
                p.Update(a_elapsedTime);
            }
        }
    }
}
