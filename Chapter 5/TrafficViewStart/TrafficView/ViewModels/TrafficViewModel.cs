using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Linq;
using Microsoft.Phone.Shell;


namespace TrafficView.ViewModels
{
    public class TrafficViewModel : INotifyPropertyChanged
    {

        #region Properties
        private ObservableCollection<TrafficItemViewModel> trafficItems;
        public ObservableCollection<TrafficItemViewModel> TrafficItems
        {
            get
            {
                return trafficItems;
            }
            set
            {
                trafficItems = value;
                OnPropertyChanged("TrafficItems");
            }
        }
        
        private TrafficItemViewModel currentTrafficView;
        public TrafficItemViewModel CurrentTrafficView
        {
            get
            {
                return currentTrafficView;
            }
            set
            {
                currentTrafficView = value;
                OnPropertyChanged("CurrentTrafficView");

            }
        }

        #endregion

        public TrafficViewModel()
        {
            LoadTrafficList();
            this.CurrentTrafficView = this.TrafficItems.FirstOrDefault();
        }
        
        /// <summary>
        /// Loads the traffic list.
        /// </summary>
        private void LoadTrafficList()
        {
            TrafficItems = new ObservableCollection<TrafficItemViewModel>();
            TrafficItems.Add(new TrafficItemViewModel { Description = "E.C. Row near Lauzon Pkwy", ImageName = "loc27.jpg" });
            TrafficItems.Add(new TrafficItemViewModel { Description = "Howard near Hwy 3", ImageName = "loc10.jpg" });
            TrafficItems.Add(new TrafficItemViewModel { Description = "Hwy 401 near Hwy 3", ImageName = "loc89.jpg" });
            TrafficItems.Add(new TrafficItemViewModel { Description = "St. Clair College near Hwy 3", ImageName = "loc08.jpg" });
        }
        
       
        #region Notify Property Changed Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
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
