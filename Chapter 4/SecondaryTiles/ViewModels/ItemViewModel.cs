using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SecondaryTiles.ViewModels
{
    public class ItemViewModel : ViewModel
    {
        private string imageUri;
        public string ImageUri
        {
            get
            {
                return imageUri;
            }
            set
            {
                imageUri = value;
                RaisePropertyChanged();
            }
        }

        private string characterName;
        public string CharacterName
        {
            get
            {
                return characterName;
            }
            set
            {
                characterName = value;
                RaisePropertyChanged();
            }
        }
 
    }
}