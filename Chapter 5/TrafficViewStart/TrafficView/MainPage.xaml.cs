using System;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;

namespace TrafficView
{
    public partial class MainPage : PhoneApplicationPage
    {
        private bool isPinned = false;

        public MainPage()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);            
        }
        
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateAppBar();
        }

        /// <summary>
        /// Updates the application bar based on whether a pinned tile exists on the start screen.
        /// If a pinned tile is found, then the unpin icon is displayed to allow the user to remove it from the start screen.
        /// If a pinned tile is not found, then the pin icon is displayed to allow the user to pin it to the start screen.
        /// Pin/Unpin icons obtained from http://modernuiicons.com 
        /// </summary>
        private void UpdateAppBar()
        {
            string appBarResource = (isPinned) ? "appBarUnpin" : "appBarPin";
            ApplicationBar = (Microsoft.Phone.Shell.ApplicationBar)Resources[appBarResource];
        }


        /// <summary>
        /// Pin the tile to the start screen
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ApplicationBarPin_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Need to add pin logic to add tile of current traffic view to start screen.");

            //simple flag to control hide/display of pin/unpin icons
            isPinned = true; 

            UpdateAppBar();
        }
                
        /// <summary>
        /// Delete the pinned tile from the start screen, if one exists
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ApplicationBarUnpin_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Need to add unpin logic to remove tile from start screen.");

            //simple flag to control hide/display of pin/unpin icons
            isPinned = false; 

            UpdateAppBar();
        }
    }
}