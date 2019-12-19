using System.Collections.Generic;

namespace GlitchGame.Engine.Data
{
    public class Color : BitBlockSequence
    {
        public ColorChannel Red { get; } 
        public ColorChannel Green { get; }
        public ColorChannel Blue { get; }

        protected override IEnumerable<IBitBlock> GetSections()
        {
            return new IBitBlock[] { Red, Green, Blue };
        }
    }
}
