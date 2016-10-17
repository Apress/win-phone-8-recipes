using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using InspireMe.DataProvider;

namespace InspireMe.Agent
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        private const string mainPageUri = "/MainPage.xaml?state=pinned";

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
                QuoteDataProvider quoteProvider = new QuoteDataProvider();

                if (quoteProvider.QuoteList.Count() > 0)
                {
                    FlipTileData newTileData = new FlipTileData()
                    {
                        //load a random quote from the quote data provider
                        WideBackContent = quoteProvider.GetRandomQuote(),
                        BackTitle = string.Format("Last Updated: {0}", DateTime.Now.ToString("MMM dd, yyyy h:mm tt"))
                    };

                    ShellTile appTile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains(mainPageUri));
                    if (appTile != null)
                    {
                        appTile.Update(newTileData);
                    }
                }
            }

            NotifyComplete(); 
        }
    }
}