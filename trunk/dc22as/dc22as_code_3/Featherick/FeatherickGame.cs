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

namespace Featherick
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class FeatherickGame : Microsoft.Xna.Framework.Game
    {
        private const int BackBufferWidth = 1440;
        private const int BackBufferHeight = 900;
        
        private GraphicsDeviceManager m_graphics;

        //Model
        Featherick.Model.Game m_game;

        //Controllers
        Controller.PlayGame m_playGameController;

        //View
        View.Core m_coreView;



        public FeatherickGame()
        {
            m_graphics = new GraphicsDeviceManager(this);
            m_graphics.PreferredBackBufferWidth = BackBufferWidth;
            m_graphics.PreferredBackBufferHeight = BackBufferHeight;
            //m_graphics.IsFullScreen = true;
            m_coreView = new View.Core(m_graphics);
            m_playGameController = new Controller.PlayGame(m_coreView);
            m_game = new Featherick.Model.Game(m_playGameController);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            m_coreView.Initiate(GraphicsDevice, Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime a_gameTime)
        {
            float elapsedTime = m_game.GetTimeSeconds(a_gameTime);
            m_game.Update(elapsedTime);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime a_gameTime)
        {
            base.Update(a_gameTime);

            //Update input 
            float elapsedTime = m_game.GetTimeSeconds(a_gameTime);
            m_coreView.Update(elapsedTime, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);



            //Main controller
            GraphicsDevice.Clear(Color.White);
            m_coreView.Begin();
            //m_coreView.DrawText("test", m_coreView.m_fonts.m_baseFont, new Vector2(10, 10), Color.Black);           
            if (m_playGameController.DoControl(m_game, elapsedTime, m_game) == false)
            {
                Exit();
            }
            m_coreView.End();

            base.Draw(a_gameTime);


            m_game.m_level.Test();
        }
    }
}
