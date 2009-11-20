using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CombatLand.Model
{
    class Tile
    {
        private const int WIDTH = 64;
        private const int HEIGHT = 48;

        public static readonly Vector2 Size = new Vector2(WIDTH, HEIGHT);

        public enum TileType
        {
            Empty,
            Passable,
            Impassable
        };
        
        public TileType m_tileType;

        public Tile()
        {
            m_tileType = TileType.Empty;
        }
    }
}
