using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Live;
using MyLiveConnectApp.Models;
using System;
using System.Collections.Generic;

namespace MyLiveConnectApp
{
    public partial class Calendars : PhoneApplicationPage
    {
        public Calendars()
        {
            InitializeComponent();
            this.Loaded += Calendars_Loaded;
            this.DataContext = App.ViewModel;
        }

        private void Calendars_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCalendars();
        }

        private void LongListSelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            LiveConnectCalendar selectedCalendar = calendarList.SelectedItem as LiveConnectCalendar;
            NavigationService.Navigate(new Uri("/Events.xaml?id=" + selectedCalendar.Id, UriKind.RelativeOrAbsolute));
        }

        private void CalendarButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (CalendarButton.Content.ToString().Equals("Add Calendar"))
            {
                CreateNewCalendar();
                CalendarButton.Content = "Delete Calendar";
            }
            else
            {
                DeleteCalendar(); 
                CalendarButton.Content = "Add Calendar";
            }
        }

        private async void LoadCalendars()
        {
            try
            {
                if (App.ViewModel.IsConnected)
                {
                    LiveOperationResult operationResult = await App.ViewModel.LiveClient.GetAsync("me/calendars");
                    dynamic eventsResult = ((dynamic)operationResult.Result).data;
                    App.ViewModel.LoadCalendars(eventsResult);
                }
            }
            catch (LiveConnectException ex)
            {
                MessageBox.Show("Error occurred loading calendars: " + ex.Message);
            }
        }

        private async void DeleteCalendar()
        {
            try
            {
                if (App.ViewModel.IsConnected)
                {
                    string calendarId = CalendarButton.Tag.ToString();
                    LiveOperationResult operationResult = await App.ViewModel.LiveClient.DeleteAsync(calendarId);
                    CalendarButton.Tag = null;

                    //reload the calendars from the web service so the new calendar appears in the list
                    LoadCalendars();
                }
            }
            catch (LiveConnectException exception)
            {
                MessageBox.Show("Error deleting calendar: " + exception.Message);
            }
        }

        private async void CreateNewCalendar()
        {
            try
            {
                if (App.ViewModel.IsConnected)
                {
                    Dictionary<string, object> newCalendar = new Dictionary<string, object>();
                    newCalendar.Add("name", "My New Calendar");
                    newCalendar.Add("description", "Sample to demonstrate adding and deleting calendars");
                    LiveOperationResult operationResult = await App.ViewModel.LiveClient.PostAsync("me/calendars", newCalendar);
                    dynamic result = operationResult.Result;

                    //this is just a quick and easy way to store the new calendar Id for this example
                    //which will be deleted the next time the button is clicked
                    CalendarButton.Tag = result.id;

                    //reload the calendars from the web service so the new calendar appears in the list
                    LoadCalendars();
                }
            }
            catch (LiveConnectException exception)
            {
                MessageBox.Show("Error creating calendar: " + exception.Message);
            }

        }
    }
}