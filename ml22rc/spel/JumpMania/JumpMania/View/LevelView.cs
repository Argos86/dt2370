using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace JumpMania.View
{
    class LevelView
    {
        public Particle[] m_particles;
        int MAXPARTICLES = 1000;


        public LevelView(float a_levelheight)
        {
            m_particles = new Particle[MAXPARTICLES];
            Random r = new Random();
            for (int i = 0; i < MAXPARTICLES; i++)
            {
                m_particles[i] = new Particle(ref r, a_levelheight);
            }
        }

        public void DrawLevel(Model.Level a_level, Core a_core, Camera a_camera)
        {
            switch(a_level.m_levelID)
            {
                case 1:
                    {
                        a_core.Draw(a_core.m_assets.m_backgroundtexture1, new Vector2(0, (a_camera.m_scale - a_camera.camY)), new Vector2(a_camera.m_scale * 18, a_camera.m_scale * 90), new Rectangle(0, 0, 900, 4500), Color.White);
                        break;
                    }
                case 2:
                    {
                        a_core.Draw(a_core.m_assets.m_backgroundtexture2, new Vector2(0, (a_camera.m_scale - a_camera.camY)), new Vector2(a_camera.m_scale * 18, a_camera.m_scale * 90), new Rectangle(0, 0, 900, 4500), Color.White);
                        break;
                    }
                case 3:
                    {
                        a_core.Draw(a_core.m_assets.m_backgroundtexture3, new Vector2(0, (a_camera.m_scale - a_camera.camY)), new Vector2(a_camera.m_scale * 18, a_camera.m_scale * 90), new Rectangle(0, 0, 900, 4500), Color.White);
                        break;
                    }
                case 4:
                    {
                        a_core.Draw(a_core.m_assets.m_backgroundtexture4, new Vector2(0, (a_camera.m_scale - a_camera.camY)), new Vector2(a_camera.m_scale * 18, a_camera.m_scale * 90), new Rectangle(0, 0, 900, 4500), Color.White);
                        break;
                    }
                case 5:
                    {
                        a_core.Draw(a_core.m_assets.m_backgroundtexture5, new Vector2(0, (a_camera.m_scale - a_camera.camY)), new Vector2(a_camera.m_scale * 18, a_camera.m_scale * 90), new Rectangle(0, 0, 900, 4500), Color.White);
                        break;
                    }
                default:
                    
                        Console.WriteLine("Hej!");
                    
                    break;
            }
            

            for (int x = 0; x < Model.Level.WIDTH; x++)
            {
                for (int y = 0; y < Model.Level.HEIGHT; y++)
                { 
                    if (a_level.m_tiles[x, y].m_tileType == Model.Tile.TileType.Platform)
                    {
                        a_core.DrawRectangle(a_core.m_assets.m_platformtexture, new Vector2(x * a_camera.m_scale, y * a_camera.m_scale - a_camera.camY), new Rectangle(0, 0, 50, 50), a_camera.m_scale, Color.White);
                    }
                    if (a_level.m_tiles[x, y].m_tileType == Model.Tile.TileType.FloorOfDeath)
                    {
                        a_core.DrawRectangle(a_core.m_assets.m_floortexture, new Vector2(x * a_camera.m_scale, y * a_camera.m_scale - a_camera.camY), new Rectangle(0, 0, 1, 1), a_camera.m_scale, Color.White);
                    }
                    if (a_level.m_tiles[x, y].m_tileType == Model.Tile.TileType.GiantStar)
                    {
                        a_core.DrawRectangle(a_core.m_assets.m_giantstartexture, new Vector2(x * a_camera.m_scale, y * a_camera.m_scale - a_camera.camY), new Rectangle(0, 0, 50, 50), a_camera.m_scale, Color.White);
                    } 
                    
                }
            }


            for (int i = 0; i < MAXPARTICLES; i++)
            {
                Color color = new Color(m_particles[i].m_timetolive, m_particles[i].m_timetolive, m_particles[i].m_timetolive);

                Vector2 pos = m_particles[i].m_pos * a_camera.m_scale - new Vector2(0, a_camera.camY) + new Vector2((float)Math.Sin(m_particles[i].m_timetolive*3.0f), 0);

                a_core.m_spriteBatch.Draw(a_core.m_assets.m_floortexture, pos, new Rectangle(0, 0, a_core.m_assets.m_floortexture.Width, a_core.m_assets.m_floortexture.Height),
                    color, m_particles[i].m_rot, new Vector2(a_core.m_assets.m_floortexture.Width / 2, a_core.m_assets.m_floortexture.Height / 2), 0.1f, SpriteEffects.None, 0);
            }


            a_core.Draw(a_core.m_assets.m_floortexture, new Vector2(0, a_level.m_floorHeight * a_camera.m_scale - a_camera.camY), 
                                                        new Vector2(Model.Level.WIDTH * a_camera.m_scale, a_camera.m_scale * (Model.Level.HEIGHT - a_level.m_floorHeight)), 
                                                        new Rectangle(0, 0, 1, 1), Color.White);
            
        }


        public void Level1(Core a_core, Model.Level a_level, Camera a_camera)
        {
            DrawLevel(a_level, a_core, a_camera);
        }

        public void Update(float gameTime, float a_levelheight)
        {
            Random r = new Random();

            for (int i = 0; i < MAXPARTICLES; i++)
            {
                if (m_particles[i].Update(gameTime) == false)
                {
                    m_particles[i] = new Particle(ref r, a_levelheight);
                }
            }
        }
    }
}
