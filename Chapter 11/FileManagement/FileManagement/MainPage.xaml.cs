using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using FileManagement.Resources;
using FileManagement.ViewModels;
using Windows.Storage.Pickers;
using Windows.Storage;

namespace FileManagement
{
    public partial class MainPage : PhoneApplicationPage
    {

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel;
        }

        private async void LoadFileList()
        {
            await App.ViewModel.GetFiles();
            MainLongListSelector.ItemsSource = App.ViewModel.Files;

            fileOpenLabel.Visibility = (App.ViewModel.Files.Count == 0) ? 
                System.Windows.Visibility.Collapsed : 
                System.Windows.Visibility.Visible;

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadFileList();
            base.OnNavigatedFrom(e);
            
        }

        private void Button_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/FileDetails.xaml", UriKind.Relative));
        }
        
        private void LongListSelector_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {            
            if (MainLongListSelector.SelectedItem == null)
                return;

            StorageFile file = MainLongListSelector.SelectedItem as StorageFile;
            NavigationService.Navigate(new Uri("/FileDetails.xaml?fn=" + HttpUtility.UrlEncode(file.Name), UriKind.Relative));
            
            MainLongListSelector.SelectedItem = null;

        }

    }
}