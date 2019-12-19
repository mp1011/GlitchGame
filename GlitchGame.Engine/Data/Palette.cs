using GlitchGame.Engine.Extensions;

namespace GlitchGame.Engine.Data
{
    public class Palette : BitBlockArray<Color>
    {
        protected override Color[] Elements { get; }

        public Palette()
        {
            Elements = new Color[Settings.ColorsPerPalette].FillDefault();
        }
    }
}
