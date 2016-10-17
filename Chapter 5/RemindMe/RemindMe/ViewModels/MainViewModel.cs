using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Linq;
using RemindMe.Resources;
using Microsoft.Phone.Scheduler;
using System.Collections.Generic;
using System.Windows;

namespace RemindMe.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private const string appSettingKey = "BillCollection";
            
        public ObservableCollection<ItemViewModel> Items { get; set; }

        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
        }

        
        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(appSettingKey))
            {
                this.Items.Add(new ItemViewModel() { ID = "1", Description = "cable bill", DueDate = DateTime.Now.AddDays(1), AmountDue=82.33 });
                this.Items.Add(new ItemViewModel() { ID = "2", Description = "mortgage", DueDate = DateTime.Now.AddDays(1), AmountDue=1400});
                this.Items.Add(new ItemViewModel() { ID = "3", Description = "phone bill", DueDate = DateTime.Now.AddDays(1), AmountDue=35.75 });
            }
            else
            {
                this.Items = IsolatedStorageSettings.ApplicationSettings[appSettingKey] as ObservableCollection<ItemViewModel>;
                NotifyPropertyChanged("Items");

            }

            this.IsDataLoaded = true;
        }

        public void SaveBills()
        {
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(appSettingKey))
            {
                IsolatedStorageSettings.ApplicationSettings.Add(appSettingKey, this.Items);
            }
            else
            {
                IsolatedStorageSettings.ApplicationSettings[appSettingKey] = this.Items;
            }
            
        }

        public void SetNotification(ItemViewModel item)
        {
            bool removeFromSchedule = (item.DueDate.Subtract(DateTime.Now).Days < 0);
            string reminderName = item.Description + " reminder";
            string notificationMessage = string.Format("Amount due: ${0}. Due by: {1}", item.AmountDue, item.DueDate.ToShortDateString());

            if (item.ShowReminder && !removeFromSchedule)
            {
                Reminder reminder = new Reminder(reminderName);
                reminder.Title = reminderName;
                reminder.BeginTime = DateTime.Now;
                reminder.ExpirationTime = item.DueDate;
                reminder.RecurrenceType = RecurrenceInterval.Daily;
                reminder.Content = notificationMessage;
                reminder.NavigationUri = new Uri(string.Format("/DetailsPage.xaml?selectedItem={0}", item.ID), UriKind.Relative);
                try
                {
                    if (ScheduledActionService.Find(reminderName) != null)
                    {
                        ScheduledActionService.Remove(reminderName);
                    }
                
                    ScheduledActionService.Add(reminder);
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(reminderName + " Error: " + ex.Message);
                }
            }
            else
            {
                item.ShowReminder = false;
                ScheduledActionService.Remove(reminderName);
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}