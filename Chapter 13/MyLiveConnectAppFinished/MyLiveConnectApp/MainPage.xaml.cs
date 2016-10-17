using System.Windows;
using Microsoft.Live;
using Microsoft.Live.Controls;
using Microsoft.Phone.Controls;

namespace MyLiveConnectApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        private LiveConnectClient client;
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel;
        }

        private async void signInButton_SessionChanged(object sender, LiveConnectSessionChangedEventArgs e)
        {
            try
            {
                if (e.Status == LiveConnectSessionStatus.Connected)
                {
                    client = new LiveConnectClient(e.Session);
                    LiveOperationResult operationResult = await client.GetAsync("me");
                    App.ViewModel.LoadUserInfo((dynamic)operationResult.Result);
                }
                else
                {
                    App.ViewModel.ResetUserInfo();
                }
            }
            catch (LiveConnectException exception)
            {
                App.ViewModel.ResetUserInfo();
                MessageBox.Show("An error occurred signing in: " + exception.Message);
            }
        }

    }
}