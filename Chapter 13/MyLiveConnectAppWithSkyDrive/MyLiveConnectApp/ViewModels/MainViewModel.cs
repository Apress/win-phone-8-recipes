using Microsoft.Live;
using MyLiveConnectApp.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Windows.Storage;

namespace MyLiveConnectApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private enum Months
        {
            January = 1,
            February,
            March,
            April,
            May,
            June,
            July,
            August,
            September,
            October,
            November,
            December
        }

        #region Properties
        public LiveConnectClient LiveClient { get; set; }

        public string ClientId
        {
            get
            {
                return "TO DO: REGISTER YOUR APP FOR A CLIENT_ID ONLINE";
            }
        }

        private LiveConnectUser user;
        public LiveConnectUser User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged("User");

            }
        }


        private ObservableCollection<LiveConnectEvent> events;
        public ObservableCollection<LiveConnectEvent> Events
        {
            get
            {
                return events;
            }
            set
            {
                events = value;
                OnPropertyChanged("Events");
            }
        }

        private ObservableCollection<LiveConnectCalendar> calendars;
        public ObservableCollection<LiveConnectCalendar> Calendars
        {
            get
            {
                return calendars;
            }
            set
            {
                calendars = value;
                OnPropertyChanged("Calendars");
            }
        }

        private ObservableCollection<LiveConnectContact> contacts;
        public ObservableCollection<LiveConnectContact> Contacts
        {
            get
            {
                return contacts;
            }
            set
            {
                contacts = value;
                OnPropertyChanged("Contacts");
            }
        }

        private ObservableCollection<LiveConnectSkyDriveItem> skyDriveItems;
        public ObservableCollection<LiveConnectSkyDriveItem> SkyDriveItems
        {
            get
            {
                return skyDriveItems;
            }
            set
            {
                skyDriveItems = value;
                OnPropertyChanged("SkyDriveItems");
            }
        }

        private bool isConnected;
        public bool IsConnected
        {
            get
            {
                return isConnected;
            }
            set
            {
                isConnected = value;
                OnPropertyChanged("IsConnected");

            }
        }

        private string signInStatus = "Not signed in";
        public string SignInStatus
        {
            get
            {
                return signInStatus;
            }
            set
            {
                signInStatus = value;
                OnPropertyChanged("SignInStatus");
            }
        }
        #endregion

        #region Load/Reset User
        public void LoadUserInfo(dynamic result)
        {
            this.User = new LiveConnectUser();
            this.User.FirstName = result.first_name;
            this.User.LastName = result.last_name;

            string profilePageUrl = result.link + result.id;
            this.User.ProfileUri = new Uri(profilePageUrl, UriKind.Absolute);

            int birthMonth = result.birth_month;
            this.User.Birthday = Enum.Parse(typeof(Months), birthMonth.ToString()) + " " + result.birth_day;

            this.SignInStatus = "You are signed in with your Live ID!";
            this.IsConnected = true;
        }

        public void ResetUserInfo()
        {
            this.SignInStatus = "You are not signed in.";
            this.IsConnected = false;
            this.User = null;
            this.LiveClient = null;
        }
        #endregion

        
        public void LoadEvents(dynamic result)
        {
            this.Events = new ObservableCollection<LiveConnectEvent>();
            foreach (dynamic evnt in result)
            {
                LiveConnectEvent liveEvent = new LiveConnectEvent();
                liveEvent.Id = evnt.id;
                liveEvent.Name = evnt.name;
                liveEvent.Description = evnt.description;

                string startTimeString = evnt.start_time;
                if (!string.IsNullOrEmpty(startTimeString))
                {
                    DateTime start = DateTime.Parse(startTimeString);
                    liveEvent.StartTime = start;
                }


                string endTimeString = evnt.end_time;
                if (!string.IsNullOrEmpty(endTimeString))
                {
                    DateTime end = DateTime.Parse(endTimeString);
                    liveEvent.EndTime = end;
                }

                liveEvent.IsRecurrent = evnt.is_recurrent;
                liveEvent.IsAllDayEvent = evnt.is_all_day_event;
                this.Events.Add(liveEvent);
            }
        }

        public void LoadCalendars(dynamic result)
        {
            this.Calendars = new ObservableCollection<LiveConnectCalendar>();
            foreach (dynamic cal in result)
            {
                LiveConnectCalendar liveCalendar = new LiveConnectCalendar();
                liveCalendar.Id = cal.id;
                liveCalendar.Name = cal.name;
                liveCalendar.Description = cal.description;
                liveCalendar.Permissions = cal.permissions;
                this.Calendars.Add(liveCalendar);
            }
        }

        public void LoadContacts(dynamic result, bool isFriendsList)
        {
            this.Contacts = new ObservableCollection<LiveConnectContact>();
            foreach (dynamic contact in result)
            {
                LiveConnectContact liveContact = new LiveConnectContact();

                liveContact.Id = contact.id;
                liveContact.FirstName = (contact.first_name == null) ? contact.name : contact.first_name;

                if (!isFriendsList)
                {
                    liveContact.LastName = contact.last_name;
                    liveContact.Gender = contact.gender;
                    liveContact.IsFriend = contact.is_friend;
                    liveContact.IsFavourite = contact.is_favorite;
                }

                this.Contacts.Add(liveContact);
            }
        }

        public void LoadSkyDriveContents(dynamic result)
        {
            Visibility darkBackgroundVisibility = (Visibility)Application.Current.Resources["PhoneDarkThemeVisibility"];
            string folderTheme = (darkBackgroundVisibility == Visibility.Visible) ? "dark" : "light";
            string itemType = "";

            this.SkyDriveItems = new ObservableCollection<LiveConnectSkyDriveItem>();
            foreach (dynamic cal in result)
            {
                LiveConnectSkyDriveItem skyDriveItem = new LiveConnectSkyDriveItem();
                skyDriveItem.Id = cal.id;
                skyDriveItem.Name = cal.name;

                
                if (skyDriveItem.Id.Contains("folder"))
                {
                    itemType = "Folder";
                    skyDriveItem.IsFolder = true;
                    skyDriveItem.UploadLocation = cal.upload_location;
                }
                else
                {
                    itemType = "File";                 
                }

                skyDriveItem.ItemUri = new Uri(string.Format("/Assets/{0}/{1}.png", folderTheme, itemType), UriKind.RelativeOrAbsolute);
                    
                this.SkyDriveItems.Add(skyDriveItem);
            }
        }

        public async Task<bool> SaveFileToIsolatedStorage(Stream fileStream, string fileName)
        {
            try
            {
                //Get the local folder for the current application
                StorageFolder local = ApplicationData.Current.LocalFolder;

                if (local != null)
                {
                    //Create a new file, or update file if one already exists with the same name
                    StorageFile file = await local.CreateFileAsync(fileName, CreationCollisionOption.GenerateUniqueName);

                    //Write the file contents 
                    Stream newFileStream = await file.OpenStreamForWriteAsync();
                    await fileStream.CopyToAsync(newFileStream);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> OpenSavedFile(string fileName)
        {
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

            if (local != null)
            {
                try
                {
                    //Load the specified file just to see if it downloaded and saved successfully
                    Stream file = await local.OpenStreamForReadAsync(fileName);
                    file.Close();

                }
                catch (FileNotFoundException ex)
                {
                    return false;
                }
            }

            return true;
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
