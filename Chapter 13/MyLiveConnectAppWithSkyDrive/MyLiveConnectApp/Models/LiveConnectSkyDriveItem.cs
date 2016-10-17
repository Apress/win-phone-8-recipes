using System;
using System.ComponentModel;

namespace MyLiveConnectApp.Models
{
    public class LiveConnectSkyDriveItem : INotifyPropertyChanged
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

        private bool isFolder;
        public bool IsFolder
        {
            get
            {
                return isFolder;
            }
            set
            {
                isFolder = value;
                OnPropertyChanged("IsFolder");
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

        private string uploadLocation;
        public string UploadLocation
        {
            get
            {
                return uploadLocation;
            }
            set
            {
                uploadLocation = value;
                OnPropertyChanged("UploadLocation");
            }
        }

        private Uri itemUri;
        public Uri ItemUri
        {
            get
            {
                return itemUri;
            }
            set
            {
                itemUri = value;
                OnPropertyChanged("ItemUri");
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