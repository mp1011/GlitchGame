using System.Collections.Generic;

namespace GlitchGame.Engine.Data
{
    public class VRAM : BitBlockSequence
    {
        public PaletteTable PaletteTable { get; } = new PaletteTable();

        public VRAMTileData TileData { get; } = new VRAMTileData();

        protected override IEnumerable<IBitBlock> GetSections()
        {
            return new IBitBlock[]
            {
                PaletteTable,
                TileData
            };
        }
    }
}
