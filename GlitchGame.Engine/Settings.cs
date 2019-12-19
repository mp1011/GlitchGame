namespace GlitchGame.Engine
{
    public static class Settings
    {
        public const int BitsPerColorChannel = 3;

        public const int ColorsPerPalette = 8;
        public const int NumPalettes = 4;

        public const int VRAMTileRows = 8;
        public const int VRAMTileColumns = 8;

        public const int TileSize = 8;

        public const int BackgroundLayers = 2;

        public const int ScreenWidthInPixels = 320;

        public const int ScreenHeightInPixels = 240;


        public const int ScreenWidthInTiles = ScreenWidthInPixels / TileSize;

        public const int ScreenHeightInTiles = ScreenHeightInPixels / TileSize;

        public const int ScreensPerBGLayer = 4;

        public const int TotalSprites = 8;

        public const int TilesPerSprite = 4;

        public const int GeneralDataBitsPerSprite = 1024;

        public const int GeneralDataBits = 1024*5

    }
}
