using System;
using System.ComponentModel;

namespace RemindMe.ViewModels
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        private string _id;
        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }

        private string _desc;
        public string Description
        {
            get
            {
                return _desc;
            }
            set
            {
                if (value != _desc)
                {
                    _desc = value;
                    NotifyPropertyChanged("Description");
                }
            }
        }

        private DateTime _dueDate;
        public DateTime DueDate
        {
            get
            {
                return _dueDate;
            }
            set
            {
                if (value != _dueDate)
                {
                    _dueDate = value;
                    NotifyPropertyChanged("DueDate");
                }
            }
        }

        private double _amountDue;
        public double AmountDue
        {
            get
            {
                return _amountDue;
            }
            set
            {
                if (value != _amountDue)
                {
                    _amountDue = value;
                    NotifyPropertyChanged("AmountDue");
                }
            }
        }

        private bool _showReminder;
        public bool ShowReminder
        {
            get
            {
                return _showReminder;
            }
            set
            {
                if (value != _showReminder)
                {
                    _showReminder = value;
                    NotifyPropertyChanged("ShowReminder");
                }
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