using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MyBookList.Resources;
using MyBookList.ViewModels;
using MyBookList.Data;

namespace MyBookList
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
            App.ViewModel.LoadBooks();
            MainLongListSelector.ItemsSource = App.ViewModel.Books;
        }

        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainLongListSelector.SelectedItem == null)
                return;

            NavigationService.Navigate(new Uri("/DetailsPage.xaml?bookId=" + (MainLongListSelector.SelectedItem as Book).BookId, UriKind.Relative));
            MainLongListSelector.SelectedItem = null;
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/DetailsPage.xaml", UriKind.Relative));
        }

    }
}