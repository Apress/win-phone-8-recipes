using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Live;

namespace MyLiveConnectApp
{
    public partial class Events : PhoneApplicationPage
    {
        public Events()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            LoadEvents();
        }

        private async void LoadEvents()
        {
            try
            {
                if (NavigationContext.QueryString.ContainsKey("id") && App.ViewModel.IsConnected)
                {
                    string calendarId = NavigationContext.QueryString["id"];
                    LiveOperationResult operationResult = await App.ViewModel.LiveClient.GetAsync(string.Format("{0}/events", calendarId));
                    dynamic eventsResult = ((dynamic)operationResult.Result).data;
                    App.ViewModel.LoadEvents(eventsResult);
                }
            }
            catch (LiveConnectException ex)
            {
                MessageBox.Show("Error occurred loading events: " + ex.Message);
            }
        }
    }
}