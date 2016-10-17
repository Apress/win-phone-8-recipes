using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingAndDirections
{
    public enum RouteQueryHresult : uint
    {
        EErrorGraphDisconnected = 0x80042328,
        EErrorGraphDisconnectedCheckOptions = 0x80042327,
        EErrorNoStartPoint = 0x80042326,
        EErrorNoEndPoint = 0x80042325,
        EErrorNoEndPointCheckOptions = 0x80042324,
        EErrorCannotDoPedestrian = 0x80042323,
        EErrorRouteUsesDisabledRoads = 0x80042322,
        EErrorRouteCorrupted = 0x80042321,
        EErrorRouteNotReady = 0x80042320,
        EErrorRouteNotReadyFailedLocally = 0x8004231F,
        EErrorRoutingCancelled = 0x8004231E
    }
}
