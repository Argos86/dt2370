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
    class Camera
    {

        public int m_scale;
        public int camY;

        public Camera()
        {
           //m_scale = a_core.m_graphics.GraphicsDevice.Viewport.Width / Model.Level.WIDTH
           //m_scale = 900/18

           m_scale = 50;
           camY = 256;
        }

        public void centreCamera(float playerPosY, int screenheight)
        {
            camY = (int)(playerPosY * m_scale - screenheight/2);
        } 
    }
}
