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

namespace JumpMania
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class JM : Microsoft.Xna.Framework.Game
    {
        //Modell
        Model.Game m_game;

        //Controller
        Controller.ControllPlayer a_playguy;


        //View
        JumpMania.View.Core m_chview;
        JumpMania.View.LevelView m_leview;

        public JM()
        {
            Content.RootDirectory = "Content";

            a_playguy = new JumpMania.Controller.ControllPlayer();
            m_game = new JumpMania.Model.Game();
            m_chview = new JumpMania.View.Core(new GraphicsDeviceManager(this));
            m_leview = new JumpMania.View.LevelView();

            
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


            Window.Title = "JumpMania";

            m_game.Init();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.


            m_chview.Initiate(GraphicsDevice, Content);


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
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            a_playguy.Update(gameTime, m_game);


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            
            GraphicsDevice.Clear(Color.Aquamarine);

            m_chview.Begin();
            

            m_chview.DrawPlaya(gameTime, GraphicsDevice, m_game.m_player.m_position);


            m_chview.End();

            base.Draw(gameTime);
        }
    }
}
