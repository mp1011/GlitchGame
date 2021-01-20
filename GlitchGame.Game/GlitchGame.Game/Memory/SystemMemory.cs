using GlitchGame.GameMain.Graphics;

namespace GlitchGame.GameMain.Memory
{
    public class SystemMemory
    {
        public VideoMemory VideoMemory { get; }

        public int Clock { get; set; } //todo

        public SystemMemory(VideoMemory videoMemory)
        {
            VideoMemory = videoMemory;
        }
    }
}
