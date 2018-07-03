using System.Collections.Generic;

namespace WpfPlot.Model
{
    public interface IPlot
    {
        ISeries[] Series { get; }

        EditResult AddPoints(PointAdd[] adds);

        EditResult DeletePoints(IPoint[] deletes);

        EditResult EditPoints(PointEdit[] edits);

        EditResult MovePoints(PointMove[] moves);

        bool SetSelection(IEnumerable<ISelectable> selection);
    }
}