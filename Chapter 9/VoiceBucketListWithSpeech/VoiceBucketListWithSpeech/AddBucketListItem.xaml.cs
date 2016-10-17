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
using Windows.Phone.Speech.Recognition;

namespace VoiceBucketList
{
    public partial class AddBucketListItem : PhoneApplicationPage
    {
        SpeechRecognizerUI speechRecognizerUI;

        public AddBucketListItem()
        {
            InitializeComponent();
            DataContext = App.ViewModel;

            InitializeSpeechRecognizer();
        }

        private void InitializeSpeechRecognizer()
        {
            speechRecognizerUI = new SpeechRecognizerUI();
            speechRecognizerUI.Recognizer.Grammars.AddGrammarFromPredefinedType("voiceBucketListKeywords", SpeechPredefinedGrammar.Dictation);

            speechRecognizerUI.Settings.ReadoutEnabled = true;
            speechRecognizerUI.Settings.ShowConfirmation = true;

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

                        speechRecognizerUI.Settings.ListenText = "Which place do you want to add?";
                        speechRecognizerUI.Settings.ExampleText = "New York City";                        
                        break;
                    case "AddRestaurant":
                        App.ViewModel.NewItem = new Models.BucketListItem { ItemType = BucketListItemType.Restaurant };
                        App.ViewModel.PageTitle = BucketListItemType.Restaurant.ToString();
                        
                        speechRecognizerUI.Settings.ListenText = "Which restaurant do you want to add?";
                        speechRecognizerUI.Settings.ExampleText = "McDonalds";                        
                        break;
                    default:
                        App.ViewModel.NewItem = new Models.BucketListItem { ItemType = BucketListItemType.Movie };
                        App.ViewModel.PageTitle = BucketListItemType.Movie.ToString();

                        speechRecognizerUI.Settings.ListenText = "Which movie do you want to add?";
                        speechRecognizerUI.Settings.ExampleText = "Spiderman";
                        break;
                }

                PromptUserToSpeak();

            }
        }

        private async void PromptUserToSpeak()
        {            
            try
            {
                SpeechRecognitionUIResult recognitionResult = await speechRecognizerUI.RecognizeWithUIAsync();

                if (recognitionResult.ResultStatus == SpeechRecognitionUIStatus.Succeeded)
                {
                    App.ViewModel.NewItem.Name = recognitionResult.RecognitionResult.Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                NavigationService.Navigate(new Uri("/MainPage.xaml?clearHistory=1", UriKind.RelativeOrAbsolute));
            }
        }

    }
}