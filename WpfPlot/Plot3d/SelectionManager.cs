using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using WpfPlot.Model;

namespace WpfPlot.Plot3d
{
    internal class SelectionManager
    {
        private IPlot _plot;
        private Point3D? _pointEnd;
        private Point3D? _pointStart;
        private IPoint[] _selectedPoints;

        public SelectionManager(IPlot plot)
        {
            _plot = plot;
            Clear();
        }

        public IPoint[] SelectedPoints
        {
            get
            {
                if (_selectedPoints == null)
                {
                    List<IPoint> selectedPoints = new List<IPoint>();
                    foreach (ISeries series in _plot.Series)
                    {
                        foreach (IPoint point in series.Points)
                        {
                            if (point.IsSelected)
                                selectedPoints.Add(point);
                        }
                    }
                    _selectedPoints = selectedPoints.ToArray();
                }
                return _selectedPoints;
            }
        }

        public void Clear()
        {
            _pointStart = null;
            _pointEnd = null;
            _selectedPoints = null;
        }

        public void Start(Point3D start)
        {
            _pointStart = start;
            _pointEnd = start;
            _selectedPoints = null;
        }

        public void Update(Point3D end)
        {
            _pointEnd = end;
            Rect3D rect = Helpers.GetRect3D(_pointStart.Value, _pointEnd.Value, 0.5);

            List<ISelectable> selection = new List<ISelectable>();
            foreach (ISeries series in _plot.Series)
            {
                foreach (IPoint item in series.Points)
                {
                    Point3D p3d = Helpers.GetPoint3D(item);
                    if (rect.Contains(p3d))
                    {
                        selection.Add(item);
                    }
                }
            }

            bool changed = _plot.SetSelection(selection);
            if (changed)
                _selectedPoints = null;
        }
    }
}