using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;

namespace RoutingAndDirections
{
    public class LocationItem
    {
        private string address;
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        private GeoCoordinate coordinate;
        public GeoCoordinate Coordinate
        {
            get
            {
                return coordinate;
            }
            set
            {
                coordinate = value;
            }
        }
    }
}
