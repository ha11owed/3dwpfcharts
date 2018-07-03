// Copyright (c) 2018, ASK Industries GmbH, Niederwinkling.
// All rights reserved.
//
// $Id$

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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