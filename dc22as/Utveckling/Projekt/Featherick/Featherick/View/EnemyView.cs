﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Featherick.View
{
    class EnemyView
    {
        SpriteEffects m_lastFlip = SpriteEffects.None;

        public void DrawEmeny(Core a_core, Vector2 a_pos, Point a_currentFrame, Point a_frameSize, Model.Enemy a_enemy, Camera a_camera)
        {
            if (a_enemy.IsAlive() == true)
            {
                float rescaledImageSize = a_camera.m_scale * 2.0f;

                Rectangle m_dest = new Rectangle((int)(a_pos.X * a_camera.m_scale - rescaledImageSize / 4.0f - a_camera.m_topLeft.X), (int)(a_pos.Y * a_camera.m_scale - a_camera.m_topLeft.Y), (int)rescaledImageSize, (int)rescaledImageSize);
                Rectangle m_source = new Rectangle(0, 0, a_core.m_assets.m_playerTexture.Width, a_core.m_assets.m_playerTexture.Height);

                a_core.Draw(a_core.m_assets.m_enemy1Texture, m_dest, m_source, Color.White, 0, Vector2.Zero, m_lastFlip, 0);
            }
        }

    }
}
