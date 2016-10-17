using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace MyBackgroundMusicApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        string folderTheme;

        public MainPage()
        {
            InitializeComponent();
            DataContext = App.ViewModel;

            Visibility darkBackgroundVisibility = (Visibility)Application.Current.Resources["PhoneDarkThemeVisibility"];
            folderTheme = (darkBackgroundVisibility == Visibility.Visible) ? "dark" : "light";
            SetButtonImage(previousTrack, "rew");
            SetButtonImage(nextTrack, "ff");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }

        }


        private void SetButtonImage(Button button, string action)
        {
            Image imageContent = button.Content as Image;
            if (null != imageContent)
            {
                imageContent.Source = new BitmapImage(new Uri(string.Format("Assets/{0}/transport.{1}.png", folderTheme, action), UriKind.Relative));
            }

        }


        private void previousTrack_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
        }

        private void playTrack_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
        }

        private void nextTrack_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
        }

    }
}