using System.Collections.Generic;

namespace GlitchGame.Engine.Data
{
    public class Sprite : BitBlockSequence
    {
        public Coordinates Position { get; }

        public TileRowIndex TileRowIndex { get; }

        public PaletteIndex PaletteIndex { get; }

        public TileGrid Tiles { get; }

        public GeneralData Data { get; }

        public Sprite()
        {
            Position = new Coordinates(Settings.ScreenWidthInPixels, Settings.ScreenHeightInPixels);
            Tiles = new TileGrid(Settings.TilesPerSprite);
            Data = new GeneralData(Settings.GeneralDataBitsPerSprite);
        }

        protected override IEnumerable<IBitBlock> GetSections()
        {
            return new IBitBlock[]
            {
                Position,
                TileRowIndex,
                PaletteIndex,
                Tiles,
                Data
            };
        }
    }
}
