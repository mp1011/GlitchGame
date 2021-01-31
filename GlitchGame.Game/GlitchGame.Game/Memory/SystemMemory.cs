using GlitchGame.GameMain.Graphics;

namespace GlitchGame.GameMain.Memory
{
    public static class Address
    {
        public static readonly int Clock = 0;
        public static readonly int GameLogic = 4000; //todo
    }

   

    public class SystemMemory
    {
        public VideoMemory VideoMemory { get; }

        public byte[] Clock
        {
            get 
            {
                SystemBinaryData.SetIOPointer(Address.Clock, 0);
                return SystemBinaryData.ReadBytes(2);
            }
            set
            {
                SystemBinaryData.SetIOPointer(Address.Clock, 0);
                SystemBinaryData.WriteBytes(value);
            }
        }

        public SystemMemory(VideoMemory videoMemory)
        {
            VideoMemory = videoMemory;
        }
    }
}
