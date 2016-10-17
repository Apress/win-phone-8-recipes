using System;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;

namespace InspireMe
{
    public partial class MainPage : PhoneApplicationPage
    {
        private ShellTile appTile;
        private const string mainPageUri = "/MainPage.xaml?state=pinned";
        
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);            
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = App.ViewModel;            
            UpdateAppBar();
            RegisterAgent();
        }

        /// <summary>
        /// Registers the scheduled agent which will run periodically and
        /// update the application's pinned tile with a random quote
        /// </summary>
        private void RegisterAgent()
        {
            string taskName = "InspireMeTask";
            try
            {
                if (ScheduledActionService.Find(taskName) != null)
                {
                    //if the agent exists, remove and then add it to ensure 
                    //the agent's schedule is updated to avoid expiration                    
                    ScheduledActionService.Remove(taskName);
                }

                PeriodicTask periodicTask = new PeriodicTask(taskName);
                periodicTask.Description = "InspireMe task updates the tile with a new quote";
                ScheduledActionService.Add(periodicTask);                

                //only use LaunchForTest when deugging
                //be sure to remove this code section before publishing your app 
#if DEBUG
                ScheduledActionService.LaunchForTest(taskName, TimeSpan.FromSeconds(30));
#endif

            }
            catch (InvalidOperationException exception)
            {
                MessageBox.Show(exception.Message);
            }
            catch (SchedulerServiceException schedulerException)
            {
                MessageBox.Show(schedulerException.Message);
            }            

        }

                    
        /// <summary>
        /// Updates the application bar based on whether a pinned tile exists on the start screen.
        /// If a pinned tile is found, then the unpin icon is displayed to allow the user to remove it from the start screen.
        /// If a pinned tile is not found, then the pin icon is displayed to allow the user to pin it to the start screen.
        /// Pin/Unpin icons obtained from http://modernuiicons.com 
        /// </summary>
        private void UpdateAppBar()
        {
            ShellTile appTile = GetPinnedTile();
            string appBarResource = (appTile != null) ? "appBarUnpin" : "appBarPin";
            ApplicationBar = (Microsoft.Phone.Shell.ApplicationBar)Resources[appBarResource];
        }

        /// <summary>
        /// Pins a secondary tile to the device's start screen
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ApplicationBarPin_Click(object sender, EventArgs e)
        {
            //check to determine if a tile has already been pinned to the start screen 
            ShellTile appTile = GetPinnedTile();
            if (appTile == null)
            {
                //define the tile data
                FlipTileData newTileData = new FlipTileData()
                {
                    Title = "InspireMe",
                    BackContent = "Expand to wide tile to view random quotes",
                    WideBackContent = App.ViewModel.GetRandomQuote(),
                    BackTitle = DateTime.Now.ToString("MMM dd, yyyy h:mm tt"),
                    SmallBackgroundImage = new Uri("/Assets/Tiles/AppTile159.png", UriKind.RelativeOrAbsolute),
                    BackgroundImage = new Uri("/Assets/Tiles/AppTile336.png", UriKind.RelativeOrAbsolute),
                    WideBackgroundImage = new Uri("/Assets/Tiles/AppTile691.png", UriKind.RelativeOrAbsolute)
                };

                //add the tile to the start screen, including support for wide tiles
                ShellTile.Create(new Uri(mainPageUri, UriKind.Relative), newTileData, true);
            }

            //update the application bar to show the unpin button
            UpdateAppBar();
        }

        /// <summary>
        /// Delete the pinned tile from the start screen, if one exists
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ApplicationBarUnpin_Click(object sender, EventArgs e)
        {
            //check to determine if a tile has been pinned to the start screen
            ShellTile appTile = GetPinnedTile();
            if (appTile != null)
            {
                //remove the pinned tile from the start screen
                appTile.Delete();

                MessageBox.Show("The InspireMe live tile has been unpinned from the Start screen.");
            }

            //update the application bar to show the pin icon
            UpdateAppBar();
        }

        /// <summary>
        /// Returns the application's pinned tile, if one exists
        /// or null, if one is not found.
        /// </summary>
        /// <returns></returns>
        private ShellTile GetPinnedTile()
        {
            return ShellTile
                .ActiveTiles
                .FirstOrDefault(x => x.NavigationUri.ToString().Contains(mainPageUri));
        }

    }
}