namespace WpfPlot.Model.Impl
{
    internal class Point : IPoint
    {
        public string Description { get; set; }

        public bool IsSelected { get; set; }

        public Position Position { get; set; }

        public object Tag { get; set; }
    }
}