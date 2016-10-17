using System;
using System.ComponentModel;

namespace MyAppSettings.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {

        private MyAppSettings.Models.Settings _settings = new MyAppSettings.Models.Settings();
        
        public string UserName
        {
            get
            {
                return _settings.UserNameSetting;
            }
            set
            {
                _settings.UserNameSetting = value;
                NotifyPropertyChanged("UserName");
            }
        }

        public bool PlaySounds
        {
            get
            {
                return _settings.PlaySoundSetting;
            }
            set
            {
                _settings.PlaySoundSetting = value;
                NotifyPropertyChanged("PlaySounds");
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
