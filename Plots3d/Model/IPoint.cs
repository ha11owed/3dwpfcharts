// Copyright (c) 2018, ASK Industries GmbH, Niederwinkling.
// All rights reserved.
//
// $Id$

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace WpfPlot.Model
{
    public interface IPoint : ISelectable
    {
        string Description { get; }

        Position Position { get; }

        object Tag { get; }
    }
}