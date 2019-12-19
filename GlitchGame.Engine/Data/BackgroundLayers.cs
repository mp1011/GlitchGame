using GlitchGame.Engine.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitchGame.Engine.Data
{
    public class BackgroundLayers : BitBlockArray<BackgroundLayer>
    {
        protected override BackgroundLayer[] Elements { get; }

        public BackgroundLayers()
        {
            Elements = new BackgroundLayer[Settings.BackgroundLayers].FillDefault();
        }
    }
}
