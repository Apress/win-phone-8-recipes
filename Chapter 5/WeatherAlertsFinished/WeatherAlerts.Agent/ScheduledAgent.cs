using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using System.Net;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using WeatherAlerts.DataProvider;

namespace WeatherAlerts.Agent
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        AlertsDataProvider dataProvider = new AlertsDataProvider();

        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        static ScheduledAgent()
        {
            // Subscribe to the managed exception handler
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                Application.Current.UnhandledException += UnhandledException;
            });
        }

        /// Code to execute on Unhandled Exceptions
        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        protected override void OnInvoke(ScheduledTask task)
        {
            if (task is PeriodicTask)
            {
                dataProvider.AlertsDownloaded += new AlertsDataProvider.EventHandler(dataProvider_AlertsDownloaded);
                dataProvider.LoadData();            
            }
        }

        private void dataProvider_AlertsDownloaded(object sender, System.EventArgs e)
        {
            if (dataProvider.WeatherAlerts.Count() > 0)
            {
                ShellToast toast = new ShellToast();
                toast.Title = "Weather Alert";
                toast.Content = dataProvider.WeatherAlerts.Count().ToString() + " active alert(s)";
                toast.NavigationUri = new System.Uri("/MainPage.xaml", System.UriKind.RelativeOrAbsolute);
                toast.Show();
            }

            dataProvider.AlertsDownloaded -= new AlertsDataProvider.EventHandler(dataProvider_AlertsDownloaded);

            NotifyComplete();
        }

    }
}