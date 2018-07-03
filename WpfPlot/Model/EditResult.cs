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