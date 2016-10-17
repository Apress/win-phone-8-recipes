using System;
using System.ComponentModel;

namespace MyLiveConnectApp.Models
{
    public class LiveConnectCalendar : INotifyPropertyChanged
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

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private string desc;
        public string Description
        {
            get
            {
                return desc;
            }
            set
            {
                desc = value;
                OnPropertyChanged("Description");
            }
        }


        private string permissions;
        public string Permissions
        {
            get
            {
                return permissions;
            }
            set
            {
                permissions = value;
                OnPropertyChanged("Permissions");
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
