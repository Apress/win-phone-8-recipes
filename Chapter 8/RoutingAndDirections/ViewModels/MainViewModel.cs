using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RoutingAndDirections.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {

        private bool searchControlsEnabled;
        public bool SearchControlsEnabled
        {
            get
            {
                return searchControlsEnabled;
            }
            set
            {
                searchControlsEnabled = value;
                NotifyPropertyChanged("SearchControlsEnabled");
            }
        }

        private Visibility setUpListVisibility = Visibility.Visible;
        public Visibility SetUpListVisibility
        {
            get
            {
                return setUpListVisibility;
            }
            set
            {
                setUpListVisibility = value;
                NotifyPropertyChanged("SetUpListVisibility");
            }
        }


        private ObservableCollection<ObservableLongListGroup<string>> maneuvers = new ObservableCollection<ObservableLongListGroup<string>>();
        public ObservableCollection<ObservableLongListGroup<string>> Maneuvers
        {
            get
            {
                return maneuvers;
            }
            set
            {
                maneuvers = value;
            }
        }

        private ObservableCollection<LocationItem> wayPoints = new ObservableCollection<LocationItem>();
        public ObservableCollection<LocationItem> WayPoints
        {
            get
            {
                return wayPoints;
            }
            set
            {
                wayPoints = value;
            }
        }

        private ObservableCollection<LocationItem> locationItems = new ObservableCollection<LocationItem>();
        public ObservableCollection<LocationItem> LocationItems
        {
            get
            {
                return locationItems;
            }
            set
            {
                locationItems = value;
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }
}
