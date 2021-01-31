using GlitchGame.GameMain.Memory;

namespace GlitchGame.GameMain.Graphics
{
    public class Palette : Sequence<Value6>
    {
        public override int Length => 4;
        public Palette(byte color1, byte color2, byte color3, byte color4) 
            : this(new Value6(color1), new Value6(color2), new Value6(color3), new Value6(color4))
        {
        }

        private Palette(Value6 color1, Value6 color2, Value6 color3, Value6 color4) : base(color1)
        {
        }

        public Palette() : this(new Value6(), new Value6(), new Value6(), new Value6())
        {
        }
    }

    public class PaletteGroup : Sequence<Palette>
    {
        public override int Length => 4;

        public PaletteGroup(Palette p1, Palette p2, Palette p3, Palette p4) : base(p1)
        {
        }

        public PrecisionAddress GetColorAddress(Value2 palette, byte color)
        {
            var paletteAddress = GetAddress(palette.Value);
            return new PrecisionAddress(paletteAddress.Address, paletteAddress.BitOffset + (6 * color));
        }
    }
}
