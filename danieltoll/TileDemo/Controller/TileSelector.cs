using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TileDemo.Controller
{
    class TileSelector
    {
        public IMGui m_gui = new IMGui();
        public MouseState m_oldState;
        public void DoControll(SpriteBatch a_batch, ref View.LevelView a_view)
        {
            MouseState state = Mouse.GetState();
            Vector2 mp = new Vector2(state.X, state.Y);
            bool isClicked = m_oldState.LeftButton == ButtonState.Pressed && state.LeftButton == ButtonState.Released;
            m_oldState = state;
            if (m_gui.DoButton(mp, isClicked, "Map", new Vector2(10 * 64, 32), true, false, a_batch) == IMGui.ButtonState.MouseOverLBClicked)
            {
                a_view.m_variation = 0;
            }

            if (m_gui.DoButton(mp, isClicked, "Clean", new Vector2(10 * 64, 96), true, false, a_batch) == IMGui.ButtonState.MouseOverLBClicked)
            {
                a_view.m_variation = 192;
            }

            if (m_gui.DoButton(mp, isClicked, "Weird", new Vector2(10 * 64, 32+128), true, false, a_batch) == IMGui.ButtonState.MouseOverLBClicked)
            {
                a_view.m_variation = 320;
            }

            a_batch.Draw(a_view.m_textures.m_sprites, new Rectangle((int)mp.X, (int)mp.Y, 32, 32), Color.Black);
        }
    }
}
