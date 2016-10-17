using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.BackgroundTransfer;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Tasks;

namespace DownloadMe
{
    public partial class MainPage : PhoneApplicationPage
    {
        
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the LongListSelector control to the sample data
            DataContext = App.ViewModel;
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void DownloadButton_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {            
            App.ViewModel.DownloadSelectedItems();
        }

        private void PlayButton_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Button btn = (Button)sender;

            //the file location is stored in the Tag of list item's play button
            if (btn.Tag != null && btn.Tag.ToString() != "")
            {
                string fileLocation = btn.Tag.ToString();
                //use IsolatedStorageFile to check the file location to be sure the file exists
                //before attempting to launch the video
                IsolatedStorageFile isoFile = IsolatedStorageFile.GetUserStoreForApplication();
                if (isoFile.FileExists(fileLocation))
                {
                    Uri videoUri = new Uri(fileLocation, UriKind.RelativeOrAbsolute);

                    try
                    {
                        MediaPlayerLauncher mediaPlayerLauncher = new MediaPlayerLauncher()
                        {
                            Media = videoUri,
                            Location = MediaLocationType.Data,
                            Orientation =  MediaPlayerOrientation.Landscape,
                            Controls = MediaPlaybackControls.All
                        };

                        mediaPlayerLauncher.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

                
    }
}