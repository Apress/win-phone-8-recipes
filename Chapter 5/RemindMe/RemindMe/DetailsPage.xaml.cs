using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RemindMe.Resources;
using RemindMe.ViewModels;

namespace RemindMe
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        // Constructor
        public DetailsPage()
        {
            InitializeComponent();
        }

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (DataContext == null)
            {
                if (!App.ViewModel.IsDataLoaded)
                {
                    App.ViewModel.LoadData();
                }

                string selectedIndex = "";
                if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
                {
                    DataContext = App.ViewModel.Items.Where(i => i.ID == selectedIndex).FirstOrDefault();
                }
            }
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            ApplicationBarIconButton btn = sender as ApplicationBarIconButton;
            App.ViewModel.SetNotification((ItemViewModel)DataContext);
            App.ViewModel.SaveBills();
            NavigationService.GoBack();
        }

    }
}