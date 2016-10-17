using System;
using System.ComponentModel;

namespace MyEmergencyInfo.Models
{
    public class EmergencyInfo : INotifyPropertyChanged
    {
        private string _contactName;
        public string ContactName
        {
            get
            {
                return _contactName;
            }
            set
            {
                _contactName = value;
                NotifyPropertyChanged("ContactName");
            }
        }

        private string _contactPhoneNumber;
        public string ContactPhoneNumber
        {
            get
            {
                return _contactPhoneNumber;
            }
            set
            {
                _contactPhoneNumber = value;
                NotifyPropertyChanged("ContactPhoneNumber");
            }
        }

        private string _allergies;
        public string Allergies
        {
            get
            {
                return _allergies;
            }
            set
            {
                _allergies = value;
                NotifyPropertyChanged("Allergies");
            }
        }

        private string _medications;
        public string Medications
        {
            get
            {
                return _medications;
            }
            set
            {
                _medications = value;
                NotifyPropertyChanged("Medications");
            }
        }

        public EmergencyInfo()
        {
        }

        public EmergencyInfo(string emergencyInfoData)
        {
            string[] emergencyInfoArray = emergencyInfoData.Split(new string[]{"||"}, StringSplitOptions.None);
            ContactName = emergencyInfoArray[0];
            ContactPhoneNumber = emergencyInfoArray[1];
            Allergies = emergencyInfoArray[2];
            Medications = emergencyInfoArray[3];            
        }

        public override string ToString()
        {
            return String.Join("||", ContactName, ContactPhoneNumber, Allergies, Medications);
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
