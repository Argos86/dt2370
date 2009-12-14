using System;
using System.Collections.Generic;
using System.Linq;
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
    
    class GameView
    {
        // : Microsoft.Xna.Framework.Game
        View.Core m_chview;
        View.LevelView m_leview;
        View.Camera m_cam;

        public GameView(Game a_game)
        {
            
            
            m_chview = new View.Core(new GraphicsDeviceManager(a_game));
            m_leview = new View.LevelView();
            //m_floview = new JumpMania.View.FloorView();
            m_cam = new View.Camera();
        }
       
        public void Load(ContentManager a_content, GraphicsDevice a_gradevice)
        {
            m_chview.Initiate(a_gradevice, a_content);
        }

        public void Draw(GameTime gameTime, Model.Game a_game)
        {
            m_chview.Begin();

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            m_cam.centreCamera(a_game.m_player.m_position.Y, m_chview.m_graphics.GraphicsDevice.Viewport.Height);
            m_leview.Level1(m_chview, a_game.m_level, m_cam);
            m_chview.DrawPlaya(gameTime, a_game.m_player.m_position, m_cam, a_game.m_player);
 


            m_chview.Update(elapsed);
            m_chview.DrawParticle();



            m_chview.End();
        }


    }
}
