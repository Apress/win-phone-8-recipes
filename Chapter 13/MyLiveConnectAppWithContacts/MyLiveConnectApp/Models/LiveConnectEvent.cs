using System;
using System.ComponentModel;

namespace MyLiveConnectApp.Models
{
    public class LiveConnectEvent : INotifyPropertyChanged
    {
        private string id;
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private string desc;
        public string Description
        {
            get
            {
                return desc;
            }
            set
            {
                desc = value;
                OnPropertyChanged("Description");
            }
        }
    
        private DateTime startTime;
        public DateTime StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                startTime = value;
                OnPropertyChanged("StartTime");
            }
        }

        private DateTime endTime;
        public DateTime EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
                OnPropertyChanged("EndTime");
            }
        }

        private bool isAllDayEvent;
        public bool IsAllDayEvent
        {
            get
            {
                return isAllDayEvent;
            }
            set
            {
                isAllDayEvent = value;
                OnPropertyChanged("IsAllDayEvent");
            }
        }

        private bool isRecurrent;
        public bool IsRecurrent
        {
            get
            {
                return isRecurrent;
            }
            set
            {
                isRecurrent = value;
                OnPropertyChanged("IsRecurrent");
            }
        }

        #region Notify Property Changed Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion    
    }
}
