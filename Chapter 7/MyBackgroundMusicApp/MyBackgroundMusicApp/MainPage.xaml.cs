using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.BackgroundAudio;
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
            BackgroundAudioPlayer.Instance.PlayStateChanged += new EventHandler(Instance_PlayStateChanged);
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

            if (PlayState.Playing == BackgroundAudioPlayer.Instance.PlayerState)
            {
                SetButtonImage(playTrack, "pause");
                App.ViewModel.CurrentTrackTitle = BackgroundAudioPlayer.Instance.Track.Title;

            }
            else
            {
                SetButtonImage(playTrack, "play");
            }
        }


        void Instance_PlayStateChanged(object sender, EventArgs e)
        {
            BitmapImage image = new BitmapImage();
                
            switch (BackgroundAudioPlayer.Instance.PlayerState)
            {
                case PlayState.Playing:
                    SetButtonImage(playTrack, "pause");
                    
                    break;

                case PlayState.Paused:
                case PlayState.Stopped:
                    SetButtonImage(playTrack, "play"); 
                    break;
            }

            if (null != BackgroundAudioPlayer.Instance.Track)
            {
                App.ViewModel.CurrentTrackTitle = BackgroundAudioPlayer.Instance.Track.Title;
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
            BackgroundAudioPlayer.Instance.SkipPrevious();
        }

        private void playTrack_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (PlayState.Playing == BackgroundAudioPlayer.Instance.PlayerState)
            {
                BackgroundAudioPlayer.Instance.Pause();
            }
            else
            {
                BackgroundAudioPlayer.Instance.Play();
            }
        }

        private void nextTrack_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            BackgroundAudioPlayer.Instance.SkipNext();
        }

    }
}