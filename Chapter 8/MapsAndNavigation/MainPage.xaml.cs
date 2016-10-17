using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MapsAndNavigation.Resources;
using Microsoft.Phone.Tasks;
using System.Device.Location;
using System.Diagnostics;
using Windows.Devices.Geolocation;

namespace MapsAndNavigation
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private async void WhereAmI(object sender, RoutedEventArgs e)
        {
            try
            {
                Geolocator locator = new Geolocator();
                locator.DesiredAccuracy = PositionAccuracy.High;

                Geoposition position = await locator.GetGeopositionAsync();

                GeoCoordinate coordinate = new GeoCoordinate(position.Coordinate.Latitude, position.Coordinate.Longitude);

                MapsTask mapTask = new MapsTask();
                mapTask.Center = coordinate;
                mapTask.Show();
            }
            catch (UnauthorizedAccessException ex)
            {
                // this means that location is disabled on the device.
                MessageBox.Show("Location is disabled.");

            }

        }

        private void WhereIsIt(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(locationToFind.Text))
            {
                MessageBox.Show("Please enter a location.");
            }
            else
            {
                MapsTask mapTask = new MapsTask();
                mapTask.ZoomLevel = 15;
                mapTask.SearchTerm = locationToFind.Text;
                mapTask.Show();
            }
        }

        private void HowToGetThere(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(locationToFind.Text))
            {
                MessageBox.Show("Please enter a location.");
            }
            else
            {
                MapsDirectionsTask mapsDirectionsTask = new MapsDirectionsTask();
                mapsDirectionsTask.End = new LabeledMapLocation(locationToFind.Text, null);
                mapsDirectionsTask.Show();
            }

        }

        private void DownloadMaps(object sender, RoutedEventArgs e)
        {
            MapDownloaderTask mapDownloaderTask = new MapDownloaderTask();
            mapDownloaderTask.Show();

        }

        private void UpdateMaps(object sender, RoutedEventArgs e)
        {
            MapUpdaterTask mapUpdaterTask = new MapUpdaterTask();
            mapUpdaterTask.Show();
        }
    }
}