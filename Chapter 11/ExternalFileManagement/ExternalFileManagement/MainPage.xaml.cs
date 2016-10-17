using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ExternalFileManagement.Resources;
using ExternalFileManagement.ViewModels;
using Microsoft.Phone.Storage;
using System.IO;

namespace ExternalFileManagement
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (App.ViewModel.Files.Count == 0)
            {
                LoadFileList();
            }
        }

        private async void LoadFileList()
        {
            FilesResult result = await App.ViewModel.GetFiles();
            if (!result.Success)
            {
                MessageBox.Show(result.Message);
            }
            else
            {
                MainLongListSelector.ItemsSource = App.ViewModel.Files;
            }

        }

        // Handle selection changed on LongListSelector
        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected item is null (no selection) do nothing
            if (MainLongListSelector.SelectedItem == null)
                return;

            App.ViewModel.SelectedFile = (ExternalStorageFile)MainLongListSelector.SelectedItem;

            // Navigate to the new page
            NavigationService.Navigate(new Uri("/DetailsPage.xaml", UriKind.Relative));

            // Reset selected item to null (no selection)
            MainLongListSelector.SelectedItem = null;
        }

    }
}