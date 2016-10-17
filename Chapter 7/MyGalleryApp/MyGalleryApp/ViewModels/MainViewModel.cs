using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Xna.Framework.Media;

namespace MyGalleryApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<PictureAlbum> photoAlbums;
        public ObservableCollection<PictureAlbum> PhotoAlbums
        {
            get
            {
                return photoAlbums;
            }
            set
            {
                photoAlbums = value;
                NotifyPropertyChanged("PhotoAlbums");
            }
        }
                
        private PictureAlbum currentAlbum;
        public PictureAlbum CurrentAlbum
        {
            get
            {
                return currentAlbum;
            }
            set
            {
                if (currentAlbum != value)
                {
                    currentAlbum = value;
                    NotifyPropertyChanged("CurrentAlbum");

                    //when the current album changes, update the CurrentAlbumPictures collection 
                    //to reflect the pictures in the newly selected album
                    CurrentAlbumPictures = new ObservableCollection<Picture>(currentAlbum.Pictures.ToList());
                }
            }
        }

        private ObservableCollection<Picture> currentAlbumPictures;
        public ObservableCollection<Picture> CurrentAlbumPictures
        {
            get
            {
                return currentAlbumPictures;
            }
            set
            {
                currentAlbumPictures = value;
                NotifyPropertyChanged("CurrentAlbumPictures");
            }
        }
        
        private Picture currentPicture;
        public Picture CurrentPicture
        {
            get
            {
                return currentPicture;
            }
            set
            {
                currentPicture = value;
                NotifyPropertyChanged("CurrentPicture");
            }
        }

        public void LoadData()
        {
            using (MediaLibrary library = new MediaLibrary())
            {
                this.PhotoAlbums = new ObservableCollection<PictureAlbum>(library.RootPictureAlbum.Albums.ToList());
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