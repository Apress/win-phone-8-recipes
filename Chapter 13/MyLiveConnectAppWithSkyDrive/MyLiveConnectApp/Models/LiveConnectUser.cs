using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLiveConnectApp.Models
{
    public class LiveConnectUser : INotifyPropertyChanged
    {
        private string id;
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        private string firstName;
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        private string lastName;
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        private Uri profileUri;
        public Uri ProfileUri
        {
            get
            {
                return profileUri;
            }
            set
            {
                profileUri = value;
                OnPropertyChanged("ProfileUri");
            }
        }
        
        private string birthday;
        public string Birthday
        {
            get
            {
                return birthday;
            }
            set
            {
                birthday = value;
                OnPropertyChanged("Birthday");
            }
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
