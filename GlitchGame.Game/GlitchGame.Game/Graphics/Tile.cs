using GlitchGame.GameMain.Extensions;
using GlitchGame.GameMain.Memory;
using System;
using System.Text;

namespace GlitchGame.GameMain.Graphics
{
    public struct Tile
    {
        public byte[] PixelData { get; }

        private string _binary;

        public Tile(byte[] data)
        {
            PixelData = data;

            var sb = new StringBuilder();
            foreach(var d in data)  
                sb.Append(Convert.ToString(d, 2).PadLeft(8,'0'));

            _binary = sb.ToString().PadLeft(128, '0');
        }


        public Value4 GetColorAtPoint(int pixelX, int pixelY)
        {
            byte index = (byte)(pixelX + (pixelY * SystemConstants.TileSize));

            var valueAtIndex = Convert.ToByte(_binary.Substring(index*2, 2), 2);
            return new Value4(valueAtIndex);
        }
    }

    public class TileSet
    {
        public Tile[] Tiles { get; }

        public TileSet(params Tile[] tiles)
        {
            Tiles = tiles;
        }
    }

    public struct TileIndex
    {
        public byte Value { get; }

        public TileIndex(byte value)
        {
            Value = value;
        }

        public static implicit operator byte(TileIndex i) => i.Value;
        public static implicit operator TileIndex(byte b) => new TileIndex(b);
    }

    public class TileMap
    {
        public Value8 XOffset { get; set; }
        public Value8 YOffset { get; set; }

        public TileIndex[] Tiles { get; }

        public byte Columns { get; }

        public TileMap(Value8 xOffset, Value8 yOffset, TileIndex[] tiles, byte columns)
        {
            XOffset = xOffset;
            YOffset = yOffset;
            Tiles = tiles;
            Columns = columns;
        }

        public Value4 GetColorAtPoint(TileSet tileSet, int pixelX, int pixelY)
        {
            int tileX = 0, tileY = 0;

            while(pixelX >= SystemConstants.TileSize)
            {
                pixelX -= SystemConstants.TileSize;
                tileX++;
            }

            while (pixelY >= SystemConstants.TileSize)
            {
                pixelY -= SystemConstants.TileSize;
                tileY++;
            }

            var tileIndex = Tiles.GetFromCoordinates(tileX, tileY, Columns);
            return tileSet.Tiles[tileIndex].GetColorAtPoint(pixelX, pixelY);
        }
    }
}
