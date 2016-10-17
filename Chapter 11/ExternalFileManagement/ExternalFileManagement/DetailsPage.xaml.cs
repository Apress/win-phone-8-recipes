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

namespace ExternalFileManagement
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        // Constructor
        public DetailsPage()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadFile();
        }

        private async void LoadFile()
        {
            if (App.ViewModel.SelectedFile != null)
            {
                FilesResult result = await App.ViewModel.LoadSelectedFile();
                if (!result.Success)
                {
                    MessageBox.Show(result.Message);
                }
            }
        }

    }
}