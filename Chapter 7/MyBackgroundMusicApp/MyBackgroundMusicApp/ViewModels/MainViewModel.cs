using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Xna.Framework.Media;
using System.IO.IsolatedStorage;
using Microsoft.Phone.BackgroundAudio;

namespace MyBackgroundMusicApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<AudioTrack> tracks = new ObservableCollection<AudioTrack>();
        public ObservableCollection<AudioTrack> Tracks
        {
            get
            {
                return tracks;
            }
            set
            {
                tracks = value;
                NotifyPropertyChanged("Tracks");
            }
        }

        private string currentTrackTitle;
        public string CurrentTrackTitle
        {
            get
            {
                return currentTrackTitle;
            }
            set
            {
                if (currentTrackTitle != value)
                {
                    currentTrackTitle = value;
                    NotifyPropertyChanged("CurrentTrackTitle");
                }
            }
        }
    
        public void LoadData()
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string[] musicFiles = storage.GetFileNames().Where(f => f.EndsWith(".mp3")).ToArray();
                foreach (string musicFile in musicFiles)
                {
                    AudioTrack track = new AudioTrack(new Uri(musicFile, UriKind.Relative),
                                                    musicFile.Replace(".mp3", "").Replace("_", " "),
                                                    "Unknown",
                                                    "Unknown",
                                                    null);

                    this.Tracks.Add(track);
                }
            }
            this.IsDataLoaded = true;
        }


        public bool IsDataLoaded
        {
            get;
            private set;
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