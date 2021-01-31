using GlitchGame.GameMain.Extensions;
using GlitchGame.GameMain.Helpers;
using GlitchGame.GameMain.Memory;
using System;
using System.Linq;
using System.Text;

namespace GlitchGame.GameMain.Graphics
{
    public class Tile : IDataBlock
    {

        public int Address { get; }

        public int BitWidth => 128;

        private string _stringRep;

        public override string ToString()
        {
            return _stringRep;
        }

        public Tile()
        {
            Address = SystemBinaryData.IOPointer;

            var bytes = SystemBinaryData.PeekBytes(16);
            SetStringRep(bytes);
        }

        public Tile(params string[] lines)
        {
            Address = SystemBinaryData.IOPointer;

            var binaryString = string.Join("",
                lines.Select(l => BinaryHelper.HexStringToBitString(l, 2)).ToArray());

            var bytes = BinaryHelper.BitStringToBytes(binaryString);
            SystemBinaryData.WriteBytes(bytes);

            _stringRep = string.Join(Environment.NewLine, lines);
        }

        public Tile(byte[] data)
        {
            Address = SystemBinaryData.IOPointer;
            SystemBinaryData.WriteBytes(data);
            SetStringRep(data);
        }

        private void SetStringRep(byte[] data)
        {
            var bitString = BinaryHelper.BytesToBitString(data);
            var hexString = BinaryHelper.BitStringToHexString(bitString, 2);
            var sb = new StringBuilder();
            for (int i = 0; i < 8; i++)
            {
                sb.AppendLine(hexString.Substring(i * 8, 8));
            }

            _stringRep = sb.ToString();
        }

        public Value2 GetColorAtPoint(int pixelX, int pixelY, Flip flip)
        {
            if ((flip & Flip.FlipX) != 0)
                pixelX = 7 - pixelX;

            if ((flip & Flip.FlipY) != 0)
                pixelY = 7 - pixelY;

            byte index = (byte)(pixelX + (pixelY * SystemConstants.TileSize));

            SystemBinaryData.SetIOPointer(Address, index * 2);
            return new Value2();
        }

        public static byte GetColorAtPoint(PrecisionAddress tileAddress, byte pixelX, byte pixelY, Flip flip)
        {
            if ((flip & Flip.FlipX) != 0)
                pixelX = (byte)(7 - pixelX);

            if ((flip & Flip.FlipY) != 0)
                pixelY = (byte)(7 - pixelY);

            byte index = (byte)(pixelX + (pixelY * SystemConstants.TileSize));
            return SystemBinaryData.ReadAt(new PrecisionAddress(tileAddress.Address, tileAddress.BitOffset + (index * 2)), 2);
        }
    }

    public class TileSet : Sequence<Tile>
    {
        public override int Length => 8; //todo, increase
        public TileSet(params Tile[] tiles) : base(tiles.First())
        {
        }

        public byte GetColorAtPoint(byte tileIndex, byte pixelX, byte pixelY, Flip flip)
        {
            var tileAddress = GetAddress(tileIndex);
            return Tile.GetColorAtPoint(tileAddress, pixelX, pixelY, flip);
        }
    }

    public class TileIndex : ComplexDataBlock<ByteValue,Value2>
    {
        public static byte GetValue(PrecisionAddress p)
        {
            return SystemBinaryData.ReadAt(p, 8);
        }

        public static Flip GetFlip(PrecisionAddress p)
        {
            var flip = SystemBinaryData.ReadAt(new PrecisionAddress(p.Address, p.BitOffset + 8),2);
            return (Flip)flip;
        }

        public ByteValue Value => Block1;

        public Value2 FlipValue => Block2;

        public Flip Flip => (Flip)FlipValue.Value;

        public TileIndex() : base(new ByteValue(), new Value2())
        {
        }

        public TileIndex(ByteValue value, Flip flip) : base(value, new Value2((byte)flip))
        {
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class TileMap : ComplexDataBlock<ByteValue,ByteValue, Grid<TileIndex>>
    {
        public ByteValue XOffset { get; set; }
        public ByteValue YOffset { get; set; }

        public Grid<TileIndex> Tiles { get; }

        public TileMap() : this(new ByteValue(), new ByteValue(), new BackgroundTileGrid()) { }

        public TileMap(ByteValue xOffset, ByteValue yOffset, BackgroundTileGrid tiles) : base(xOffset, yOffset, tiles)
        {
            XOffset = xOffset;
            YOffset = yOffset;
            Tiles = tiles;
        }
        public void SetAll(byte tile)
        {
            Tiles.SetWritePointer(0);

            for (int i = 0; i < Tiles.Length; i++)
                new TileIndex(new ByteValue(tile), Flip.Normal);
        }

        public void Set(int x, int y, byte tile)
        {
            Tiles.SetWritePointer(x, y);
            new TileIndex(new ByteValue(tile), Flip.Normal);
        }

       
    }
}
