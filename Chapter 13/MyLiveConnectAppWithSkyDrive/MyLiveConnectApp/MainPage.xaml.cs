using System;
using Microsoft.Live;
using Microsoft.Live.Controls;
using Microsoft.Phone.Controls;
using System.Windows;

namespace MyLiveConnectApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel;
        }

        private async void signInButton_SessionChanged(object sender, LiveConnectSessionChangedEventArgs e)
        {
            if (e.Status == LiveConnectSessionStatus.Connected)
            {
                try
                {
                    App.ViewModel.LiveClient = new LiveConnectClient(e.Session);
                    LiveOperationResult operationResult = await App.ViewModel.LiveClient.GetAsync("me");
                    App.ViewModel.LoadUserInfo(operationResult.Result);
                }
                catch (LiveConnectException exception)
                {
                    App.ViewModel.ResetUserInfo();
                    MessageBox.Show("An error occurred signing in: " + exception.Message);                    
                }
            }
            else
            {
                App.ViewModel.ResetUserInfo();
            }
        }

        private void HubTile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (App.ViewModel.IsConnected)
            {
                HubTile selectedTile = (HubTile)sender;
                Uri pageUri = new Uri(string.Format("/{0}.xaml", selectedTile.Tag.ToString()), UriKind.Relative);
                NavigationService.Navigate(pageUri);
            }
            else
            {
                MessageBox.Show("You must sign in first.");
            }
        }

    }
}