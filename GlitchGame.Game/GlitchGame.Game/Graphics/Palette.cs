using GlitchGame.GameMain.Memory;

namespace GlitchGame.GameMain.Graphics
{
    public struct Palette
    {
        public Value64[] Colors { get; }

        public Palette(Value64 color1, Value64 color2, Value64 color3, Value64 color4)
        {
            Colors = new Value64[4] { color1, color2, color3, color4 };
        }

        public Value64 Get(Value4 value)
        {
            return Colors[value];
        }
    }

    public struct PaletteGroup
    {
        public Palette[] Palettes { get; }

        public PaletteGroup(Palette p1, Palette p2, Palette p3, Palette p4)
        {
            Palettes = new Palette[4] { p1, p2, p3, p4 };
        }

        public Palette Get(Value4 value)
        {
            return Palettes[value];
        }
    }
}
