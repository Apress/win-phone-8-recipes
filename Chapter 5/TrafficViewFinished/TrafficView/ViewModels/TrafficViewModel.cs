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

        #region Constants        
        private const string currentViewKey = "currentTrafficView";
        private const string pinnedTileKey = "pinnedTile";
        private const string mainPageUri = "/MainPage.xaml?state=pinned";
        #endregion

        #region Private variables
        private ShellTileSchedule ugTileSchedule;
        #endregion 

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

        private TrafficItemViewModel pinnedTile;
        public TrafficItemViewModel PinnedTile
        {
            get
            {
                return pinnedTile;
            }
            set
            {
                pinnedTile = value;
                OnPropertyChanged("PinnedTile");
            }
        }
        #endregion

        #region Constructor
        public TrafficViewModel()
        {
            LoadTrafficList();
            LoadLastAppState();
        }
        #endregion
                
        /// <summary>
        /// Loads the last state of the app.
        /// </summary>
        public void LoadLastAppState()
        {
            //check app settings to get the last traffic view, if one exists
            if (IsolatedStorageSettings.ApplicationSettings.Contains(currentViewKey))
            {
                this.CurrentTrafficView = this.TrafficItems
                    .Where(t => t.ImageName == IsolatedStorageSettings.ApplicationSettings[currentViewKey].ToString())
                    .FirstOrDefault(); ;
            }
            else
            {
                //a previous view hasn't been saved to app settings
                //so default to the first item in the collection
                this.CurrentTrafficView = this.TrafficItems.FirstOrDefault();
            }

            if (IsolatedStorageSettings.ApplicationSettings.Contains(pinnedTileKey))     
            {
                this.PinnedTile = this.TrafficItems
                    .Where(t => t.ImageName == IsolatedStorageSettings.ApplicationSettings[pinnedTileKey].ToString())
                    .FirstOrDefault();

                if (this.PinnedTile != null)
                {
                    //check the actual ActiveTiles collection to determine 
                    //if the previously pinned tile is still pinned to the start screen
                    //it may have been removed by the user right from the start screen
                    //instead of the app, so this is check will confirm if it still exists
                    ShellTile appTile = ShellTile.ActiveTiles
                        .FirstOrDefault(x => x.NavigationUri.ToString().Contains(mainPageUri));

                    if (appTile == null)
                    {
                        //tile is no longer pinned to start, so update the app settings
                        //and current app state to reflect this
                        this.PinnedTile = null;
                        SaveAppState();
                    }
                }
            }
        }

        /// <summary>
        /// Pins the tile.
        /// </summary>
        /// <returns></returns>
        public void PinTile()
        {
            this.PinnedTile = this.CurrentTrafficView;

            FlipTileData tileData = new FlipTileData();
            tileData.Title = "";
            tileData.BackTitle = "Windsor Traffic View";

            //set the back tile content to indicate which traffic view is represented on the front tile
            tileData.BackContent = this.PinnedTile.Description;

            //if a traffic view tile has not yet been pinned to the start screen, create a new one
            //otherwise, update the existing tile with the new traffic view selection
            ShellTile scheduledTile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains(mainPageUri));
            if (scheduledTile == null)
            {
                ShellTile.Create(new Uri(mainPageUri, UriKind.Relative), tileData, false);
                scheduledTile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains(mainPageUri));
            }
            else
            {
                scheduledTile.Update(tileData);
            }
                        
            //with the tile pinned to start, start the shell tile schedule
            StartShellTileSchedule(scheduledTile);
            
            //save the current view and pinned tile info to app settings
            SaveAppState();

        }

        /// <summary>
        /// Unpins the tile.
        /// </summary>
        public void UnpinTile()
        {            
            if (this.PinnedTile != null)
            {
                ShellTile appTile = ShellTile.ActiveTiles
                        .FirstOrDefault(x => x.NavigationUri.ToString().Contains(mainPageUri));

                //stop the shell tile schedule, and then unpin the tile
                StopShellTileSchedule(appTile);

                //remove the pinned tile from the start screen
                appTile.Delete();
            }

            //clear the pinned tile from the view model 
            this.PinnedTile = null;

            //save app settings to reflect the change
            SaveAppState();
        }
        
        /// <summary>
        /// Stops the shell tile schedule.
        /// </summary>
        private void StopShellTileSchedule(ShellTile scheduledTile)
        {            
            if (scheduledTile != null)
            {
                //to stop a running schedule, we must attach to an existing schedule
                //to do that, we will need to recreate the schedule and start it,
                //if the schedule was started during a previous run of the application
                if (ugTileSchedule == null)
                {
                    StartShellTileSchedule(scheduledTile);
                }

                ugTileSchedule.Stop();
            }
        }

        /// <summary>
        /// Starts the shell tile schedule.
        /// </summary>
        /// <param name="scheduledTile">The scheduled tile.</param>
        private void StartShellTileSchedule(ShellTile scheduledTile)
        {
            ugTileSchedule = new ShellTileSchedule(scheduledTile);

            //set the schedule to update every hour, indefinitely, 
            //or until the user chooses to stop the schedule 
            ugTileSchedule.Recurrence = UpdateRecurrence.Interval;
            ugTileSchedule.Interval = UpdateInterval.EveryHour;
            ugTileSchedule.StartTime = DateTime.Now;
            ugTileSchedule.RemoteImageUri = App.ViewModel.CurrentTrafficView.ImageUri;
            ugTileSchedule.Start();
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
                
        /// <summary>
        /// Saves the state of the app.
        /// </summary>
        private void SaveAppState()
        {
            //save the current view and schedule state within application settings
            IsolatedStorageSettings.ApplicationSettings[currentViewKey] = this.CurrentTrafficView.ImageName;

            if (this.PinnedTile != null)
            {
                IsolatedStorageSettings.ApplicationSettings[pinnedTileKey] = this.PinnedTile.ImageName;
            }
            else
            {
                IsolatedStorageSettings.ApplicationSettings.Remove(pinnedTileKey);
            }

            IsolatedStorageSettings.ApplicationSettings.Save();
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
