using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Linq;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;


namespace TrafficView.ViewModels
{
    public class TrafficItemViewModel : INotifyPropertyChanged
    {
        private const string windsorCamUri = "http://www.cdn.mto.gov.on.ca/english/traveller/compass/camera/pictures/LondonCamera/windsor/";
        private string description = "";
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        private string imageName;
        public string ImageName
        {
            get
            {
                return imageName;
            }
            set
            {
                imageName = value;

                this.ImageUri = new Uri(windsorCamUri + imageName);
                this.TrafficImage = new BitmapImage(this.ImageUri);

                OnPropertyChanged("ImageName");
            }
        }

        public Uri ImageUri
        {
            get;
            private set;
        }

        private BitmapImage trafficImage;
        public BitmapImage TrafficImage
        {
            get
            {
                return trafficImage;
            }
            set
            {
                trafficImage = value;
                OnPropertyChanged("TrafficImage");
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
