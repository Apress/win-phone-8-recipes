using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Device.Location;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Services;
using Microsoft.Phone.Maps.Toolkit;
using Microsoft.Phone.Shell;
using RoutingAndDirections.Resources;
using RoutingAndDirections.ViewModels;
using Windows.Devices.Geolocation;
using Windows.System;

namespace RoutingAndDirections
{
    public partial class MainPage : PhoneApplicationPage
    {
        private List<GeoCoordinate> coordinates = new List<GeoCoordinate>();
        private GeocodeQuery codeQuery;
        private GeoCoordinate currentLocation;

        public MainPage()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
        }



        #region location code

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                currentLocation = await GetCurrentLocation();
                App.ViewModel.WayPoints.Clear();
                App.ViewModel.WayPoints.Add(new LocationItem
                {
                    Address = "My Location",
                    Coordinate = currentLocation
                });

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

        private async Task<GeoCoordinate> GetCurrentLocation()
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
                currentLocation = await GetCurrentLocation();

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
        #endregion

        private void LocationSearchClick(object sender, EventArgs e)
        {
            App.ViewModel.SearchControlsEnabled = false;
            App.ViewModel.SetUpListVisibility = Visibility.Visible;

            try
            {
                codeQuery = new GeocodeQuery();
                codeQuery.SearchTerm = locationSearchText.Text;
                codeQuery.GeoCoordinate = currentLocation;
                codeQuery.QueryCompleted += GeocodeQueryQueryCompleted;
                codeQuery.QueryAsync();
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("location is disabled");
                App.ViewModel.SearchControlsEnabled = true;
            }

        }

        void GeocodeQueryQueryCompleted(object sender, QueryCompletedEventArgs<IList<MapLocation>> e)
        {
            App.ViewModel.LocationItems.Clear();
            if (e.Error != null || e.Result.Count == 0)
            {
                string errorText = string.Empty;
                if (e.Error != null)
                {
                    GeocodeQueryHresult hresult = (GeocodeQueryHresult)e.Error.HResult;
                    errorText = hresult.GetGeocodeQueryHresultDescription();

                }
                else
                {
                    errorText = "Location not found";
                }

                App.ViewModel.LocationItems.Add(new LocationItem { Address = errorText });
            }
            else
            {
                App.ViewModel.LocationItems.Clear();
                foreach (var result in e.Result)
                {
                    MapAddress address = result.Information.Address;
                    string addressText = string.Format("{0} {1}, {2}",
                        address.HouseNumber, address.Street, address.City);
                    LocationItem item = new LocationItem
                    {
                        Address = addressText,
                        Coordinate = result.GeoCoordinate
                    };
                    App.ViewModel.LocationItems.Add(item);
                    App.ViewModel.SearchControlsEnabled = true;
                }
            }
            App.ViewModel.SearchControlsEnabled = true;
        }

        void RouteQueryQueryCompleted(object sender, QueryCompletedEventArgs<Route> e)
        {
            App.ViewModel.Maneuvers.Clear();
            try
            {
                if (e.Error != null)
                {
                    RouteQueryHresult hresult = (RouteQueryHresult)e.Error.HResult;
                    string errorText = hresult.GetRouteQueryHresultDescription();
                }
                else
                {
                    Route route = e.Result;
                    for (int j = 0; j < route.Legs.Count; j++)
                    {

                        string wayPoint = App.ViewModel.WayPoints[j].Address;
                        List<string> manuvers = (from man in route.Legs[j].Maneuvers
                                                 select man.InstructionText).ToList();

                        App.ViewModel.Maneuvers.Add(
                            new ObservableLongListGroup<string>(manuvers, wayPoint));
                    }
                }
            }
            finally
            {
                App.ViewModel.SearchControlsEnabled = true;
            }
        }

        private void LocationListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected item is null (no selection) do nothing
            if (locationList.SelectedItem == null)
                return;

            //coordinates.Add(((LocationItem)locationList.SelectedItem).Coordinate);
            App.ViewModel.WayPoints.Add(locationList.SelectedItem as LocationItem);
            App.ViewModel.LocationItems.Clear();

        }

        private void RouteClick(object sender, EventArgs e)
        {
            App.ViewModel.SetUpListVisibility = Visibility.Collapsed;
            App.ViewModel.SearchControlsEnabled = false;
            RouteQuery routeQuery = new RouteQuery();
            coordinates = (from point in App.ViewModel.WayPoints
                           select point.Coordinate).ToList();

            routeQuery.Waypoints = coordinates;
            routeQuery.QueryCompleted += RouteQueryQueryCompleted;
            routeQuery.QueryAsync();
        }

    }
}