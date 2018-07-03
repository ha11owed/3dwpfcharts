using System;
using System.Windows.Media.Media3D;
using WpfPlot.Model;

namespace WpfPlot.Plot3d
{
    internal static class Helpers
    {
        public static Point3D GetPoint3D(IPoint point)
        {
            Position p = point.Position;
            return new Point3D(p.X, p.Y, p.Z);
        }

        public static Rect3D GetRect3D(Point3D p1, Point3D p2, double margin)
        {
            double marginX2 = margin + margin;
            Rect3D r = new Rect3D(
                Math.Min(p1.X, p2.X) - margin,
                Math.Min(p1.Y, p2.Y) - margin,
                Math.Min(p1.Z, p2.Z) - margin,
                Math.Abs(p1.X - p2.X) + marginX2,
                Math.Abs(p1.Y - p2.Y) + marginX2,
                Math.Abs(p1.Z - p2.Z) + marginX2);
            return r;
        }

        public static Position Move(Position position, Vector3D delta)
        {
            return new Position(position.X + delta.X, position.Y + delta.Y, position.Z + delta.Z);
        }

        public static Position Point3DToPosition(Point3D point)
        {
            return new Position(point.X, point.Y, point.Z);
        }
    }
}