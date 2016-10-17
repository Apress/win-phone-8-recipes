using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using WhereAmI.Resources;
using Windows.Devices.Geolocation;

namespace WhereAmI
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private async Task<GeoCoordinate> GetCurrentCoordinate()
        {
            Geolocator locator = new Geolocator();
            locator.DesiredAccuracy = PositionAccuracy.High;

            Geoposition position = await locator.GetGeopositionAsync();

            GeoCoordinate coordinate = 
                new GeoCoordinate(position.Coordinate.Latitude, position.Coordinate.Longitude);
            return coordinate;
        }

        private async void WhereAmI(object sender, RoutedEventArgs e)
        {
            try
            {
                GeoCoordinate coordinate = await GetCurrentCoordinate();

                MapsTask mapTask = new MapsTask();
                mapTask.ZoomLevel = 15;
                mapTask.Center = coordinate;
                mapTask.Show();
            }
            catch (UnauthorizedAccessException ex)
            {
                // this means that location is disabled on the device.
                MessageBox.Show("Location is disabled.");

            }
        }
    }
}