using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WeatherAlerts.DataProvider;

namespace WeatherAlerts.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        AlertsDataProvider dataProvider = new AlertsDataProvider();
            
        public MainViewModel()
        {
            this.WeatherAlerts = new ObservableCollection<string>();
            dataProvider.AlertsDownloaded += dataProvider_AlertsDownloaded;                
        }

        public void LoadAlerts()
        {
            dataProvider.LoadData();  
        }

        void dataProvider_AlertsDownloaded(object sender, EventArgs e)
        {
            this.WeatherAlerts = new ObservableCollection<string>(dataProvider.WeatherAlerts);
            this.AlertLocation = dataProvider.AlertLocation;
            this.IsDataLoaded = true;
        }        

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        private string alertLocation;
        public string AlertLocation
        {
            get
            {
                return alertLocation;
            }
            set
            {
                alertLocation = value;
                NotifyPropertyChanged("AlertLocation");
            }
        }


        private ObservableCollection<string> weatherAlerts;
        public ObservableCollection<string> WeatherAlerts
        {
            get
            {
                return weatherAlerts;
            }
            set
            {
                weatherAlerts = value;
                NotifyPropertyChanged("WeatherAlerts");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
