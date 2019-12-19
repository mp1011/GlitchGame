using GlitchGame.Engine.Extensions;

namespace GlitchGame.Engine.Data
{ 
    public struct PaletteIndex : IBitBlock
    {
        public int BitSize => Settings.NumPalettes.BitsNeeded();
    }
}
