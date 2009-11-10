using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ZombieHoards.Model
{
    class MathHelper 
    {
        
        public static float ClampF(float a_min, float a_max, float a_data) {
            if (a_data < a_min) {
                a_data = a_min;
            }
            if (a_data > a_max) {
                a_data = a_max;
            }
            return a_data;
        }

        public static Vector2 Clamp(Vector2 a_min, Vector2 a_max, Vector2 a_data)
        {

            a_data.X = ClampF(a_min.X, a_max.X, a_data.X);
            a_data.Y = ClampF(a_min.Y, a_max.Y, a_data.Y);

            return a_data;
        }
    }
}
