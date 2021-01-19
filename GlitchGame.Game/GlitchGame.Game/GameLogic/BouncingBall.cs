using GlitchGame.GameMain.Memory;

namespace GlitchGame.GameMain.GameLogic
{
    class BouncingBall
    {
        public Value64 SpriteIndex { get; set; }

        public byte XSpeed { get; set; }
        public byte YSpeed { get; set; }

        public int XPosition { get; set; }
        public int YPosition { get; set; }

        public int Width = 16;
        public int Height = 16;

        public BouncingBall()
        {
            XSpeed = 128;
            YSpeed = 128;
        }

        public void Update(SystemMemory systemMemory)
        {
            XPosition += XSpeed - 128;
            YPosition += YSpeed - 128;

            if (XPosition + Width >= SystemConstants.ScreenPixelWidth &&
                XSpeed > 128)
            {
                XSpeed -= (byte)((XSpeed - 128) * 2);
                XPosition = SystemConstants.ScreenPixelWidth - Width;
            }

            if (XPosition < 0 &&
                XSpeed < 128)
            {
                XSpeed += (byte)((128 - XSpeed) * 2);
                XPosition = 0;
            }

            if (YPosition + Height >= SystemConstants.ScreenPixelHeight &&
                YSpeed > 128)
            {
                YSpeed -= (byte)((YSpeed - 128) * 2);
                YPosition = SystemConstants.ScreenPixelHeight - Height;
            }

            if (YPosition < 0 &&
                YSpeed < 128)
            {
                YSpeed += (byte)((128 - YSpeed) * 2);
                YPosition = 0;
            }

            systemMemory.VideoMemory.Sprites.Get(SpriteIndex).X = (byte)XPosition; //todo, overflow
            systemMemory.VideoMemory.Sprites.Get(SpriteIndex).Y = (byte)YPosition; //todo, overflow
        }
    }
}
