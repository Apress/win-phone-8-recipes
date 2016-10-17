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

            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
            this.DataContext = App.ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadAlerts();
            }
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            RegisterAgent();
        }

        private void RegisterAgent()
        {
            string taskName = "WeatherAlertsTask";
            try
            {

                if (ScheduledActionService.Find(taskName) != null)
                {
                    //if the agent exists, remove and then add it to ensure 
                    //the agent's schedule is updated to avoid expiration                    
                    ScheduledActionService.Remove(taskName);
                }

                PeriodicTask periodicTask = new PeriodicTask(taskName);
                periodicTask.Description = "WeatherAlertsTask task provides toast notifications when a weather alert is active";
                ScheduledActionService.Add(periodicTask);

                //only use LaunchForTest when deugging
                //be sure to remove this code section before publishing your app 
#if DEBUG
                ScheduledActionService.LaunchForTest(taskName, TimeSpan.FromSeconds(30));
#endif

            }
            catch (InvalidOperationException exception)
            {
                MessageBox.Show(exception.Message);
            }
            catch (SchedulerServiceException schedulerException)
            {
                MessageBox.Show(schedulerException.Message);
            }

        }      

    }
}