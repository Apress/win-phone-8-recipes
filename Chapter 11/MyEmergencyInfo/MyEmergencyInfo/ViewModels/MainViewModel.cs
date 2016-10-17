using MyEmergencyInfo.Models;
using System;
using System.ComponentModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Security.Cryptography;
using System.Text;

namespace MyEmergencyInfo.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {

        private readonly string filePath = "MyEmergencyInfo.txt";

        private EmergencyInfo _emergencyInfo = new EmergencyInfo();
        public EmergencyInfo EmergencyInfoData
        {
            get
            {
                return _emergencyInfo;
            }
            set
            {
                _emergencyInfo = value;
                NotifyPropertyChanged("EmergencyInfoData");
            }
        }


        public bool SaveInfo()
        {
            try
            {
                byte[] emergencyInfoByteArray = Encoding.UTF8.GetBytes(EmergencyInfoData.ToString());
                byte[] encryptedEmergencyInfoByteArray = ProtectedData.Protect(emergencyInfoByteArray, null);

                // Create a file in the application's isolated storage.
                IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
                IsolatedStorageFileStream writestream = new IsolatedStorageFileStream(filePath, FileMode.Create, FileAccess.Write, file);

                Stream writer = new StreamWriter(writestream).BaseStream;
                writer.Write(encryptedEmergencyInfoByteArray, 0, encryptedEmergencyInfoByteArray.Length);
                writer.Close();
                writestream.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        
        public void LoadInfo()
        {
            try
            {
                IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
                IsolatedStorageFileStream readstream = new IsolatedStorageFileStream(filePath, FileMode.Open, FileAccess.Read, file);

                if (readstream != null)
                {
                    Stream reader = new StreamReader(readstream).BaseStream;
                    byte[] encryptedEmergencyInfoByteArray = new byte[reader.Length];

                    reader.Read(encryptedEmergencyInfoByteArray, 0, encryptedEmergencyInfoByteArray.Length);
                    reader.Close();
                    readstream.Close();

                    byte[] emergencyInfoByteArray = ProtectedData.Unprotect(encryptedEmergencyInfoByteArray, null);

                    string emergencyInfoData = Encoding.UTF8.GetString(emergencyInfoByteArray, 0, emergencyInfoByteArray.Length);
                    EmergencyInfoData = new EmergencyInfo(emergencyInfoData);
                }
            }
            catch
            {
                //file may be corrupt or unavailable, so just load empty form
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
