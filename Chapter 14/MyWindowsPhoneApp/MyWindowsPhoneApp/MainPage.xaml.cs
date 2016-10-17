using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MyWindowsPhoneApp.Resources;

namespace MyWindowsPhoneApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Button btn = sender as Button;
            Uri pageUri = new Uri(string.Format("/{0}.xaml",btn.Tag.ToString()), UriKind.Relative);
            NavigationService.Navigate(pageUri);

        }

    }
}