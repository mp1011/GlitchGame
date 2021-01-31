using GlitchGame.GameMain.Extensions;
using GlitchGame.GameMain.Memory;

namespace GlitchGame.GameMain.Graphics
{
    public class SpriteTable : Sequence<Sprite>
    {
        public override int Length => 4; //todo, increase to 64
        public SpriteTable(Sprite first) : base(first)
        {
        }
    }

    public class SpriteAttributes : ComplexDataBlock<Value2,Value2, Value4>
    {
        public SpriteAttributes() : base(new Value2(), new Value2(), new Value4())
        {
        }

        public SpriteAttributes(Value2 palette, Value2 flip, Value4 unused) : base(palette, flip, unused)
        {
        }

        public Value2 Palette => Block1;

        public Value2 FlipValue => Block2;

        public Flip Flip => (Flip)FlipValue.Value;

        public Value4 Unused => Block3;

    }

    public class Sprite : ComplexDataBlock<SpriteTiles, ByteValue,ByteValue, SpriteAttributes>
    {
        public static Sprite CreateDefault()
        {
            return new Sprite(
                new SpriteTiles(new TileIndex(new ByteValue(0), Flip.Normal)),
                xPos: new ByteValue(0),
                yPos: new ByteValue(0),
                new SpriteAttributes(new Value2(), new Value2(), new Value4()));
        }

        public SpriteAttributes Attributes => Block4;

        public SpriteTiles Tiles => Block1;

        public byte X
        {
            get => Block2.Value;
            set
            {
                BeginWriteBlock2();
                new ByteValue(value);
            }
        }

        public byte Y
        {
            get => Block3.Value;
            set
            {
                BeginWriteBlock3();
                new ByteValue(value);
            }
        }

        public byte Width = 16; //todo
        public byte Height = 16; //todo


        public Sprite() : base (new SpriteTiles(), new ByteValue(), new ByteValue(), new SpriteAttributes()) { }

        public Sprite(SpriteTiles tileMap, ByteValue xPos, ByteValue yPos, SpriteAttributes attr)
            : base(tileMap, xPos,yPos, attr)
        {
        }

        public bool HitTest(byte screenX, byte screenY)
        {
            return screenX >= X && screenX < X + Width &&
                   screenY >= Y && screenY < Y + Height;
        }

        public byte GetColorAtScreenPoint(TileSet tileSet, byte screenX, byte screenY)
        {
            return Tiles.GetColorAtPoint(tileSet, (byte)(screenX - X), (byte)(screenY - Y));
        }
    }

    public class SpriteTiles : Grid<TileIndex>
    {
        public SpriteTiles() : base()
        {

        }

        public SpriteTiles(params TileIndex[] items) : base(items)
        {
        }

        public override int Columns => 2;
        public override int Rows => 2;
    }
}
