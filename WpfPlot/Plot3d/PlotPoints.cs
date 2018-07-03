using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using WpfPlot.Model;

namespace WpfPlot.Plot3d
{
    internal class PlotPoints
    {
        private readonly Dictionary<Model3D, IPoint> _modelToPoint = new Dictionary<Model3D, IPoint>();
        private readonly Material _mtLineDefault = MaterialHelper.CreateMaterial(Colors.Gray);
        private readonly Material _mtLineSelected = MaterialHelper.CreateMaterial(Colors.Red);
        private readonly Material _mtPointDefault = MaterialHelper.CreateMaterial(Colors.Gold);
        private readonly Material _mtPointSelected = MaterialHelper.CreateMaterial(Colors.Red);

        public void AddPoints(IPlot plot, Model3DGroup group)
        {
            HashSet<double> selectedX = new HashSet<double>();
            HashSet<double> selectedY = new HashSet<double>();

            double minX = double.MaxValue, maxX = double.MinValue;
            double minY = double.MaxValue, maxY = double.MinValue;

            foreach (ISeries series in plot.Series)
            {
                IPoint[] points = series.Points;

                for (int i = 0; i < points.Length; i++)
                {
                    IPoint item = points[i];
                    Point3D p3d = Helpers.GetPoint3D(item);
                    minX = Math.Min(minX, p3d.X);
                    minY = Math.Min(minY, p3d.Y);
                    maxX = Math.Max(maxX, p3d.X);
                    maxY = Math.Max(maxY, p3d.Y);

                    // Add the point
                    MeshBuilder mb = new MeshBuilder();
                    mb.AddSphere(p3d, 0.5);
                    //mb.AddBox(p3d, 1, 1, 1, BoxFaces.All);
                    GeometryModel3D pointModel = new GeometryModel3D(mb.ToMesh(), item.IsSelected ? _mtPointSelected : _mtPointDefault);

                    if (item.IsSelected)
                    {
                        selectedX.Add(item.Position.X);
                        selectedY.Add(item.Position.Y);
                    }

                    _modelToPoint[pointModel] = item;
                    group.Children.Add(pointModel);
                }
            }

            foreach (ISeries series in plot.Series)
            {
                IPoint[] points = series.Points;

                for (int i = 1; i < points.Length; i++)
                {
                    IPoint from = points[i - 1];
                    IPoint to = points[i];

                    if (from.Position.X != to.Position.X && from.Position.Y != to.Position.Y)
                        continue;

                    if (!selectedX.Contains(from.Position.X) || !selectedX.Contains(to.Position.X))
                        continue;

                    Point3D p3dFrom = Helpers.GetPoint3D(from);
                    Point3D p3dTo = Helpers.GetPoint3D(to);

                    MeshBuilder mb = new MeshBuilder();
                    mb.AddPipe(p3dFrom, p3dTo, 0, 0.01, 5);

                    bool isSelected = from.IsSelected && to.IsSelected;
                    GeometryModel3D lineModel = new GeometryModel3D(mb.ToMesh(), isSelected ? _mtLineSelected : _mtLineDefault);
                    group.Children.Add(lineModel);
                }
            }
        }

        public void Clear()
        {
            _modelToPoint.Clear();
        }

        public IPoint GetPoint(Model3D model)
        {
            IPoint point;
            _modelToPoint.TryGetValue(model, out point);
            return point;
        }
    }
}