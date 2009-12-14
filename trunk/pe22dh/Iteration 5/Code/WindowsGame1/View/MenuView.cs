using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CombatLand.View
{
    class MenuView
    {
        private View.Core m_core;

        public MenuView(View.Core a_core)
        {
            m_core = a_core;
           
        }
        public void DrawMenu(CombatLand.Model.Game a_game)
        {
            float a_layerDepth = 0.0f;
            
            Rectangle src = new Rectangle(0, 0, View.Core.BACK_BUFFER_WIDTH, View.Core.BACK_BUFFER_HEIGHT);
            m_core.Draw(m_core.m_assets.m_mainMenuTexture, new Rectangle(0, 0, View.Core.BACK_BUFFER_WIDTH,
                View.Core.BACK_BUFFER_HEIGHT), src, Color.White, SpriteEffects.None, a_layerDepth);
            m_core.DrawText("COMBAT LAND", m_core.m_fonts.m_font, new Vector2(500.0f, 200.0f), Color.Brown);
            m_core.DrawText("Press jump to play", m_core.m_fonts.m_font, new Vector2(450.0f, 270.0f), Color.WhiteSmoke);
        }
        public void DrawOverlay(CombatLand.Model.Game a_game)
        {
            float a_layerDepth = 0.0f;

            if (!a_game.m_player.IsAlive())
            {
                Rectangle src = new Rectangle(0, 0, View.Core.BACK_BUFFER_WIDTH, View.Core.BACK_BUFFER_HEIGHT);
                m_core.Draw(m_core.m_assets.m_overlayGameOver, new Rectangle(0, 0, View.Core.BACK_BUFFER_WIDTH,
                    View.Core.BACK_BUFFER_HEIGHT), src, Color.White, SpriteEffects.None, a_layerDepth);
            }
            if (a_game.m_player.IsAlive() && a_game.m_hasWon)
            {
                Rectangle src = new Rectangle(0, 0, View.Core.BACK_BUFFER_WIDTH, View.Core.BACK_BUFFER_HEIGHT);
                m_core.Draw(m_core.m_assets.m_overlayWin, new Rectangle(0, 0, View.Core.BACK_BUFFER_WIDTH,
                    View.Core.BACK_BUFFER_HEIGHT), src, Color.White, SpriteEffects.None, a_layerDepth);
            }
        }
    }
}
