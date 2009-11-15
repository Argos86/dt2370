using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace MeCloidGame.Model
{
    class Tile
    {
        public enum TileType
        {
            Clear = 0,
            Solid = 1,
            Destroyable = 2
        }

        private TileType m_tileType;
        public TileType Type
        {
            get { return m_tileType; }
            set { m_tileType = value; }
        }

        public const int Width = 64;

        public const int Height = 48;

        private static readonly Vector2 m_size = new Vector2(Width, Height);
        public static Vector2 Size
        {
            get { return Tile.m_size; }
        } 


        public Tile(TileType a_tileType)
        {
            m_tileType = a_tileType;
        }
    }
}
