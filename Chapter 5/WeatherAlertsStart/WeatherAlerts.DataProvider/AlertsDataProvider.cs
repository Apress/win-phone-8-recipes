using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace WeatherAlerts.DataProvider
{
    public class AlertsDataProvider
    {
        private const string locationUri = "http://www.weatheroffice.gc.ca/rss/battleboard/on21_e.xml";

        public event EventHandler AlertsDownloaded; 
        public delegate void EventHandler(object sender, EventArgs e);  

        public AlertsDataProvider()
        {
            this.WeatherAlerts = new List<string>();
        }

        public void LoadData()
        {
            WebClient client = new WebClient();
            client.OpenReadCompleted += client_OpenReadCompleted;
            client.OpenReadAsync(new System.Uri(locationUri));
        }


        private void client_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            StreamReader reader = new StreamReader(e.Result);
            string result = reader.ReadToEnd();
            
            if (e.Error == null && !string.IsNullOrEmpty(result))
            {
                XDocument xmlDoc = XDocument.Parse(result);

                this.WeatherAlerts = xmlDoc
                            .Descendants("channel")
                            .Descendants("item")
                            .Elements("title")
                            .Select(s => s.Value)
                            .ToList<string>();

                this.AlertLocation = (from node in xmlDoc.Descendants("channel")
                                      select node.Element("description").Value).FirstOrDefault();

                if (this.AlertsDownloaded != null)
                {
                    this.AlertsDownloaded(sender, null);
                }

            }
        }

        public string AlertLocation
        {
            get;
            private set;
        }

        public List<string> WeatherAlerts
        {
            get;
            private set;
        }

    }
}
