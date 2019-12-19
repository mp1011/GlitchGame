using GlitchGame.Engine.Extensions;

namespace GlitchGame.Engine.Data
{
    public class PaletteTable : BitBlockArray<Palette>
    {
        protected override Palette[] Elements { get; }

        public PaletteTable()
        {
            Elements = new Palette[Settings.NumPalettes].FillDefault();
        }
    }
}
