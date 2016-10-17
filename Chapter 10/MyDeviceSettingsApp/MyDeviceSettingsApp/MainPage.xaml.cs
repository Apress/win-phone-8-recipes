using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MyDeviceSettingsApp.Resources;

namespace MyDeviceSettingsApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private async void SettingsButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            bool success = false;
            Button buttonTapped = (Button)sender;
            if (buttonTapped.Tag != null)
            {
                Uri uri = new Uri(buttonTapped.Tag.ToString());
                success = await Windows.System.Launcher.LaunchUriAsync(uri);
            }

            if (success == false)
            {
                MessageBox.Show("Failed to launch the app");
            }
        }
    }
}