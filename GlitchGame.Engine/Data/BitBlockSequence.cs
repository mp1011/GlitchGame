using System.Collections.Generic;
using System.Linq;

namespace GlitchGame.Engine.Data
{
    public abstract class BitBlockSequence : IBitBlock
    {
        public int BitSize => GetSections()
                                .Sum(p => p.BitSize);

        protected abstract IEnumerable<IBitBlock> GetSections();
    }
}
