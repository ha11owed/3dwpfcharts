namespace WpfPlot.Model
{
    public interface IPoint : ISelectable
    {
        string Description { get; }

        Position Position { get; }

        object Tag { get; }
    }
}