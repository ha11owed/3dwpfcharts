using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfPlot.Model;

namespace WpfPlot.Plot3d
{
    public class PlotAxes
    {
        public BillboardTextItem[] Labels { get; private set; }

        public void Create(IPlot plot)
        {
            MinMaxRange xRange = new MinMaxRange();
            MinMaxRange yRange = new MinMaxRange();
            MinMaxRange zRange = new MinMaxRange();

            foreach (ISeries series in plot.Series)
            {
                foreach (IPoint point in series.Points)
                {
                    xRange.Update(point.Position.X);
                    yRange.Update(point.Position.Y);
                    zRange.Update(point.Position.Z);
                }
            }

            //foreach( xRange.Min =
        }
    }
}