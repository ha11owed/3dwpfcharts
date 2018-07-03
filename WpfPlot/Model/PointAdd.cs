namespace WpfPlot.Model
{
    public class PointAdd
    {
        public PointAdd(Position position, ISeries series, object tag)
        {
            Position = position;
            Series = series;
            Tag = tag;
        }

        public Position Position { get; private set; }
        public ISeries Series { get; private set; }
        public object Tag { get; private set; }
    }
}