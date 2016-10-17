using System;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace MyGalleryApp
{
    public partial class AlbumGalleryPage : PhoneApplicationPage
    {
        public AlbumGalleryPage()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
        }

        private void LongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                App.ViewModel.CurrentPicture = e.AddedItems[0] as Microsoft.Xna.Framework.Media.Picture;
                NavigationService.Navigate(new Uri("/ViewPicturePage.xaml", UriKind.Relative));
            }
        }
    }
}