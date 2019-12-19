using GlitchGame.Engine.Extensions;

namespace GlitchGame.Engine.Data
{
    public class SpriteGroup : BitBlockArray<Sprite>
    {
        protected override Sprite[] Elements { get; }

        public SpriteGroup()
        {
            Elements = new Sprite[Settings.TotalSprites].FillDefault();
        }
    }
}
