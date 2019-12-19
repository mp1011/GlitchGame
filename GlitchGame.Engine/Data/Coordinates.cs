using System.Collections.Generic;

namespace GlitchGame.Engine.Data
{
    public class Coordinates : BitBlockSequence
    {
        public Coordinate X { get; }
        public Coordinate Y { get; }

        public Coordinates(int horizontalRange, int verticalRange)
        {
            X = new Coordinate(horizontalRange);
            Y = new Coordinate(verticalRange);
        }

        protected override IEnumerable<IBitBlock> GetSections()
        {
            return new IBitBlock[]
            {
                X,
                Y
            };
        }
    }
}
