using System.Collections.Generic;

namespace GlitchGame.Engine.Data
{
    public class RAM : BitBlockSequence
    {
        public VRAM VRAM { get; } = new VRAM();
        public LayerData LayerData { get; } = new LayerData();

        public SpriteGroup Sprites { get; } = new SpriteGroup();

        public GeneralData GeneralData { get; } = new GeneralData(Settings.GeneralDataBits);

        protected override IEnumerable<IBitBlock> GetSections()
        {
            return new IBitBlock[]
            {
                VRAM,
                LayerData,
                Sprites,
                GeneralData
            };
        }
    }
}
