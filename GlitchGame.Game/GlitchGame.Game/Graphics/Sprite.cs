using GlitchGame.GameMain.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlitchGame.GameMain.Graphics
{

    public class Sprite
    {
        public Value4 Palette { get; }

        public TileMap Tiles { get; }

        public byte X { get; set; }

        public byte Y { get; set; }

        public byte Width = 16; //todo
        public byte Height = 16; //todo


        public bool HitTest(byte screenX, byte screenY)
        {
            return screenX >= X && screenX < X + Width &&
                   screenY >= Y && screenY < Y + Height;
        }

        public Sprite(Value4 palette, TileIndex ul, TileIndex ur, TileIndex bl, TileIndex br)
        {
            Palette = palette;
            Tiles = new TileMap(0,0,new TileIndex[] { ul, ur, bl, br }, 2); 
        }

        public Value4 GetColorAtScreenPoint(TileSet tileSet, byte screenX, byte screenY)
        {
            return Tiles.GetColorAtPoint(tileSet, (byte)(screenX - X), (byte)(screenY - Y));
        }
    }
}
