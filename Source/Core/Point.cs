namespace Monkiato.Core
{
    public class Point
    {
        private Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }

        public int Y { get; }

        public static Point New(int x, int y)
        {
            return new Point(x, y);
        }
    }
}