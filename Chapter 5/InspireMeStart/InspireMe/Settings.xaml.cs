using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace InspireMe
{
    public partial class Settings : PhoneApplicationPage
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            App.SettingsVM.SaveSettings();
           
            MessageBox.Show("Your settings have been saved!", "", MessageBoxButton.OK);
            NavigationService.GoBack();
        }
    }
}