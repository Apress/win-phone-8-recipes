using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Input;

namespace VoiceBucketList
{
    public partial class AddBucketListItem : PhoneApplicationPage
    {
        public AddBucketListItem()
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
                    case "AddTravel":
                        App.ViewModel.NewItem = new Models.BucketListItem { ItemType = BucketListItemType.Travel };
                        App.ViewModel.PageTitle = BucketListItemType.Travel.ToString();
                        break;
                    case "AddRestaurant":
                        App.ViewModel.NewItem = new Models.BucketListItem { ItemType = BucketListItemType.Restaurant };
                        App.ViewModel.PageTitle = BucketListItemType.Restaurant.ToString();
                        break;
                    default:
                        App.ViewModel.NewItem = new Models.BucketListItem { ItemType = BucketListItemType.Movie };
                        App.ViewModel.PageTitle = BucketListItemType.Movie.ToString();
                        break;
                }

            }
        }

        private void AddItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //force lost focus to occur on textbox so that the value gets updated in the binding
            var binding = itemToAdd.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();

            //let the viewmodel handle adding the item to the appropriate list based on item type
            App.ViewModel.AddNewItem();

            MessageBox.Show("The item has been added to the bucket list!");

            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            else
            {
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute));
            }
        }
    }
}