using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using AppointmentsAndContacts.Resources;
using System.Collections.ObjectModel;

namespace AppointmentsAndContacts
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            Loaded += MainPageLoaded;

        }

        void MainPageLoaded(object sender, RoutedEventArgs e)
        {
            this.selectDemo.ItemsSource = new ObservableCollection<string>(
                new List<string> 
                {
                    "appointments", 
                    "contacts", 
                    "contact store" 
                });
            this.selectDemo.SelectionChanged += SelectDemoSelectionChanged;
        }

        private void SelectDemoSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LongListSelector selector = sender as LongListSelector;
            if(selector != null)
            {
                string selectedDemo = selector.SelectedItem.ToString();
                switch (selectedDemo)
                {
                    case "appointments":
                        NavigationService.Navigate(new Uri("/Views/AppointmentsView.xaml", UriKind.Relative));
                        break;
                    case "contacts":
                        NavigationService.Navigate(new Uri("/Views/ContactsView.xaml", UriKind.Relative));
                        break;
                    case "contact store":
                        NavigationService.Navigate(new Uri("/Views/CustomStoreView.xaml", UriKind.Relative));
                        break;

                }
            }

        }


    }
}