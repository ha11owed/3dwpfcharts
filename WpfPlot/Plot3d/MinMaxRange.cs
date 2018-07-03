using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPlot.Plot3d
{
    internal class MinMaxRange
    {
        public MinMaxRange()
        {
            Min = double.MaxValue;
            Max = double.MinValue;
        }

        public double Max { get; set; }
        public double Min { get; set; }

        public void Update(double value)
        {
            Min = Math.Min(Min, value);
            Max = Math.Min(Min, value);
        }
    }
}