namespace GlitchGame.GameMain.Graphics
{
    public class VideoMemory
    {
        public PaletteGroup Palettes { get; }

        public TileSet Tiles { get; }

        public SpriteTable Sprites { get; }

        public TileLayer BgLayer { get; }

        public VideoMemory(PaletteGroup paletteGroup, TileSet tiles)
        {
            Palettes = paletteGroup;
            Tiles = tiles;
            Sprites = new SpriteTable();
            BgLayer = new TileLayer();
        }
    }
}
