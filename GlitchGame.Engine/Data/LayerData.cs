using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitchGame.Engine.Data
{
    public class LayerData : BitBlockSequence
    {
        public ScreenLayout ScreenLayout { get; }
        public UILayer UILayer { get; } = new UILayer();
        public BackgroundLayers BackgroundLayers { get; } = new BackgroundLayers();

        public Coordinates ScrollAmount { get; }

        public LayerData()
        {
            ScrollAmount = new Coordinates(ScreenLayout.GetHorizontalPixels(),
                ScreenLayout.GetVerticalPixels());
        }

        protected override IEnumerable<IBitBlock> GetSections()
        {
            return new IBitBlock[]
            {
                ScreenLayout,
                ScrollAmount,
                UILayer,
                BackgroundLayers
            };
        }
    }
}
