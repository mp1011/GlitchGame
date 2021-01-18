namespace GlitchGame.GameMain.Graphics
{
    public class VideoMemory
    {
        public PaletteGroup Palettes { get; }

        public TileSet Tiles { get; }

        public Sprite Sprite { get; }

        public TileLayer BgLayer { get; }

        public VideoMemory(PaletteGroup paletteGroup, TileSet tiles, Sprite sprite)
        {
            Palettes = paletteGroup;
            Tiles = tiles;
            Sprite = sprite;
            BgLayer = new TileLayer();
        }
    }
}
