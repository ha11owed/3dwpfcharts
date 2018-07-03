// Copyright (c) 2018, ASK Industries GmbH, Niederwinkling.
// All rights reserved.
//
// $Id$

namespace WpfPlot.Model
{
    public interface ISeries : ISelectable
    {
        bool IsActive { get; }

        IPoint[] Points { get; }
    }
}