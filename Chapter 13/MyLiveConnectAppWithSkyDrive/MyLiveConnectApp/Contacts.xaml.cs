using System.Linq;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Live;

namespace MyLiveConnectApp
{
    public partial class Contacts : PhoneApplicationPage
    {
        public Contacts()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel;
            this.Loaded += Contacts_Loaded;
        }

        private void Contacts_Loaded(object sender, RoutedEventArgs e)
        {
            LoadContacts("me/contacts", false);
        }

        private void FriendsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            bool friendsOnly = (bool)FriendsCheckBox.IsChecked;
            string path = (friendsOnly) ? "me/friends" : "me/contacts";
            LoadContacts(path, friendsOnly);

        }

        private async void LoadContacts(string path, bool isFriendsList)
        {
            try
            {
                if (App.ViewModel.LiveClient != null)
                {
                    LiveOperationResult operationResult = await App.ViewModel.LiveClient.GetAsync(path);
                    dynamic contactResult = ((dynamic)operationResult.Result).data;
                    App.ViewModel.LoadContacts(contactResult, isFriendsList);
                }
            }
            catch (LiveConnectException ex)
            {
                MessageBox.Show("Error occurred loading contacts: " + ex.Message);
            }
        }
    }
}