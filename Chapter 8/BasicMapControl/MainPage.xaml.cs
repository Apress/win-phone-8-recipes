using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using System.Device.Location;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Windows.System;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Controls.Primitives;

namespace BasicMapControl
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {
                theMap.Center = await GetCoordinate();
                zoomSlider.Value = 15;
            }
            catch (Exception)
            {
                MessageBoxResult result = MessageBox.Show("Location services is not enabled.  Would you like to go to the settings page to enable it?", "Error", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    Launcher.LaunchUriAsync(new Uri("ms-settings-location:"));
                }
            }
        }

        private async Task<GeoCoordinate> GetCoordinate()
        {

            Geolocator locator = new Geolocator();
            GeoCoordinate coordinate;
            locator.DesiredAccuracy = PositionAccuracy.High;


            Geoposition position = await locator.GetGeopositionAsync();

            coordinate = new GeoCoordinate(position.Coordinate.Latitude, position.Coordinate.Longitude);
            return coordinate;
        }

        private async void UpdateLocation(object sender, EventArgs e)
        {
            try
            {
                ((ApplicationBarIconButton)sender).IsEnabled = false;
                GeoCoordinate coordinate = await GetCoordinate();
                theMap.Center = coordinate;
            }
            catch (Exception)
            {
                MessageBoxResult result = MessageBox.Show("Location services is not enabled.  Would you like to go to the settings page to enable it?", "Error", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    Launcher.LaunchUriAsync(new Uri("ms-settings-location:"));
                }
            }
            finally
            {
                ((ApplicationBarIconButton)sender).IsEnabled = true;
            }
        }

        private void ZoomSliderValueChanged(object sender, 
            RoutedPropertyChangedEventArgs<double> e)
        {
            if (theMap != null)
            {
                theMap.ZoomLevel = e.NewValue;
            }
        }

        private void PitchSliderValueChanged(object sender, 
            RoutedPropertyChangedEventArgs<double> e)
        {
            if (theMap != null)
            {
                theMap.Pitch = e.NewValue;
            }
        }

        private void HeadingSliderValueChanged(object sender, 
            RoutedPropertyChangedEventArgs<double> e)
        {
            if (theMap != null)
            {
                theMap.Heading = e.NewValue;
            }
        }

        private void ShowSettings(object sender, EventArgs e)
        {
            settingsPanel.Visibility = 
                settingsPanel.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void ToggleColorMode(object sender, RoutedEventArgs e)
        {
            ((Button)(sender)).Content = theMap.ColorMode.ToString();
            theMap.ColorMode = theMap.ColorMode == MapColorMode.Dark ? MapColorMode.Light :
                                                                       MapColorMode.Dark;
        }

        private void CycleMapCartographicMode(object sender, RoutedEventArgs e)
        {
            switch (theMap.CartographicMode)
            {
                case MapCartographicMode.Aerial:
                    theMap.CartographicMode = MapCartographicMode.Hybrid;
                    break;
                case MapCartographicMode.Hybrid:
                    theMap.CartographicMode = MapCartographicMode.Road;
                    break;
                case MapCartographicMode.Road:
                    theMap.CartographicMode = MapCartographicMode.Terrain;
                    break;
                case MapCartographicMode.Terrain:
                    theMap.CartographicMode = MapCartographicMode.Aerial;
                    break;
            }
        }


    }
}