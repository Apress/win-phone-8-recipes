using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Live;
using MyLiveConnectApp.Models;
using System.Windows.Controls;

namespace MyLiveConnectApp
{
    public partial class SkyDriveContents : PhoneApplicationPage
    {
        public SkyDriveContents()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel;
            this.Loaded += SkyDriveContents_Loaded;
        }

        private async void SkyDriveContents_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (App.ViewModel.LiveClient != null)
                {
                    LiveOperationResult operationResult = await App.ViewModel.LiveClient.GetAsync("me/skydrive/files");
                    dynamic skyDriveResult = ((dynamic)operationResult.Result).data;
                    App.ViewModel.LoadSkyDriveContents(skyDriveResult);
                }
            }
            catch (LiveConnectException ex)
            {
                MessageBox.Show("Error occurred loading files from SkyDrive: " + ex.Message);
            }
        }

        private async void LongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LiveConnectSkyDriveItem selectedItem = this.skyDriveList.SelectedItem as LiveConnectSkyDriveItem;
            if (!selectedItem.IsFolder)
            {
                MessageBoxResult result = MessageBox.Show("Would you like to download this file?", "", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    LiveConnectClient client = App.ViewModel.LiveClient;
                    LiveDownloadOperationResult operationResult = await App.ViewModel.LiveClient.DownloadAsync(selectedItem.Id);

                    MessageBox.Show("Download completed. Now saving to IsolatedStorage...");

                    bool success = await App.ViewModel.SaveFileToIsolatedStorage(operationResult.Stream, selectedItem.Name);

                    if (success)
                    {
                        MessageBox.Show("File saved to IsolatedStorage!");
                    }

                    //this is just a check from this app to verify the file exists in IsolatedStorage
                    success = await App.ViewModel.OpenSavedFile(selectedItem.Name);
                    if (success)
                    {
                        MessageBox.Show("File exists in IsolatedStorage!");
                    }
                }
            }
            else
            {
                if (App.ViewModel.IsConnected)
                {
                    LiveOperationResult operationResult = await App.ViewModel.LiveClient.GetAsync(string.Format("{0}/files", selectedItem.Id));
                    dynamic filesResult = ((dynamic)operationResult.Result).data;
                    App.ViewModel.LoadSkyDriveContents(filesResult);
                }
            }

        }

    }

}