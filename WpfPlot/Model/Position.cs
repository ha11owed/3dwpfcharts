namespace WpfPlot.Model
{
    public class Position
    {
        public Position(double x, double y)
        {
            X = x;
            Y = y;
            Z = 0;
        }

        public Position(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }
    }
}