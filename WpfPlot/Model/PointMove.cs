namespace WpfPlot.Model
{
    public class PointMove
    {
        public PointMove(IPoint point, Position newPosition)
        {
            Point = point;
            NewPosition = newPosition;
        }

        public Position NewPosition { get; private set; }
        public IPoint Point { get; private set; }
    }
}