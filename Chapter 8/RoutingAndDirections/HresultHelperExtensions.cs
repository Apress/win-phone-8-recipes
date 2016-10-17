using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingAndDirections
{
    public static class HresultHelperExtensions
    {
        private static Dictionary<RouteQueryHresult, string> RouteQueryErrorStrings;
        private static Dictionary<GeocodeQueryHresult, string> GeocodeQueryErrorStrings;


        static HresultHelperExtensions()
        {
            LoadRouteQueryErrorStrings();

            LoadGeocodeQueryErrorStrings();

        }

        public static string GetRouteQueryHresultDescription(this RouteQueryHresult hresult)
        {
            string errorString = string.Empty;

            if (!RouteQueryErrorStrings.TryGetValue(hresult, out errorString))
            {
                errorString = "Unknown error.";
            }
            
            return errorString;

        }

        public static string GetGeocodeQueryHresultDescription(this GeocodeQueryHresult hresult)
        {
            string errorString = string.Empty;

            if (!GeocodeQueryErrorStrings.TryGetValue(hresult, out errorString))
            {
                errorString = "Unknown error.";
            }

            return errorString;

        }

        private static void LoadGeocodeQueryErrorStrings()
        {
            GeocodeQueryErrorStrings = new Dictionary<GeocodeQueryHresult, string>();
            GeocodeQueryErrorStrings.Add(GeocodeQueryHresult.EErrorBadLocation, "Bad Location");
            GeocodeQueryErrorStrings.Add(GeocodeQueryHresult.EErrorCancelled, "Cancelled");
            GeocodeQueryErrorStrings.Add(GeocodeQueryHresult.EErrorIndexFailure, "Index Failure");
        }
        private static void LoadRouteQueryErrorStrings()
        {
            RouteQueryErrorStrings = new Dictionary<RouteQueryHresult, string>();
            RouteQueryErrorStrings.Add(RouteQueryHresult.EErrorGraphDisconnected, "No route found.");
            RouteQueryErrorStrings.Add(RouteQueryHresult.EErrorGraphDisconnectedCheckOptions, "No route found, some option(for example disabled highways) may be prohibiting it.");
            RouteQueryErrorStrings.Add(RouteQueryHresult.EErrorNoStartPoint, "Start point not found.");
            RouteQueryErrorStrings.Add(RouteQueryHresult.EErrorNoEndPoint, "End point not found.");
            RouteQueryErrorStrings.Add(RouteQueryHresult.EErrorNoEndPointCheckOptions, "End point unreachable, some option (for example disabled highways) may be prohibiting it.");
            RouteQueryErrorStrings.Add(RouteQueryHresult.EErrorCannotDoPedestrian, "Pedestrian mode was set, but cannot do pedestrian route (too long route).");
            RouteQueryErrorStrings.Add(RouteQueryHresult.EErrorRouteUsesDisabledRoads, "Route was calculated but it uses roads, which were disabled by options, note: this error is given also when start direction is violated.");
            RouteQueryErrorStrings.Add(RouteQueryHresult.EErrorRouteCorrupted, "Corrupted route.");
            RouteQueryErrorStrings.Add(RouteQueryHresult.EErrorRouteNotReady, "Route not ready.");
            RouteQueryErrorStrings.Add(RouteQueryHresult.EErrorRouteNotReadyFailedLocally, "Route not ready failed locally.");
            RouteQueryErrorStrings.Add(RouteQueryHresult.EErrorRoutingCancelled, "Routing was cancelled.");
        }
    }
}
