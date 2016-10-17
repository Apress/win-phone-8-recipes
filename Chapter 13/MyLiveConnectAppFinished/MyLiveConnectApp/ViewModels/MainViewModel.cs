using Microsoft.Live;
using MyLiveConnectApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLiveConnectApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private enum Months
        {
            January = 1,
            February,
            March,
            April,
            May,
            June,
            July,
            August,
            September,
            October,
            November,
            December
        }

        #region Properties
        public string ClientId
        {
            get
            {
                return "TO DO: REGISTER YOUR APP FOR A CLIENT_ID ONLINE";
            }
        }

        private LiveConnectUser user;
        public LiveConnectUser User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged("User");

            }
        }

        private bool isConnected;
        public bool IsConnected
        {
            get
            {
                return isConnected;
            }
            set
            {
                isConnected = value;
                OnPropertyChanged("IsConnected");

            }
        }

        private string signInStatus = "Not signed in";
        public string SignInStatus
        {
            get
            {
                return signInStatus;
            }
            set
            {
                signInStatus = value;
                OnPropertyChanged("SignInStatus");
            }
        }
        #endregion

        public void LoadUserInfo(dynamic result)
        {
            this.User = new LiveConnectUser();
            this.User.FirstName = result.first_name;
            this.User.LastName = result.last_name;

            string profilePageUrl = result.link + result.id;
            this.User.ProfileUri = new Uri(profilePageUrl, UriKind.Absolute);

            int birthMonth = result.birth_month;

            this.User.Birthday = Enum.Parse(typeof(Months), birthMonth.ToString()) + " " + result.birth_day;
            this.SignInStatus = "You are signed in with your Live ID!";
        }

        public void ResetUserInfo()
        {
            App.ViewModel.SignInStatus = "You are not signed in.";
            App.ViewModel.IsConnected = false;
            App.ViewModel.User = null;
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
