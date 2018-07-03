namespace WpfPlot.Model
{
    public interface ISeries : ISelectable
    {
        bool IsActive { get; }

        IPoint[] Points { get; }
    }
}