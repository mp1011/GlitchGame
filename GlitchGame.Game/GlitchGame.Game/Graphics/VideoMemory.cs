namespace GlitchGame.GameMain.Graphics
{
    public class VideoMemory
    {
        public PaletteGroup Palettes { get; }

        public TileSet Tiles { get; }

        public Sprite Sprite { get; }

        public TileMap ScreenTiles { get; }

        public VideoMemory(PaletteGroup paletteGroup, TileSet tiles, Sprite sprite)
        {
            Palettes = paletteGroup;
            Tiles = tiles;
            Sprite = sprite;
            ScreenTiles = new TileMap(0, 0, new TileIndex[960], 32);
        }
    }
}
