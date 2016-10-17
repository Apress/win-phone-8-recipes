using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using VoiceBucketList.Models;

namespace VoiceBucketList.ViewModels
{
    public class BucketListViewModel : INotifyPropertyChanged
    {
        public BucketListViewModel()
        {
            LoadSampleData();
        }

        private string pageTitle;
        public string PageTitle
        {
            get
            {
                return pageTitle;
            }
            set
            {
                pageTitle = value;
                NotifyPropertyChanged("PageTitle");
            }
        }

        private BucketListItem newItem;
        public BucketListItem NewItem
        {
            get
            {
                return newItem;
            }
            set
            {
                newItem = value;
                NotifyPropertyChanged("NewItem");
            }
        }

        private ObservableCollection<BucketListItem> movies;
        public ObservableCollection<BucketListItem> Movies
        {
            get
            {
                return movies;
            }
            set
            {
                movies = value;
                NotifyPropertyChanged("Movies");
            }
        }

        private ObservableCollection<BucketListItem> travelPlaces;
        public ObservableCollection<BucketListItem> TravelPlaces
        {
            get
            {
                return travelPlaces;
            }
            set
            {
                travelPlaces = value;
                NotifyPropertyChanged("TravelPlaces");
            }
        }

        private ObservableCollection<BucketListItem> restaurants;
        public ObservableCollection<BucketListItem> Restaurants
        {
            get
            {
                return restaurants;
            }
            set
            {
                restaurants = value;
                NotifyPropertyChanged("Restaurants");
            }
        }

        public void AddNewItem()
        {
            if (this.NewItem != null)
            {
                if (this.NewItem.ItemType == BucketListItemType.Movie)
                {
                    this.Movies.Add(this.NewItem);
                }
                else if (this.NewItem.ItemType == BucketListItemType.Restaurant)
                {
                    this.Restaurants.Add(this.NewItem);
                }
                else
                {
                    this.TravelPlaces.Add(this.NewItem);
                }
            }

            this.NewItem = new BucketListItem();

        }

        private void LoadSampleData()
        {
            if (this.Movies == null)
            {
                this.Movies = new ObservableCollection<BucketListItem>();

                Movies.Add(new BucketListItem { Name = "Iron Man 3", ItemType = BucketListItemType.Movie });
                Movies.Add(new BucketListItem { Name = "Man of Steel", ItemType = BucketListItemType.Movie });
                Movies.Add(new BucketListItem { Name = "Monsters University", ItemType = BucketListItemType.Movie });
            }

            if (this.TravelPlaces == null)
            {
                this.TravelPlaces = new ObservableCollection<BucketListItem>();

                TravelPlaces.Add(new BucketListItem { Name = "Bali", ItemType = BucketListItemType.Travel });
                TravelPlaces.Add(new BucketListItem { Name = "Fiji", ItemType = BucketListItemType.Travel });
                TravelPlaces.Add(new BucketListItem { Name = "Vegas, Baby!", ItemType = BucketListItemType.Travel });
            }

            if (this.Restaurants == null)
            {
                this.Restaurants = new ObservableCollection<BucketListItem>();

                Restaurants.Add(new BucketListItem { Name = "Terra Rossa in Vegas", ItemType = BucketListItemType.Restaurant });
                Restaurants.Add(new BucketListItem { Name = "Maze in NYC", ItemType = BucketListItemType.Travel });
                Restaurants.Add(new BucketListItem { Name = "Scaramouche in Toronto", ItemType = BucketListItemType.Travel });
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