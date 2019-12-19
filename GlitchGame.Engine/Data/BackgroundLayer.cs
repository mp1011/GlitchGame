using System.Collections.Generic;

namespace GlitchGame.Engine.Data
{
    public class BackgroundLayer : BitBlockSequence
    {
        public TileRowIndex TileRowIndex { get; }

        public PaletteIndex PaletteIndex { get; }

        public TileGrid LayerTiles { get; }

        public ScrollSpeed ScrollSpeed { get; }

        public BackgroundLayer()
        {
            int sw = Settings.ScreensPerBGLayer / 2;
            int sh = Settings.ScreensPerBGLayer / 2;

            LayerTiles = new TileGrid(Settings.ScreenWidthInTiles*sw, Settings.ScreenHeightInTiles*sh);
        }

        protected override IEnumerable<IBitBlock> GetSections()
        {
            return new IBitBlock[]
            {
                TileRowIndex,
                PaletteIndex,
                ScrollSpeed,
                LayerTiles
            };
        }
    }
}
