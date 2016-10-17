using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Windows.Networking.Proximity;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using TapToShare.ViewModels;

namespace TapToShare
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
                   
        private void SharePics_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PictureShare.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ExchangeMessages_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MessageShare.xaml", UriKind.RelativeOrAbsolute));
        }

        
    }

}
