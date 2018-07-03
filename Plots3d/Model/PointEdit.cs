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
    public class PointEdit
    {
        public IPoint Point { get; set; }
        public ISeries Series { get; set; }
        public object Tag { get; set; }
    }
}