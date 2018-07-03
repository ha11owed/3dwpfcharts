// Copyright (c) 2018, ASK Industries GmbH, Niederwinkling.
// All rights reserved.
//
// $Id$

using System;

namespace WpfPlot.Model
{
    [Flags]
    public enum EditResult
    {
        Done,
        DoneRestricted,
        NotAllowed,
        Invalid,
    }
}