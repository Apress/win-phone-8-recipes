using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MyAppsLauncher.Resources;
using System.Collections.Generic;
using Windows.ApplicationModel;
using Windows.Phone.Management.Deployment;
using System.Linq;

namespace MyAppsLauncher.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {           
        }

        private ObservableCollection<Package> installedApps = new ObservableCollection<Package>();
        public ObservableCollection<Package> InstalledApps 
        {
            get
            {
                return installedApps;
            }
            set
            {
                installedApps = value;
                NotifyPropertyChanged("InstalledApps");
            
            }
        }

        
        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public void LoadData()
        {
            //use Linq to exclude the current application from the selection list
            IEnumerable<Package> appPackages = InstallationManager.FindPackagesForCurrentPublisher()
                .Where(p => p.Id.Name != "MyAppsLauncher")
                .AsEnumerable<Package>();
            this.InstalledApps = new ObservableCollection<Package>(appPackages);
                        
            this.IsDataLoaded = true;
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