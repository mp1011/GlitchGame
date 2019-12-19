namespace GlitchGame.Engine.Data
{
    public struct ScreenLayout : IBitBlock
    {
        public int BitSize => 2;

        public int GetHorizontalPixels()
        {
            //will depend on the specific value
            return (Settings.ScreensPerBGLayer / 2) * Settings.ScreenWidthInPixels;
        }

        public int GetVerticalPixels()
        {
            //will depend on the specific value
            return (Settings.ScreensPerBGLayer / 2) * Settings.ScreenHeightInPixels;
        }
    }
}
