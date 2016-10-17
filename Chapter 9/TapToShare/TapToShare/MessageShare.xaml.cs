using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TapToShare.ViewModels;
using System.Windows.Input;

namespace TapToShare
{
    public partial class MessageShare : PhoneApplicationPage
    {
        MessageShareViewModel shareViewModel;

        public MessageShare()
        {
            InitializeComponent();

            shareViewModel = new MessageShareViewModel();
            this.DataContext = shareViewModel;            
        }

        private void Publish_Tap(object sender, GestureEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Content.ToString() == "publish")
            {
                if (!string.IsNullOrEmpty(messageToSend.Text))
                {
                    shareViewModel.PublishMessage(messageToSend.Text);
                    btn.Content = "stop publishing";
                }
                else
                {
                    MessageBox.Show("You must enter a message first, then tap the publish button.");
                }
            }
            else
            {
                shareViewModel.StopPublishingForMessage();
                btn.Content = "publish";
            }
        }

        private void Subscribe_Tap(object sender, GestureEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Content.ToString() == "subscribe")
            {
                shareViewModel.SubscribeForMessage();
                btn.Content = "stop subscribing";
            }
            else
            {
                shareViewModel.StopSubscribingForMessage();
                btn.Content = "subscribe";

            }
        }
    }
}