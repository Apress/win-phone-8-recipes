using System;
using System.ComponentModel;

namespace DownloadMe.ViewModels
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        private string _id;
        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (value != _title)
                {
                    _title = value;
                    NotifyPropertyChanged("Title");
                }
            }
        }

        private string _speaker;
        public string Speaker
        {
            get
            {
                return _speaker;
            }
            set
            {
                if (value != _speaker)
                {
                    _speaker = value;
                    NotifyPropertyChanged("Speaker");
                }
            }
        }

        private string _downloadURL;       
        public string DownloadUrl
        {
            get
            {
                return _downloadURL;
            }
            set
            {
                if (value != _downloadURL)
                {
                    _downloadURL = value;
                    NotifyPropertyChanged("DownloadUrl");
                }
            }
        }
                
        private string _downloadUri;
        public string LocalUri
        {
            get
            {
                return _downloadUri;
            }
            set
            {
                if (value != _downloadUri)
                {
                    _downloadUri = value;
                    NotifyPropertyChanged("LocalUri");
                }
            }
        }

        private string _downloadProgress = "";
        public string DownloadProgress
        {
            get
            {
                return _downloadProgress;
            }
            set
            {
                if (value != _downloadProgress)
                {
                    _downloadProgress = value;
                    NotifyPropertyChanged("DownloadProgress");
                }
            }
        }

        private bool _itemDownloaded = false;
        public bool ItemDownloaded
        {
            get
            {
                return _itemDownloaded;
            }
            set
            {
                if (value != _itemDownloaded)
                {
                    _itemDownloaded = value;
                    NotifyPropertyChanged("ItemDownloaded");
                }
            }
        }

        private bool _itemSelected = false;
        public bool ItemSelected
        {
            get
            {
                return _itemSelected;
            }
            set
            {
                if (value != _itemSelected)
                {
                    _itemSelected = value;
                    NotifyPropertyChanged("ItemSelected");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}