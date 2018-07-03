using System;

namespace WpfPlot.Model.Impl
{
    internal class Series : ISeries
    {
        public bool IsActive
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsSelected
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IPoint[] Points
        {
            get; set;
        }
    }
}