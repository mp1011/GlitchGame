namespace GlitchGame.Engine.Data
{
    public class GeneralData : IBitBlock
    {
        public int BitSize { get; }

        public GeneralData(int bitSize)
        {
            BitSize = bitSize;
        }
    }
}
