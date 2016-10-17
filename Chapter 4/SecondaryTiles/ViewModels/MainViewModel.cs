using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using SecondaryTiles.Resources;

namespace SecondaryTiles.ViewModels
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
            LoadData();
        }

        private ObservableCollection<ItemViewModel> items;
        public ObservableCollection<ItemViewModel> Items
        {
            get
            {
                return items;
            }
            private set
            {
                items = value;
                RaisePropertyChanged();
            }
        }

        private bool isDataLoaded;
        public bool IsDataLoaded
        {
            get
            {
                return isDataLoaded;
            }
            set
            {
                isDataLoaded = value;
                RaisePropertyChanged();
            }
        }

        public void LoadData()
        {
            Items.Add(new ItemViewModel { ImageUri = "/Assets/bart.png", CharacterName = "Bart" });
            Items.Add(new ItemViewModel { ImageUri = "/Assets/homer.png", CharacterName = "Homer" });
            Items.Add(new ItemViewModel { ImageUri = "/Assets/lisa.png", CharacterName = "Lisa" });
            Items.Add(new ItemViewModel { ImageUri = "/Assets/maggie.png", CharacterName = "Maggie" });
            Items.Add(new ItemViewModel { ImageUri = "/Assets/marge.png", CharacterName = "Marge" });

            this.IsDataLoaded = true;
        }
    }
}