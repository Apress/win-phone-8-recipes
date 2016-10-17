using System;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Scheduler;
using System.Windows.Navigation;

namespace WeatherAlerts
{
    public partial class MainPage : PhoneApplicationPage
    {
        
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel;
        }
                
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadAlerts();
            }
        }
    }
}