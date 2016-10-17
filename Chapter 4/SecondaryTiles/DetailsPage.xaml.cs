using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SecondaryTiles.Resources;
using SecondaryTiles.ViewModels;

namespace SecondaryTiles
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        private ItemViewModel selectedCharacter;

        // Constructor
        public DetailsPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // When page is navigated to, set data context 
        // to the selected item in the list and fix up
        // the app bar button
    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        if (DataContext == null)
        {
            string selectedCharacterName = "";
            if (NavigationContext.QueryString
                .TryGetValue("selectedItem", out selectedCharacterName))
            {
                if (!string.IsNullOrEmpty(selectedCharacterName))
                {
                    selectedCharacter = (from character in App.ViewModel.Items
                                         where character.CharacterName == selectedCharacterName
                                         select character).FirstOrDefault();
                    DataContext = selectedCharacter;
                }
            }
        }

        SetPinBar();
    }

        private ApplicationBarIconButton pinButton;
        private ApplicationBarIconButton PinButton
        {
            get
            {
                if (pinButton == null)
                {
                    ApplicationBar bar = (ApplicationBar)ApplicationBar;
                    pinButton = (from button in bar.Buttons.OfType<ApplicationBarIconButton>()
                                 where button.IconUri.OriginalString.Contains("pin")
                                 select button).FirstOrDefault();
                }

                return pinButton;

            }
        }

        private bool CharacterTileExists(string navigationSource)
        {
            ShellTile tile = ShellTile.ActiveTiles
                .FirstOrDefault(o => o.NavigationUri.ToString().Contains(navigationSource));
            return tile == null ? false : true;
        }

        private void DeleteCharacterTile(string navigationSource)
        {
            ShellTile tile = ShellTile.ActiveTiles
                .FirstOrDefault(o => o.NavigationUri.ToString().Contains(navigationSource));
            if (tile != null)
            {
                tile.Delete();
            }

        }

        private void SetCharacterTile(string navigationSource)
        {

            FlipTileData tileData = new FlipTileData()
            {
                Title = selectedCharacter.CharacterName,
                SmallBackgroundImage = new Uri(selectedCharacter.ImageUri, UriKind.RelativeOrAbsolute),
                BackgroundImage = new Uri(selectedCharacter.ImageUri, UriKind.RelativeOrAbsolute),
                BackTitle = selectedCharacter.CharacterName
            };

            ShellTile.Create(new Uri(navigationSource, UriKind.Relative), tileData, false);

        }

        private void SetPinBar()
        {
            var uri = NavigationService.Source.ToString();
            if (CharacterTileExists(uri))
            {
                PinButton.IconUri = new Uri("/Assets/unpin.png", UriKind.Relative);
                PinButton.Text = "Unpin";
            }
            else
            {
                PinButton.IconUri = new Uri("/Assets/pin.png", UriKind.Relative);
                PinButton.Text = "Pin";
            }
        }

        private void PinToStartScreenClick(object sender, EventArgs e)
        {
            string uri = NavigationService.Source.ToString();

            if (CharacterTileExists(uri))
            {
                // If the tile already exists, then we delete it
                DeleteCharacterTile(uri);
            }
            else
            {
                // Otherwise create it
                SetCharacterTile(uri);
            }

            SetPinBar();

        }
    }
}