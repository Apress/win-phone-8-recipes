using System;
using System.Collections.Generic;
using System.Linq;

namespace RoutingAndDirections
{
    public enum GeocodeQueryHresult : uint
    {
        EErrorBadLocation = 0x80041B58,
        EErrorIndexFailure = 0x80041B57,
        EErrorCancelled = 0x80041B56
    }
}
