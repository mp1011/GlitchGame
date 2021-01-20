using GlitchGame.GameMain.Memory;

namespace GlitchGame.GameMain.GameLogic
{
    class ScrollingSprite
    {
        public int WorldX { get; set; }

        public int WorldY { get; set; }

        public Value64 SpriteIndex { get; set; }

        public void Update(SystemMemory systemMemory)
        {

            var screenX = WorldX - systemMemory.VideoMemory.BgLayer.XOffset;

            //todo
            var screenY = WorldY;

            systemMemory.VideoMemory.Sprites.Get(SpriteIndex).X = (byte)screenX; //todo, overflow
            systemMemory.VideoMemory.Sprites.Get(SpriteIndex).Y = (byte)screenY; //todo, overflow

        }
    }
}
