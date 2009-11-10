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

namespace TestEmAll
{
    public class TestGame : Microsoft.Xna.Framework.Game
    {
        ZombieHoards.Views.Core m_coreView;
        ZombieHoards.Views.IMGui m_imgui;
        ZombieHoards.Model.Game m_game;
        ZombieHoards.Views.MapView m_map;
        ZombieHoards.Views.CharacterView m_characterView;
        ZombieHoards.Controllers.PlayGame m_play;
        ZombieHoards.Controllers.Editor m_editor;

        enum TestState
        {
            StateAuto,
            StateCore,
            StateImgui,
            StateMapView,
            StateGame,
            StateEdit
        };

        private TestState m_state = TestState.StateAuto;

        public TestGame()
        {
            Content.RootDirectory = "Content";

            m_coreView = new ZombieHoards.Views.Core(new GraphicsDeviceManager(this));
            m_imgui = new ZombieHoards.Views.IMGui();
            
            m_map = new ZombieHoards.Views.MapView();
            m_characterView = new ZombieHoards.Views.CharacterView();
            m_play = new ZombieHoards.Controllers.PlayGame(m_coreView);
            m_editor = new ZombieHoards.Controllers.Editor(m_coreView);
            m_game = new ZombieHoards.Model.Game(m_play);
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
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (m_coreView.m_input.DoQuit())
            {
                Exit();
            }


        }



        

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime a_gameTime)
        {
            base.Update(a_gameTime);
            float time = m_game.GetTimeSeconds(a_gameTime);

            GraphicsDevice.Clear(Color.Black);

            m_coreView.Update(time, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            m_coreView.Begin();

            DoMenu();


            switch (m_state)
            {
                case TestState.StateAuto:
                    if (m_game.Test() == false)
                    {
                        Exit();
                    }

                    if (m_game.m_map.Test() == false)
                    {
                        Exit();
                    }
                    break;
                case TestState.StateCore:
                    m_coreView.Test(time, GraphicsDevice);
                    break;
                case TestState.StateImgui:
                    m_imgui.Test(m_coreView);
                    break;
                case TestState.StateMapView:
                    m_map.Test(m_coreView);
                    m_characterView.Test(m_coreView, time);
                    break;
                case TestState.StateGame:
                    m_game.Update(time);
                    m_play.DoControl(m_game, time, m_game);
                    break;
                case TestState.StateEdit:
                    m_editor.DoControl(m_game, time, m_game);
                    break;
            };

            m_coreView.DrawMouse();
            m_coreView.End();
            

            base.Draw(a_gameTime);
        }

        private void DoMenu()
        {
            Vector2 pos = new Vector2(0, GraphicsDevice.Viewport.Height - 64);

            if (m_imgui.DoButton(m_coreView, "auto", pos, true, m_state == TestState.StateAuto) == ZombieHoards.Views.IMGui.ButtonState.MouseOverLBClicked)
            {
                m_state = TestState.StateAuto;
            }
            pos.X += 128;
            if (m_imgui.DoButton(m_coreView, "Core", pos, true, m_state == TestState.StateCore) == ZombieHoards.Views.IMGui.ButtonState.MouseOverLBClicked)
            {
                m_state = TestState.StateCore;
            }
            pos.X += 128;
            if (m_imgui.DoButton(m_coreView, "Imgui", pos, true, m_state == TestState.StateImgui) == ZombieHoards.Views.IMGui.ButtonState.MouseOverLBClicked)
            {
                m_state = TestState.StateImgui;
            }
            pos.X += 128;
            if (m_imgui.DoButton(m_coreView, "Map", pos, true, m_state == TestState.StateMapView) == ZombieHoards.Views.IMGui.ButtonState.MouseOverLBClicked)
            {
                m_state = TestState.StateMapView;
            }
            pos.X += 128;
            if (m_imgui.DoButton(m_coreView, "Game", pos, true, m_state == TestState.StateGame) == ZombieHoards.Views.IMGui.ButtonState.MouseOverLBClicked)
            {
                if (TestState.StateEdit != m_state)
                {
                    m_game.SetupTestGame();
                }
                else
                {
                    m_game.SetupTestGameNoMap();
                }
                m_state = TestState.StateGame;
                
            }
            pos.X += 128;
            if (m_imgui.DoButton(m_coreView, "Edit", pos, true, m_state == TestState.StateEdit) == ZombieHoards.Views.IMGui.ButtonState.MouseOverLBClicked)
            {
                m_state = TestState.StateEdit;
                
            }
        }
    }
}
