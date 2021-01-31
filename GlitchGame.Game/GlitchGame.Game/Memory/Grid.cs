namespace GlitchGame.GameMain.Memory
{
    public abstract class Grid<T> : Sequence<T>
        where T:IDataBlock, new()
    {
        public abstract int Columns { get; }

        public abstract int Rows { get; }

        public sealed override int Length => Columns * Rows;

        public Grid() : base()
        {
        }


        public Grid(T[] items) : base(items[0])
        {
        }

        public void SetWritePointer(int x, int y)
        {
            SetWritePointer((y * Columns) + x);
        }

        public T GetFromCoordinates(int x, int y)
        {
            return Get((y * Columns) + x);
        }

        public PrecisionAddress GetAddressFromCoordinates(int x, int y)
        {
            return GetAddress((y * Columns) + x);
        }

        public void SetFromCoordinates(int x, int y, T value)
        {
            //array[(y * columns) + x] = value;
            throw new System.NotImplementedException();
        }
    }
}
