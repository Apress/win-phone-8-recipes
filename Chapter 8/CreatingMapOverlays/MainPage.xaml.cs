using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Maps.Controls;
using System.Device.Location;
using System.Windows.Input;
using Windows.System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Microsoft.Phone.Maps.Toolkit;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CreatingMapOverlays
{
    public partial class MainPage : PhoneApplicationPage
    {
        private MapLayer layer;
        // Constructor
        public MainPage()
        {
            InitializeComponent();

        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                GeoCoordinate coordinate = await GetCoordinate();
                theMap.Center = coordinate;
                theMap.ZoomLevel = 15;

                this.UserLocationMarker = (UserLocationMarker)this.FindName("UserLocationMarker");
                this.UserLocationMarker.GeoCoordinate = coordinate;
                this.UserLocationMarker.Visibility = System.Windows.Visibility.Visible;
                
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

            coordinate = new GeoCoordinate(position.Coordinate.Latitude, 
                position.Coordinate.Longitude);
            return coordinate;
        }

        private async void UpdateLocation(object sender, EventArgs e)
        {
            try
            {
                ((ApplicationBarIconButton)sender).IsEnabled = false;
                GeoCoordinate coordinate = await GetCoordinate();
                theMap.Center = coordinate;
                this.UserLocationMarker.GeoCoordinate = coordinate;

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

        private void TheMapTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            // create you favorite image using and 
            // add it to a new MapOverlay
            Image image = new Image();
            image.Source = new BitmapImage(new Uri("Assets/footprint.png", 
                                                    UriKind.RelativeOrAbsolute));
            
            MapOverlay overlay = new MapOverlay();
            overlay.Content = image;

            // get the position that the user tapped
            Point point = e.GetPosition(theMap);

            // the favs icon is 76 x 76 and we want to
            // center it on the point the user tapped
            // so we'll shift the point 
            point.X -= 38;
            point.Y -= 38;

            // convert the ViewPort point into an actual GeoCoordinate value
            GeoCoordinate coordinate = theMap.ConvertViewportPointToGeoCoordinate(point);
            overlay.GeoCoordinate = coordinate;

            // the one and only layer that we
            // add all of the overlays to
            if (layer == null)
            {
                layer = new MapLayer();
                theMap.Layers.Add(layer);
            }
            layer.Add(overlay);
        }
    }
}