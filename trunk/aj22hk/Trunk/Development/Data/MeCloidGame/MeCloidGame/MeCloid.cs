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

namespace MeCloidGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MeCloid : Microsoft.Xna.Framework.Game
    {
        // TODO: Fix the content manager issues.
        // Model
        Model.Game m_game;
        
        // Controllers
        Controllers.PlayGame m_playGameController;

        // Views
        Views.Core m_coreView;

        public MeCloid()
        {
            m_coreView = new Views.Core(new GraphicsDeviceManager(this));
            m_playGameController = new Controllers.PlayGame(m_coreView);
            m_game = new Model.Game(m_playGameController);
            
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
            // Create a new SpriteBatch, which can be used to draw textures.
            m_coreView.Initialize(GraphicsDevice, Content);

            // TODO: use this.Content to load your game content here
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
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            float elapsedTime = (float)a_gameTime.ElapsedGameTime.TotalSeconds;
            m_game.Update(elapsedTime);

            //base.Update(a_gameTime);

            //m_coreView.Update();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime a_gameTime)
        {
            base.Update(a_gameTime);

            float elapsedTime = (float)a_gameTime.ElapsedGameTime.TotalSeconds;
            m_coreView.Update();

            // Main controller
            GraphicsDevice.Clear(Color.Black);
            
            m_coreView.Begin();

            if (m_playGameController.DoControll(m_game, elapsedTime) == false)
            {
                Exit();
            }
            
            m_coreView.End();

            base.Draw(a_gameTime);
        }
    }
}
