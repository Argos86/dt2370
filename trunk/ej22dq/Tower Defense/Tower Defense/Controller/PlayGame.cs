using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense.Controller
{
    class PlayGame : Controller.ControllerBase
    {
        private Vector2 m_mousePathFrom;
        private Vector2 m_mousePathTo;
        private Model.AStar m_mouseSearcher;
        private Model.Tower.Type m_type;

        public PlayGame(View.Core a_core) : base(a_core)
        {

        }

        public enum GameState
        {
            PLAYING,
            PAUS,
            MENU,
            END
        }



        GameState gameState = GameState.MENU;
        
        
        public override bool DoControl(Tower_Defense.Model.Game a_game, float a_elapsedTime, IModel a_model)
        {

            int scale = 800/Model.Map.HEIGHT;
            
            //Use views to draw game... 

            if (gameState == GameState.MENU)
            {
                a_game.Init(Tower_Defense.Model.Game.Difficulty.NONE);
                a_game.m_IsOver = false;
                
                m_view.DrawWelcome();
                View.IMGui.ButtonState state = m_gui.DoButton(m_core, "Easy", new Vector2(350, 350), true, false);
                if (state == View.IMGui.ButtonState.MouseOverLBClicked)
                {
                    gameState = GameState.PLAYING;
                    a_game.Init(Model.Game.Difficulty.EASY);
                }

                state = m_gui.DoButton(m_core, "Medium", new Vector2(450, 350), true, false);
                if (state == View.IMGui.ButtonState.MouseOverLBClicked)
                {
                    gameState = GameState.PLAYING;
                    a_game.Init(Model.Game.Difficulty.MEDIUM);
                }

                state = m_gui.DoButton(m_core, "Hard", new Vector2(550, 350), true, false);
                if (state == View.IMGui.ButtonState.MouseOverLBClicked)
                {
                    gameState = GameState.PLAYING;
                    a_game.Init(Model.Game.Difficulty.HARD);
                }
            }

            if (a_game.IsGameOver())
            {
                gameState = GameState.END;
                m_view.DrawLost();
                RestartButton(a_game);
                MenuButton(a_game);
                m_core.DrawMouse();
            }
            else if (a_game.HasWon())
            {
                gameState = GameState.END;
                m_view.DrawWon();
                RestartButton(a_game);
                MenuButton(a_game);
                m_core.DrawMouse();
            }
            if (gameState == GameState.PLAYING || gameState == GameState.PAUS)
            {
                a_game.m_IsOver = false;
                float currentTime = a_elapsedTime;
                if (gameState == GameState.PAUS)
                {
                    currentTime = 0.0f;
                }
                m_view.Draw(a_game, currentTime, scale, m_selectedTower, m_type);
                ControlGamePlay(a_game, a_model, scale);
            }
            m_core.DrawMouse();
            //Handle quit by esc or back button (XBOX)
            if (m_core.m_input.DoQuit())
            {
                return false;
            }

            return true;
        }

        public void MenuButton(Model.Game a_game)
        {
            View.IMGui.ButtonState state = m_gui.DoButton(m_core, "Go to the Menu", new Vector2(300, 350), true, false);
            if (state == View.IMGui.ButtonState.MouseOverLBClicked)
            {
                //a_game.Init();
                gameState = GameState.MENU;
                a_game.m_IsOver = false;
            }
        }

        public void RestartButton(Model.Game a_game)
        {
            View.IMGui.ButtonState state = m_gui.DoButton(m_core, "Restart this difficulty", new Vector2(300, 400), true, false);
            if (state == View.IMGui.ButtonState.MouseOverLBClicked)
            {
                gameState = GameState.PLAYING;
                a_game.Init(a_game.m_difficulty);
            }
        }

        public void PauseButton(Model.Game a_game)
        {
            View.IMGui.ButtonState state = m_gui.DoButton(m_core, "Pause Game", new Vector2(970, 720), true, false);
            if (state == View.IMGui.ButtonState.MouseOverLBClicked)
            {
                if (gameState == GameState.PLAYING)
                {
                    gameState = GameState.PAUS;
                    a_game.m_IsStarted = false;
                }
                else if (gameState == GameState.PAUS)
                {
                    gameState = GameState.PLAYING;
                    a_game.m_IsStarted = true;
                }
            }
        }


        public bool DoUpgradeButton(Model.Tower.UpgradeLevel a_upgrade, string a_text, Vector2 a_pos, Model.Game a_game, int a_cash, float a_cost)
        {
            if (a_upgrade < Model.Tower.UpgradeLevel.Max)
            {
                View.IMGui.ButtonState state = m_gui.DoButton(m_core, "Upgrade " + a_text + " " + a_upgrade, a_pos, a_cash >= a_cost, m_view.m_state == View.GameView.PlayState.UPGRADE_TOWER);

                if (state == View.IMGui.ButtonState.MouseOverLBClicked)
                {
                    return true;
                }
            }
            else
            {
                m_view.DrawMax(a_text, a_pos);
            }
            return false;
        }

        private void ControlGamePlay(Tower_Defense.Model.Game a_game, IModel a_model, int scale)
        {
            Vector2 logicalMousePos = m_view.GetLogicalMousePos(scale);
            //Handle input from tower button
            View.IMGui.ButtonState state = m_gui.DoButton(m_core, "Basic Tower", new Vector2(970, 110), a_game.m_cash >= Model.Tower.GetPrice(Model.Tower.Type.Normal), m_view.m_state == View.GameView.PlayState.BUY_TOWER && m_type == Model.Tower.Type.Normal);
            if (state == View.IMGui.ButtonState.MouseOverLBClicked)
            {
                m_view.m_state = View.GameView.PlayState.BUY_TOWER;

                m_type = Model.Tower.Type.Normal;
            }

            state = m_gui.DoButton(m_core, "Wind Tower", new Vector2(970, 140), a_game.m_cash >= Model.Tower.GetPrice(Model.Tower.Type.Wind), m_view.m_state == View.GameView.PlayState.BUY_TOWER && m_type == Model.Tower.Type.Wind);
            if (state == View.IMGui.ButtonState.MouseOverLBClicked)
            {
                m_view.m_state = View.GameView.PlayState.BUY_TOWER;

                m_type = Model.Tower.Type.Wind;
            }
            state = m_gui.DoButton(m_core, "Fire Tower", new Vector2(970, 170), a_game.m_cash >= Model.Tower.GetPrice(Model.Tower.Type.Fire), m_view.m_state == View.GameView.PlayState.BUY_TOWER && m_type == Model.Tower.Type.Fire);
            if (state == View.IMGui.ButtonState.MouseOverLBClicked)
            {
                m_view.m_state = View.GameView.PlayState.BUY_TOWER;

                m_type = Model.Tower.Type.Fire;
            }

            state = m_gui.DoButton(m_core, "Water Tower", new Vector2(970, 200), a_game.m_cash >= Model.Tower.GetPrice(Model.Tower.Type.Water), m_view.m_state == View.GameView.PlayState.BUY_TOWER && m_type == Model.Tower.Type.Water);
            if (state == View.IMGui.ButtonState.MouseOverLBClicked)
            {
                m_view.m_state = View.GameView.PlayState.BUY_TOWER;

                m_type = Model.Tower.Type.Water;
            }

            state = m_gui.DoButton(m_core, "Earth Tower", new Vector2(970, 230), a_game.m_cash >= Model.Tower.GetPrice(Model.Tower.Type.Earth), m_view.m_state == View.GameView.PlayState.BUY_TOWER && m_type == Model.Tower.Type.Earth);
            if (state == View.IMGui.ButtonState.MouseOverLBClicked)
            {
                m_view.m_state = View.GameView.PlayState.BUY_TOWER;

                m_type = Model.Tower.Type.Earth;
            }

            state = m_gui.DoButton(m_core, "Undead Tower", new Vector2(970, 260), a_game.m_cash >= Model.Tower.GetPrice(Model.Tower.Type.Undead), m_view.m_state == View.GameView.PlayState.BUY_TOWER && m_type == Model.Tower.Type.Undead);
            if (state == View.IMGui.ButtonState.MouseOverLBClicked)
            {
                m_view.m_state = View.GameView.PlayState.BUY_TOWER;

                m_type = Model.Tower.Type.Undead;
            }

            if (m_view.m_state == Tower_Defense.View.GameView.PlayState.UPGRADE_TOWER)
            {

                Model.Tower tower = m_selectedTower;
                if (DoUpgradeButton(tower.CurrentRangeLevel, "Range", new Vector2(970, 260 + 64), a_game, a_game.m_cash, Model.Tower.UpgradePrice(tower.CurrentType, tower.m_rangeUpgrade)) == true)
                {
                    a_game.UpgradeRange(m_selectedTower);
                }

                if (DoUpgradeButton(tower.CurrentAttackSpeed, "Speed", new Vector2(970, 260 + 114), a_game, a_game.m_cash, Model.Tower.UpgradePrice(tower.CurrentType, tower.m_speedUpgrade)) == true)
                {
                    a_game.UpgradeAttackspeed(m_selectedTower);
                }

                if (DoUpgradeButton(tower.CurrentAoE, "AoE", new Vector2(970, 260 + 164), a_game, a_game.m_cash, Model.Tower.UpgradePrice(tower.CurrentType, tower.m_AoEUpgrade)) == true)
                {
                    a_game.UpgradeAoE(m_selectedTower);
                }

                if (DoUpgradeButton(tower.CurrentDamage, "Damage", new Vector2(970, 260 + 214), a_game, a_game.m_cash, Model.Tower.UpgradePrice(tower.CurrentType, tower.m_damageUpgrade)) == true)
                {
                    a_game.UpgradeDamage(m_selectedTower);
                }
                
            }

            a_game.CheckGameOver();

            PauseButton(a_game);

            m_core.DrawMouse();
            //Mouse clicked
            if (m_core.m_input.IsClickedLMB())
            {

                switch (m_view.m_state)
                {
                    case View.GameView.PlayState.BUY_TOWER:
                        {
                            a_model.BuyTower(logicalMousePos, m_type);
                            m_view.m_state = View.GameView.PlayState.NONE;

                            break;
                        }
                    case View.GameView.PlayState.NONE:
                        {
                            Model.Tower selected = a_game.GetTower(logicalMousePos);
                            if (selected != null)
                            {
                                m_view.m_state = View.GameView.PlayState.UPGRADE_TOWER;
                                m_selectedTower = selected;
                            }
                            break;
                        }
                    case View.GameView.PlayState.UPGRADE_TOWER:
                        {
                            m_view.m_state = View.GameView.PlayState.NONE;
                            m_selectedTower = null;

                            break;
                        }
                }
            }
            if (m_core.m_input.IsClickedRMB())
            {
                m_view.m_state = View.GameView.PlayState.NONE;
            }
        }

        
    }

    
}
