using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace VoiceBucketList
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.ContainsKey("voiceCommandName"))
            {
                string voiceCommand = NavigationContext.QueryString["voiceCommandName"];

                switch (voiceCommand)
                {
                    case "ViewTravelList":
                        pivotList.SelectedIndex = 1;
                        break;
                    case "ViewRestaurantList":
                        pivotList.SelectedIndex = 2;
                        break;
                    default:
                        pivotList.SelectedIndex = 0;
                        break;
                }

            }
        }

        private void AddNewItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string commandName;
            switch (pivotList.SelectedIndex)
            {
                case 1:
                    commandName = "AddTravel";
                    break;
                case 2:
                    commandName = "AddRestaurant";
                    break;
                default:
                    commandName = "AddMovie";
                    break;
            }

            NavigationService.Navigate(new Uri("/AddbucketListItem.xaml?voiceCommandName=" + commandName, UriKind.RelativeOrAbsolute));
        }
    }
}