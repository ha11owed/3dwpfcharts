using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfPlot.Model.Impl
{
    internal class Plot : IPlot
    {
        public Plot()
        {
            Series s1 = new Series();
            int n = 10;
            s1.Points = new Point[n * n];

            Random rand = new Random();

            int k = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    s1.Points[k++] = new Point() { Position = new Position(i * 2, j * 2, rand.Next(-2, 10) * 0.5) };
                }
            };
            Series = new Series[] { s1 };
        }

        public ISeries[] Series
        {
            get; set;
        }

        public EditResult AddPoints(PointAdd[] adds)
        {
            throw new NotImplementedException();
        }

        public EditResult DeletePoints(IPoint[] deletes)
        {
            throw new NotImplementedException();
        }

        public EditResult EditPoints(PointEdit[] edits)
        {
            throw new NotImplementedException();
        }

        public EditResult MovePoints(PointMove[] moves)
        {
            foreach (PointMove move in moves)
            {
                Point p = (Point)move.Point;
                p.Position = move.NewPosition;
            }
            return EditResult.Done;
        }

        public bool SetSelection(IEnumerable<ISelectable> selection)
        {
            bool changed = false;
            foreach (ISeries series in Series)
            {
                foreach (Point point in series.Points)
                {
                    bool isSelected = selection.Contains(point);
                    if (point.IsSelected != isSelected)
                    {
                        point.IsSelected = isSelected;
                        changed = true;
                    }
                }
            }
            return changed;
        }
    }
}