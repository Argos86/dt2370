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

namespace CombatLand
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class CLand : Microsoft.Xna.Framework.Game
    {
        //Model
        CombatLand.Model.Game m_game;

        //Controllers
        CombatLand.Controller.PlayGame m_playGameController;

        //View
        CombatLand.View.Core m_coreView;

        

        public CLand()
        {
            m_coreView = new CombatLand.View.Core(new GraphicsDeviceManager(this));
            m_playGameController = new Controller.PlayGame(m_coreView);
            m_game = new CombatLand.Model.Game();
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
            m_coreView.Initiate(GraphicsDevice, Content);

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            float elapsedTime = m_game.GetTimeSeconds(a_gameTime);
            base.Update(a_gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime a_gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            float elapsedTime = m_game.GetTimeSeconds(a_gameTime);

            // TODO: Add your drawing code here
            m_playGameController.DoControl(m_game, elapsedTime);
            base.Draw(a_gameTime);
        }
    }
}
