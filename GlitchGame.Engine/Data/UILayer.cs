using System.Collections.Generic;

namespace GlitchGame.Engine.Data
{
    public class UILayer : BitBlockSequence
    {
        public TileRowIndex TileRowIndex { get; }

        public PaletteIndex PaletteIndex { get; }

        public TileGrid LayerTiles { get; }

        public UILayer()
        {
            LayerTiles = new TileGrid(Settings.ScreenWidthInTiles, Settings.ScreenHeightInTiles);
        }

        protected override IEnumerable<IBitBlock> GetSections()
        {
            return new IBitBlock[]
            {
                TileRowIndex,
                PaletteIndex,
                LayerTiles
            };
        }
    }
}
