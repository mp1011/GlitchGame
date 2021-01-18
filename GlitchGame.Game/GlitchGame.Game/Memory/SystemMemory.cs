using GlitchGame.GameMain.Graphics;

namespace GlitchGame.GameMain.Memory
{
    public class SystemMemory
    {
        public VideoMemory VideoMemory { get; }

        public SystemMemory(VideoMemory videoMemory)
        {
            VideoMemory = videoMemory;
        }
    }
}
